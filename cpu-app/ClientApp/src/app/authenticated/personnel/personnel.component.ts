import { Component, OnInit, OnDestroy, AfterContentChecked } from '@angular/core';
import { NotificationQueueService } from '../../core/services/notification-queue.service';
import { Person } from '../../core/models/person.class';
import { PersonService } from '../../core/services/person.service';
import { Router } from '@angular/router';
import { StateService } from '../../core/services/state.service';
import { Transmogrifier } from '../../core/models/transmogrifier.class';
import { convertPersonToDynamics } from '../../core/models/converters/person-to-dynamics';
import { iDynamicsPostUsers } from '../../core/models/dynamics-post';
import { iPerson } from '../../core/models/person.interface';
import { iStepperElement, IconStepperService } from '../../shared/icon-stepper/icon-stepper.service';
import { nameAssemble } from '../../core/constants/name-assemble';
import { convertPersonnelToDynamics } from '../../core/models/converters/personnel-to-dynamics';
import { FormHelper } from '../../core/form-helper';
import { FormGroup } from '@angular/forms';
import * as _ from 'lodash';
import { Subscription } from 'rxjs';
import { Address } from '../../core/models/address.class';

@Component({
  selector: 'app-personnel',
  templateUrl: './personnel.component.html',
  styleUrls: ['./personnel.component.css']
})
export class PersonnelComponent implements OnInit, OnDestroy {
  reload = false;
  currentStepperElement: iStepperElement;
  stepperIndex: number = 0;
  stepperElements: iStepperElement[];
  trans: Transmogrifier;
  saving: boolean = false;
  public nameAssemble = nameAssemble;
  public formHelper = new FormHelper();
  personForm: FormGroup;
  didLoad: boolean = false;
  private stateSubscription: Subscription;
  missingFields: string[] = [];

  originalPersons: iPerson[] = [];
  constructor(
    private router: Router,
    private personService: PersonService,
    private notificationQueueService: NotificationQueueService,
    private stepperService: IconStepperService,
    private stateService: StateService,
  ) { }

  ngOnInit() {
    this.stepperService.currentStepperElement.subscribe(e => this.currentStepperElement = e);
    this.stepperService.stepperElements.subscribe(e => this.stepperElements = e);
    this.stateSubscription = this.stateService.main.subscribe((m: Transmogrifier) => {
      this.trans = m;
      this.originalPersons = _.cloneDeep(this.trans.persons);

      this.constructStepperElements(m);
      this.didLoad = true;
    });
  }
  ngOnDestroy() {
    if (!_.isEqual(this.originalPersons, this.trans.persons)) {
      // console.log("setting persons back to original values")
      this.trans.persons = this.originalPersons;
    }
    this.stepperService.reset();
    this.stateSubscription.unsubscribe();
  }
  constructStepperElements(m: Transmogrifier): void {
    this.stepperService.reset();
    if (m.persons) {
      m.persons.sort(function (a, b) {
        let name1 = nameAssemble(a.firstName, a.middleName, a.lastName);
        let name2 = nameAssemble(b.firstName, b.middleName, b.lastName);
        return name1 > name2 ? 1 : name1 < name2 ? -1 : 0;
      });
      m.persons.forEach(person => {
        this.stepperService.addStepperElement(person, nameAssemble(person.firstName, person.middleName, person.lastName), null, 'person');
      });
      if (!this.didLoad) {
        this.stepperService.setToFirstStepperElement();
      }
      else {
        this.stepperService.setCurrentStepperElement(this.stepperElements[this.stepperIndex].id);
      }
    }
  }

  save() {
    let person = this.trans.persons.find(p => p.personId == this.currentStepperElement.object['personId']);

    if (!person) {
      // console.log("no one to save...");
      return;
    }

    try {
      if (!this.formHelper.isFormValid(this.notificationQueueService)) {
        return;
      }
      if (!person.employmentStatus || person.employmentStatus === "null") {
        this.notificationQueueService.addNotification('Employment status is required.', 'warning');
        return;
      }

      // console.log(person);
      this.saving = true;
      let thisPerson = new Person(person);
      if (thisPerson.hasRequiredFields()) {
        this.missingFields = [];
        const userId = this.stateService.main.getValue().userId;
        const organizationId = this.stateService.main.getValue().organizationId;
        const post = convertPersonnelToDynamics(userId, organizationId, [person]);
        this.personService.setPersons(post).subscribe(
          (r) => {
            if (r.IsSuccess) {
              this.saving = false;
              this.notificationQueueService.addNotification(`Information is saved for ${nameAssemble(person.firstName, person.middleName, person.lastName)}`, 'success');
              this.stepperIndex = this.stepperElements.findIndex(s => s.id === this.currentStepperElement.id) || 0;
              this.stateService.refresh();
              this.formHelper.makeFormClean();
            }
            else {
              this.notificationQueueService.addNotification("There was a problem saving this person. If this problem is persisting please contact your ministry representative.", 'danger');
              this.saving = false;
            }
          },
          err => {
            this.notificationQueueService.addNotification(err, 'danger');
            this.saving = false;
          }
        );
      } else {
        this.saving = false;
        this.missingFields = thisPerson.getMissingFields();
        this.notificationQueueService.addNotification('Please fill in required fields.', 'warning');
      }
    }
    catch (err) {
      console.log(err);
      this.notificationQueueService.addNotification('The agency staff could not be saved. If this problem is persisting please contact your ministry representative.', 'danger');
      this.saving = false;
    }
  }

  saveAndExit(person: iPerson) {
    try {
      if (!this.formHelper.isFormValid(this.notificationQueueService)) {
        return;
      }
      if (!person.employmentStatus || person.employmentStatus === "null") {
        this.notificationQueueService.addNotification('Employment status is required.', 'warning');
        return;
      }

      // console.log(person);
      this.saving = true;
      let thisPerson = new Person(person);
      if (thisPerson.hasRequiredFields()) {
        this.missingFields = [];
        const userId = this.stateService.main.getValue().userId;
        const organizationId = this.stateService.main.getValue().organizationId;
        const post = convertPersonnelToDynamics(userId, organizationId, [person]);
        this.personService.setPersons(post).subscribe(
          (r) => {
            if (r.IsSuccess) {
              this.saving = false;
              this.notificationQueueService.addNotification(`Information is saved for ${nameAssemble(person.firstName, person.middleName, person.lastName)}`, 'success');

              this.stateService.refresh();
              this.router.navigate(['/authenticated/dashboard']);
            }
            else {
              this.notificationQueueService.addNotification("There was a problem saving this person. If this problem is persisting please contact your ministry representative.", 'danger');
              this.saving = false;
            }
          },
          err => {
            this.notificationQueueService.addNotification(err, 'danger');
            this.saving = false;
          }
        );
      } else {
        this.saving = false;
        this.missingFields = thisPerson.getMissingFields();
        this.notificationQueueService.addNotification('Please fill in required fields.', 'warning');
      }
    }
    catch (err) {
      console.log(err);
      this.notificationQueueService.addNotification('The agency staff could not be saved. If this problem is persisting please contact your ministry representative.', 'danger');
      this.saving = false;
    }
  }

  cancel(person: iPerson) {
    let reset = _.cloneDeep(this.originalPersons);
    if (this.currentStepperElement.itemName === 'New Person') {
      let temp = new Person();
      reset.push(temp);
    }
    this.trans.persons = reset;
  }

  exit() {
    if (this.formHelper.isFormDirty()) {
      if (confirm("Are you sure you want to return to the dashboard? All unsaved work will be lost.")) {
        this.stateService.refresh();
        this.router.navigate(['/authenticated/dashboard']);
      }
    }
    else {
      this.stateService.refresh();
      this.router.navigate(['/authenticated/dashboard']);
    }
  }

  add() {
    const element = new Person();
    this.stepperService.addStepperElement(element, 'New Person', null, 'person');
    this.trans.persons.push(element);
  }
  remove(person: iPerson) {
    try {
      if (!person.personId) {
        this.stateService.refresh();
      } else if (!person.me && confirm(`Are you sure that you want to deactivate ${person.firstName} ${person.lastName}? This user will no longer be available in the system.`)) {
        this.saving = true;
        person.deactivated = true;
        const userId = this.stateService.main.getValue().userId;
        const organizationId = this.stateService.main.getValue().organizationId;
        const post: iDynamicsPostUsers = {
          UserBCeID: userId,
          BusinessBCeID: organizationId,
          StaffCollection: [convertPersonToDynamics(person)]
        };
        this.personService.setPersons(post).subscribe(r => {
          if (r.IsSuccess) {
            this.saving = false;

            if (this.stepperIndex > 0) --this.stepperIndex;
            this.stateService.refresh();
          }
          else {
            this.notificationQueueService.addNotification('The agency staff could not be saved. If this problem is persisting please contact your ministry representative.', 'danger');
            this.saving = false;
          }
        });
      }
    }
    catch (err) {
      console.log(err);
      this.notificationQueueService.addNotification('The agency staff could not be saved. If this problem is persisting please contact your ministry representative.', 'danger');
      this.saving = false;
    }
  }
  onChange(element: iStepperElement) {
    this.missingFields = [];
    element.itemName = nameAssemble(element.object['firstName'], element.object['middleName'], element.object['lastName']);
    this.stepperService.setStepperElement(element);
  }
  setAddressSameAsAgency(person: iPerson) {
    let addressCopy = _.cloneDeep(this.trans.contactInformation.mainAddress)
    person.address = addressCopy;
  }
  clearAddress(person: iPerson) {
    person.address = new Address();
  }
}
