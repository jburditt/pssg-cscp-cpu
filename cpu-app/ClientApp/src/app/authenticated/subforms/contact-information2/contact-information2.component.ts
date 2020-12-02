import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EMAIL, PHONE_NUMBER } from '../../../core/constants/regex.constants';
import { FormHelper } from '../../../core/form-helper';
import { iContactInformation } from '../../../core/models/contact-information.interface';


@Component({
  selector: 'app-contact-information2',
  templateUrl: './contact-information2.component.html',
  styleUrls: ['./contact-information2.component.css']
})
export class ContactInformation2Component implements OnInit {
  @Input() contactInformation: iContactInformation;
  @Output() contactInformationChange = new EventEmitter<iContactInformation>();

  public formHelper = new FormHelper();
  emailRegex: RegExp = EMAIL;
  phoneRegex: RegExp = PHONE_NUMBER;

  constructor() { }

  ngOnInit() {
  }

}
