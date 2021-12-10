import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ProfileService } from '../../core/services/profile.service';
import { Router } from '@angular/router';
import { StateService } from '../../core/services/state.service';
import { Transmogrifier } from '../../core/models/transmogrifier.class';
import { convertContactInformationToDynamics } from '../../core/models/converters/contact-information-to-dynamics';
import { NotificationQueueService } from '../../core/services/notification-queue.service';
import { FormHelper } from '../../core/form-helper';
import * as _ from 'lodash';
import { Subscription } from 'rxjs';
import { Address } from '../../core/models/address.class';
import { ContactInformation } from '../../core/models/contact-information.class';
import { iContactInformation } from '../../core/models/contact-information.interface';
import { PersonPickerComponent } from '../subforms/person-picker/person-picker.component';
import { Roles } from '../../core/models/user-settings.interface';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {
  @ViewChild(PersonPickerComponent) contractorContactComp: PersonPickerComponent;
  @ViewChild(PersonPickerComponent) boardContactComp: PersonPickerComponent;
  trans: Transmogrifier;
  saving: boolean = false;
  private stateSubscription: Subscription;

  private formHelper = new FormHelper();
  originalContactInfo: iContactInformation;
  Roles = Roles;
  userRole = Roles.ProgramStaff;

  constructor(
    private router: Router,
    private stateService: StateService,
    private profileService: ProfileService,
    private notificationQueueService: NotificationQueueService
  ) { }

  ngOnInit() {
    let userSettings = this.stateService.userSettings.getValue();
    this.userRole = userSettings.userRole;
    this.stateSubscription = this.stateService.main.subscribe((m: Transmogrifier) => {
      this.trans = m;
      this.originalContactInfo = _.cloneDeep(this.trans.contactInformation);
    });
  }
  ngOnDestroy() {
    if (!_.isEqual(this.originalContactInfo, this.trans.contactInformation)) {
      // console.log("setting trans contact info back to original");
      this.trans.contactInformation = this.originalContactInfo;
    }
    this.stateSubscription.unsubscribe();
  }
  cancel() {
    this.trans.contactInformation = this.originalContactInfo;
    this.originalContactInfo = _.cloneDeep(this.trans.contactInformation);
    if (this.trans.contactInformation.executiveContact && this.trans.contactInformation.executiveContact.personId) {
      this.contractorContactComp.setPerson(this.trans.contactInformation.executiveContact.personId);
    }
    if (this.trans.contactInformation.boardContact && this.trans.contactInformation.boardContact.personId) {
      this.boardContactComp.setPerson(this.trans.contactInformation.boardContact.personId);
    }
  }
  save(shouldExit: boolean = false): void {
    try {
      if (!this.formHelper.isFormValid(this.notificationQueueService)) {
        return;
      }

      let contactInfo = new ContactInformation(this.trans.contactInformation);
      if (contactInfo.hasRequiredFields()) {
        this.saving = true;
        // console.log(JSON.parse(JSON.stringify(this.trans)));
        this.profileService.updateOrg(convertContactInformationToDynamics(this.trans))
          .subscribe(
            (res: any) => {
              this.saving = false;
              this.notificationQueueService.addNotification('The contact information for your organization has been updated.', 'success');
              this.stateService.refresh();
              this.formHelper.makeFormClean();
              if (shouldExit) this.router.navigate([this.stateService.homeRoute.getValue()]);
            },
            err => console.log(err)
          );
      }
      else {
        // console.log(contactInfo.getMissingFields());
        this.saving = false;
        this.notificationQueueService.addNotification('Please fill in required fields.', 'warning');
      }

    }
    catch (err) {
      console.log(err);
      this.notificationQueueService.addNotification('The contact information for your organization could not be saved. If this problem is persisting please contact your ministry representative.', 'danger');
      this.saving = false;
    }
  }
  exit() {
    this.stateService.refresh();
    this.router.navigate(['/authenticated/dashboard']);
  }
  setMailingAddressSameAsMainAddress() {
    if (this.trans.contactInformation.mailingAddressSameAsMainAddress) {
      this.trans.contactInformation.mailingAddress = this.trans.contactInformation.mainAddress;
    }
    else {
      this.trans.contactInformation.mailingAddress = new Address();
    }
  }
}
