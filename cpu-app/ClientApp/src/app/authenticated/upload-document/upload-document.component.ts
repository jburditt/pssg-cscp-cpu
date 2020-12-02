import { Component, ElementRef, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { FileService } from '../../core/services/file.service';
import { NotificationQueueService } from '../../core/services/notification-queue.service';
import { Router, ActivatedRoute } from '@angular/router';
import { StateService } from '../../core/services/state.service';
import { iDynamicsFile, iDynamicsDocument } from '../../core/models/dynamics-blob';
import { iDynamicsPostFile } from '../../core/models/dynamics-post';
import { FormHelper } from '../../core/form-helper';
import { Transmogrifier } from '../../core/models/transmogrifier.class';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-upload-document',
  templateUrl: './upload-document.component.html',
  styleUrls: ['./upload-document.component.scss']
})
export class UploadDocumentComponent implements OnInit, OnDestroy {
  @ViewChild('files')
  myInputVariable: ElementRef;

  saving: boolean = false;
  loadingDocuments: boolean = false;
  didLoadDocuments: boolean = false;
  existingDocuments: iDynamicsDocument[] = [];

  downloadingDocument: boolean = false;

  documentCollection: iDynamicsDocument[] = [];
  documentsToAdd: iDynamicsDocument[] = [];
  fileNames: string[] = [];
  fileSizes: string[] = [];
  fileData: string[] = [];

  trans: Transmogrifier;
  contractNumber: string;
  organizationId: string;
  userId: string;
  contractId: string;
  private stateSubscription: Subscription;
  isContractUpload: boolean = false;

  private ORGANIZATION_DOCUMENT_TYPES: string[] = ["AGM Minutes", "Annual Report", "Audit Letter", "Financial Statements (Audited/Unaudited)", "Certificate of Insurance", "General Service Agreement", "Referral Protocol (PBVS & CBVS)", "Other"];
  private CONTRACT_DOCUMENT_TYPES: string[] = ["Custom Financial Report", "Custom Statistical Report", "Custom Activity Report", "Counselor Support Plan", "Letter of Reference", "Direct Deposit Form", "Invoice/Expense Report", "Other"];

  document_types: string[] = [];
  selectedDocumentType: string = "";

  private formHelper = new FormHelper();
  constructor(
    private fileService: FileService,
    private router: Router,
    private stateService: StateService,
    private notificationQueueService: NotificationQueueService,
    private route: ActivatedRoute,
  ) { }

  ngOnDestroy() {
    this.stateSubscription.unsubscribe();
  }

  ngOnInit() {
    this.stateSubscription = this.stateService.main.subscribe((m: Transmogrifier) => {
      this.trans = m;
    });

    this.route.params.subscribe(p => {
      this.userId = this.stateService.main.getValue().userId;
      this.organizationId = this.stateService.main.getValue().organizationId;
      this.document_types = this.ORGANIZATION_DOCUMENT_TYPES;
      this.contractId = p['contractId'];
      if (this.contractId) {
        this.document_types = this.CONTRACT_DOCUMENT_TYPES;
        this.isContractUpload = true;
        this.contractNumber = this.trans.contracts.find(c => c.contractId == this.contractId).contractNumber;

        console.log("getContractDocuments");
        this.getContractDocuments();
      }
      else {
        console.log("getAccountDocuments");
        this.getAccountDocuments();
      }
    });

  }
  exit() {
    if (this.formHelper.isFormDirty() && confirm("Are you sure you want to return to the dashboard? All unsaved work will be lost.")) {
      this.stateService.refresh();
      this.router.navigate(['/authenticated/dashboard']);
    }
    else {
      this.stateService.refresh();
      this.router.navigate(['/authenticated/dashboard']);
    }
  }

  refresh() {
    this.isContractUpload ? this.getContractDocuments() : this.getAccountDocuments();
  }

  removeFile(i: number) {
    this.fileData.splice(i, 1);
    this.fileNames.splice(i, 1);
    this.fileSizes.splice(i, 1);
    this.documentsToAdd.splice(i, 1);
  }
  upload() {
    this.saving = true;

    this.documentsToAdd.forEach(doc => {
      if (doc.subject === "Other" && doc.subjectOther) {
        doc.subject = doc.subjectOther;
      }
    });
    const file: iDynamicsPostFile = {
      Businessbceid: this.organizationId,
      Userbceid: this.userId,
      DocumentCollection: this.documentsToAdd
    }
    let record_id = this.isContractUpload ? this.contractId : this.trans.accountId
    if (this.isContractUpload) {
      this.fileService.uploadContractDocuments(file, record_id).subscribe((d) => {
        if (d.IsSuccess) {
          console.log("uploaded documents");
          console.log(d);
          this.saving = false;
          this.documentsToAdd = [];
          this.refresh();
          this.notificationQueueService.addNotification(`Documents successfully uploaded.`, 'success');
        }
        else {
          this.saving = false;
          this.notificationQueueService.addNotification(`There was a problem uploading Documents.`, 'danger');
        }
      });
    }
    else {
      this.fileService.uploadAccountDocuments(file, record_id).subscribe((d) => {
        if (d.IsSuccess) {
          console.log("uploaded documents");
          console.log(d);
          this.saving = false;
          this.documentsToAdd = [];
          this.refresh();
          this.notificationQueueService.addNotification(`Documents successfully uploaded.`, 'success');
        }
        else {
          this.saving = false;
          this.notificationQueueService.addNotification(`There was a problem uploading Documents.`, 'danger');
        }
      });
    }
  }

  fakeBrowseClick(): void {
    this.myInputVariable.nativeElement.click();
  }
  onFilesAdded(files: FileList): void {
    const fileNames = this.fileNames;
    const fileSizes = this.fileSizes;
    const fileData = this.fileData;
    for (let i = 0; i < files.length; i++) {
      const reader: FileReader = new FileReader();
      if (files.item(i).size > 10000000) {
        this.notificationQueueService.addNotification(`The file "${files.item(i).name}" is too large. If this is an image or collection of images please add compression or lower the resolution.`, 'danger');
      } else {
        reader.readAsDataURL(files.item(i));
        reader.onload = () => {
          const fileNumber = fileNames.indexOf(files.item(i).name);
          let fileDataString = reader.result.toString();
          fileDataString = fileDataString.split(',').slice(-1)[0];
          if (fileNumber >= 0) {
            fileData[fileNumber] = fileDataString;
            fileSizes[fileNumber] = this.toFileSize(files.item(i).size)

          } else {
            if (files.item(i).name.split('.').slice(-1)[0].indexOf("pdf") < 0) {
              this.notificationQueueService.addNotification(`Unsupported file type.`, 'danger');
              return;
            }

            fileNames.push(files.item(i).name);
            fileData.push(fileDataString);
            fileSizes.push(this.toFileSize(files.item(i).size));

            this.documentsToAdd.push({
              filename: files.item(i).name,
              subject: this.selectedDocumentType,
              subjectOther: "",
              body: fileDataString
            });
          }
        };
        reader.onerror = error => console.log('Error: ', error);
      }
    }
    this.fileData = fileData;
    this.fileSizes = fileSizes;
    this.fileData = fileData;
  }

  download(doc: iDynamicsDocument) {
    if (this.downloadingDocument) return;
    this.downloadingDocument = true;
    this.fileService.downloadDocument(this.organizationId, this.userId, doc.activitymimeattachmentid).subscribe(
      (d: any) => {
        this.downloadingDocument = false;
        console.log(d);
        if (!d.IsSuccess) {
          this.notificationQueueService.addNotification('There has been a data problem retrieving this file. Please let your ministry contact know that you have seen this error.', 'danger');
        }
        else {
          let downloadLink = document.createElement("a");
          downloadLink.href = "data:application/octet-stream;base64," + d.Body;
          downloadLink.download = d.FileName;

          document.body.appendChild(downloadLink);
          downloadLink.click();
          document.body.removeChild(downloadLink);
        }
      });
  }

  toFileSize(bytes: number): string {
    let fileSize: string = "0 bytes";
    if (bytes >= 1073741824) { fileSize = (bytes / 1073741824).toFixed(2) + " GB"; }
    else if (bytes >= 1048576) { fileSize = (bytes / 1048576).toFixed(2) + " MB"; }
    else if (bytes >= 1024) { fileSize = (bytes / 1024).toFixed(2) + " KB"; }
    else if (bytes > 1) { fileSize = bytes + " bytes"; }
    else if (bytes == 1) { fileSize = bytes + " byte"; }
    return fileSize;
  }

  getContractDocuments() {
    this.loadingDocuments = true;
    this.fileService.getContractDocuments(this.organizationId, this.userId, this.contractId).subscribe(
      (d: iDynamicsFile) => {
        this.loadingDocuments = false;
        this.didLoadDocuments = true;
        if (!d.IsSuccess) {
          this.notificationQueueService.addNotification(`There has been a data problem retrieving this file. Please let your ministry contact know that you have seen this error.`, 'danger');
        } else {
          this.existingDocuments = d.DocumentCollection.filter(d => d.filename.indexOf(".pdf") > 0);
        }
      });
  }
  getAccountDocuments() {
    this.loadingDocuments = true;
    this.fileService.getAccountDocuments(this.organizationId, this.userId, this.trans.accountId).subscribe(
      (d: iDynamicsFile) => {
        this.loadingDocuments = false;
        this.didLoadDocuments = true;
        if (!d.IsSuccess) {
          this.notificationQueueService.addNotification(`There has been a data problem retrieving this file. Please let your ministry contact know that you have seen this error.`, 'danger');
        } else {
          this.existingDocuments = d.DocumentCollection.filter(d => d.filename.indexOf(".pdf") > 0);
        }
      });
  }
}
