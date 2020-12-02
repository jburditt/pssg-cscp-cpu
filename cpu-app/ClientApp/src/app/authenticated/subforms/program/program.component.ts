import { AbstractControl, FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { FormHelper } from '../../../core/form-helper';
import { Hours } from '../../../core/models/hours.class';
import { StateService } from '../../../core/services/state.service';
import { Transmogrifier } from '../../../core/models/transmogrifier.class';
import { iContactInformation } from '../../../core/models/contact-information.interface';
import { iPerson } from '../../../core/models/person.interface';
import { iProgramApplication } from '../../../core/models/program-application.interface';
import { EMAIL, PHONE_NUMBER } from '../../../core/constants/regex.constants';
import { iHours } from '../../../core/models/hours.interface';
import { perTypeDict } from '../../../core/constants/per-type';
import * as _ from 'lodash';
import { Subscription } from 'rxjs';
import { MatDialog } from '@angular/material';
import { AddPersonDialog } from '../../dialogs/add-person/add-person.dialog';
import { ProgramApplicationComponent } from '../../program-application/program-application.component';
import { Address } from '../../../core/models/address.class';

@Component({
  selector: 'app-program',
  templateUrl: './program.component.html',
  styleUrls: ['./program.component.scss']
})
export class ProgramComponent implements OnInit, OnDestroy {
  @Input() programApplication: iProgramApplication;
  @Input() isDisabled: boolean = false;
  @Output() programApplicationChange = new EventEmitter<iProgramApplication>();
  required = false;
  differentProgramContact: boolean = false;
  persons: iPerson[] = [];
  trans: Transmogrifier;
  programContactInformation: iContactInformation;
  tabs: string[];
  public formHelper = new FormHelper();
  emailRegex: RegExp;
  phoneRegex: RegExp;

  perType: string = "Week";
  personsObj: any = { persons: [], removedPersons: [] };
  subContractedPersonsObj: any = { persons: [], removedPersons: [] };
  private stateSubscription: Subscription;

  public programFormGroup: FormGroup;

  constructor(
    private stateService: StateService,
    public dialog: MatDialog,
    private fb: FormBuilder
  ) {
    this.tabs = ['Program Information', 'Program Hours of Operations'];
    this.emailRegex = EMAIL;
    this.phoneRegex = PHONE_NUMBER;
  }

  ngOnDestroy() {
    this.stateSubscription.unsubscribe();
  }
  ngOnInit() {
    this.stateSubscription = this.stateService.main.subscribe((m: Transmogrifier) => {
      this.trans = m;
      this.persons = m.persons;
      if (this.programApplication.standbyHours.length == 0) {
        this.addStandbyHours();
      }
      if (this.programApplication.operationHours.length == 0) {
        this.addOperationHours();
      }
    });
    this.onInput();
    this.personsObj.persons = this.programApplication.additionalStaff;
    this.personsObj.removedPersons = this.programApplication.removedStaff;
    this.subContractedPersonsObj.persons = this.programApplication.subContractedStaff;
    this.subContractedPersonsObj.removedPersons = this.programApplication.removedSubContractedStaff;
    this.perType = perTypeDict[this.programApplication.perType];
    if (this.programApplication.standbyHours.length == 0) {
      this.addStandbyHours();
    }
    if (this.programApplication.operationHours.length == 0) {
      this.addOperationHours();
    }

    this.programFormGroup = this.fb.group({
      'scheduledHours': new FormControl({ disabled: this.isDisabled, value: this.programApplication.scheduledHours }, Validators.min(this.programApplication.numberOfHours))
    });
  }

  showValidFeedback(control: AbstractControl): boolean { return !(control.valid && (control.dirty || control.touched)) }
  showInvalidFeedback(control: AbstractControl): boolean { return !(control.invalid && (control.dirty || control.touched)) }
  onInput() {
    this.programApplicationChange.emit(this.programApplication);
  }

  showProgramContact() {
    this.differentProgramContact = !this.differentProgramContact;
  }

  addOperationHours() {
    this.programApplication.operationHours.push(new Hours());
  }
  addStandbyHours() {
    this.programApplication.standbyHours.push(new Hours());
  }
  removeOperationHours(i: number) {
    if (this.programApplication.operationHours[i].hoursId) {
      this.programApplication.operationHours[i].isActive = false;
    }
    else {
      this.programApplication.operationHours = this.programApplication.operationHours.filter((hours: iHours, j: number) => i !== j);
    }
  }
  removeStandbyHours(i: number) {
    if (this.programApplication.standbyHours[i].hoursId) {
      this.programApplication.standbyHours[i].isActive = false;
    }
    else {
      this.programApplication.standbyHours = this.programApplication.standbyHours.filter((hours: iHours, j: number) => i !== j);
    }
  }

  onPaidStaffChange(event: iPerson[]) {
    this.programApplication.additionalStaff = event;
    this.onInput();

  }
  onProgramStaffChange(personsObj: any) {
    this.programApplication.additionalStaff = personsObj.persons;
    this.programApplication.removedStaff = personsObj.removedPersons;
    this.onInput();

  }
  onSubContractedStaffChange(personsObj: any) {
    this.programApplication.subContractedStaff = personsObj.persons;
    this.programApplication.removedSubContractedStaff = personsObj.removedPersons;
    this.onInput();

  }
  setCurrentTab(tab) {
    this.programApplication.currentTab = tab;
  }
  showAddProgramStaffDialog() {
    let dialogRef = this.dialog.open(AddPersonDialog, {
      autoFocus: false,
      width: '80vw',
      data: { agencyAddress: this.programApplication.mainAddress }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.stateService.refresh();
      }
    });
  }
  setAddressSameAsAgency(person: iPerson) {
    let addressCopy = _.cloneDeep(this.trans.contactInformation.mainAddress)
    person.address = addressCopy;
  }
  clearAddress(person: iPerson) {
    person.address = new Address();
  }
  setMailingAddressSameAsMainAddress() {
    if (this.programApplication.mailingAddressSameAsMainAddress) {
      this.programApplication.mailingAddress = this.programApplication.mainAddress;
    }
    else {
      this.programApplication.mailingAddress = new Address();
    }
  }
  hasSubContractedStaffChange() {
    console.log(this.programApplication.hasSubContractedStaff)
    if (!this.programApplication.hasSubContractedStaff) {
      for (let i = 0; i < this.programApplication.subContractedStaff.length; ++i) {
        let person: iPerson = this.trans.persons.filter(p => p.personId === this.programApplication.subContractedStaff[i].personId)[0];
        if (!person) {
          person = this.persons.filter(p => p.personId === this.programApplication.subContractedStaff[i].personId)[0];
        }

        if (person) {
          this.programApplication.removedSubContractedStaff.push(person);
        }
      }
      this.programApplication.subContractedStaff = [];
    }
  }
}
