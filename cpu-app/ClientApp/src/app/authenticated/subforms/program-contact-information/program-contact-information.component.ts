import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { ContactInformation } from '../../../core/models/contact-information.class';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { StateService } from '../../../core/services/state.service';
import { Transmogrifier } from '../../../core/models/transmogrifier.class';
import { iContactInformation } from '../../../core/models/contact-information.interface';
import { iPerson } from '../../../core/models/person.interface';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-program-contact-information',
  templateUrl: './program-contact-information.component.html',
  styleUrls: ['./program-contact-information.component.css']
})
export class ProgramContactInformationComponent implements OnInit, OnDestroy {
  @Input() contactInformation: iContactInformation;
  @Output() contactInformationChange = new EventEmitter<iContactInformation>();
  @Input() required = true;
  @Input() title = 'Primary Program Contact Information';

  contactInformationForm: FormGroup;
  persons: iPerson[] = [];
  hasBoardOfDirectors: boolean = false;
  private stateSubscription: Subscription;

  constructor(
    private stateService: StateService,
  ) { }

  ngOnDestroy() {
    this.stateSubscription.unsubscribe();
  }
  ngOnInit() {
    this.hasBoardOfDirectors = this.contactInformation.boardContact ? true : false;
    this.contactInformationForm = new FormGroup({
      'contactInformation': new FormControl('', Validators.required)
    });
    this.stateSubscription = this.stateService.main.subscribe((m: Transmogrifier) => {
      this.contactInformationForm.controls['contactInformation'].setValue(m.contactInformation);
      this.persons = m.persons;
    });
  }
  onInput() {
    const ci: ContactInformation = this.contactInformationForm.value['contactInformation'];
    ci.boardContact = this.contactInformation.boardContact;
    ci.executiveContact = this.contactInformation.executiveContact;
    this.contactInformationChange.emit(this.contactInformationForm.value['contactInformation']);
  }
  cleanBoardContact() {
    if (!this.hasBoardOfDirectors) {
      this.contactInformation.boardContact = null;
    }
  }
}
