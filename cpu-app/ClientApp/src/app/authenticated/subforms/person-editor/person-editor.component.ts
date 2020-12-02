import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { iPerson } from '../../../core/models/person.interface';
import { FormHelper } from '../../../core/form-helper';
import { EMAIL, PHONE_NUMBER, LETTERS_SPACES, NAME_REGEX } from '../../../core/constants/regex.constants';

@Component({
  selector: 'app-person-editor',
  templateUrl: './person-editor.component.html',
  styleUrls: ['./person-editor.component.css']
})
export class PersonEditorComponent implements OnInit {
  @Input() person: iPerson;
  @Input() isDisabled: boolean = false;
  @Input() idNum: number = 0;
  @Input() isPoliceContact: boolean = false;
  @Input() invalidFields: string[] = [];
  @Output() personChange = new EventEmitter<iPerson>();
  @Output() setAddress = new EventEmitter<iPerson>();
  @Output() clearAddress = new EventEmitter<iPerson>();

  public formHelper = new FormHelper();
  me: boolean = false;
  emailRegex: RegExp = EMAIL;
  phoneRegex: RegExp = PHONE_NUMBER;
  wordRegex: RegExp = LETTERS_SPACES;
  nameRegex: RegExp = NAME_REGEX;
  constructor() { }

  ngOnInit() {
    if (this.person.me) {
      this.me = true;
    }
  }

  onChanges() {
    this.personChange.emit(this.person);
  }

  setAddressSameAsAgency() {
    if (!this.person.addressSameAsAgency) {
      this.setAddress.emit(this.person);
    }
    else {
      this.clearAddress.emit(this.person)
    }
  }
}
