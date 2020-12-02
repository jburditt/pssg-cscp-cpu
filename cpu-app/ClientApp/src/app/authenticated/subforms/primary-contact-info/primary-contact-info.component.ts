import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { iContactInformation } from '../../../core/models/contact-information.interface';
import { EMAIL, PHONE_NUMBER } from '../../../core/constants/regex.constants';
import { FormHelper } from '../../../core/form-helper';

@Component({
  selector: 'app-primary-contact-info',
  templateUrl: './primary-contact-info.component.html',
  styleUrls: ['./primary-contact-info.component.css']
})
export class PrimaryContactInfoComponent implements OnInit {
  @Input() contactInformation: iContactInformation;
  @Input() isDisabled: boolean = false;
  @Output() contactInformationChange = new EventEmitter<iContactInformation>();

  public formHelper = new FormHelper();
  emailRegex: RegExp = EMAIL;
  phoneRegex: RegExp = PHONE_NUMBER;
  constructor() { }

  ngOnInit() {
  }
}
