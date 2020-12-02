import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { iContract } from '../../../core/models/contract.interface';
import { TaskStatus } from '../../../core/constants/task-status';
import { formTypes } from '../../../core/constants/form-types';
import { FileService } from '../../../core/services/file.service';
import { StateService } from '../../../core/services/state.service';
import { Subscription } from 'rxjs';
import { Transmogrifier } from '../../../core/models/transmogrifier.class';
import { iDynamicsFile, iDynamicsDocument, iDynamicsMonthlyStatistics, iDynamicsDataCollection } from '../../../core/models/dynamics-blob';
import { StatusReportService } from '../../../core/services/status-report.service';
import { months } from '../../../core/constants/month-codes';
import { iTask } from '../../../core/models/task.interface';
import * as moment from 'moment';
import { CRMPaymentStatusCode } from '../../../core/models/payment-status.interface';
import { Roles } from '../../../core/models/user-settings.interface';
import { NotificationQueueService } from '../../../core/services/notification-queue.service';

enum StatusReasons {
  Received = 1,
  Processing = 100000000,
  Approved = 100000001,
  Information_Denied = 100000002,
}

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit, OnDestroy {
  @Input() contract: iContract;
  trans: Transmogrifier;

  tabs: any;
  currentTab: string;

  statuses: string[];
  formTypes: string[];

  didLoadStats: boolean = false;
  loadingStats: boolean = false;
  completedStats: iDynamicsMonthlyStatistics;
  dataCollection: iDynamicsDataCollection[] = [];


  didLoadDocuments: boolean = false;
  documentCollection: iDynamicsDocument[] = [];
  private stateSubscription: Subscription;
  loadingDocuments: boolean = false;
  downloadingDocument: boolean = false;

  organizationId: string;
  userId: string;
  today = moment().endOf('day');

  StatusReasons = StatusReasons;
  PaymentStatusCode = CRMPaymentStatusCode;

  userRole = Roles.ProgramStaff
  Roles = Roles;

  constructor(private stateService: StateService,
    private statusReportService: StatusReportService,
    private notificationQueueService: NotificationQueueService,
    public fileService: FileService) {
    this.tabs = {
      'tasksDue': 'Tasks Due',
      'tasksCompleted': 'Completed Tasks',
      'completedStats': 'Completed Monthly Reports',
      'editProgramInformation': 'Update Program/Contact Information',
      'yourDocuments': 'Your Documents',
      'messages': 'Messages'
    };
    this.currentTab = this.tabs.tasksDue;
    this.statuses = TaskStatus;
    this.formTypes = formTypes;

  }

  ngOnInit() {
    let userSettings = this.stateService.userSettings.getValue();
    this.userRole = userSettings.userRole;
    this.stateSubscription = this.stateService.main.subscribe((m: Transmogrifier) => {
      this.trans = m;

      this.organizationId = this.stateService.main.getValue().organizationId;
      this.userId = this.stateService.main.getValue().userId;
    });

    this.contract.tasks.forEach((task: iTask) => {
      task.isOverDue = this.today.isAfter(moment(task.deadline));
    });
  }
  ngOnDestroy() {
    this.stateSubscription.unsubscribe();
  }

  setCurrentTab(tab: any) {
    this.currentTab = tab;
  }

  getCompletedMonthlyStats(contractId: string) {
    if (this.didLoadStats) {
      return;
    }

    console.log("getting monthly stats");
    this.loadingStats = true;



    this.statusReportService.getMonthlyStats(this.organizationId, this.userId, contractId).subscribe((res: iDynamicsMonthlyStatistics) => {
      this.loadingStats = false;
      this.didLoadStats = true;
      if (!res.IsSuccess) {
        this.notificationQueueService.addNotification('There was an issue loading Monthly Stats information. If this problem is persisting please contact your ministry representative.', 'danger');
      }
      else {
        console.log("Monthly Stats:");
        console.log(res);
        this.completedStats = res;
        this.dataCollection = this.completedStats.DataCollection;
        for (let data of this.dataCollection) {
          data.reportingPeriod = Object.keys(months).find(key => months[key] === data.vsd_reportingperiod);
        }
        console.log(this.dataCollection);
      }
    });
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

  getContractDocuments(contractId: string) {
    if (this.didLoadDocuments) {
      return;
    }
    this.loadingDocuments = true;
    this.fileService.getContractDocuments(this.trans.organizationId, this.trans.userId, contractId).subscribe(
      (d: iDynamicsFile) => {
        this.didLoadDocuments = true;
        this.loadingDocuments = false;
        if (!d.IsSuccess) {
          this.notificationQueueService.addNotification('There has been a data problem retrieving this file. Please let your ministry contact know that you have seen this error.', 'danger');
        }
        else {
          this.documentCollection = d.DocumentCollection.filter(d => d.filename.indexOf(".pdf") > 0);;
        }
      });
  }
}
