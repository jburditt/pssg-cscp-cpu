import { Address } from '../../../core/models/address.class';
import { COUNTRIES_ADDRESS_2 } from '../../../core/constants/country-list';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormHelper } from '../../../core/form-helper';
import { POSTAL_CODE } from '../../../core/constants/regex.constants';
import { iAddress } from '../../../core/models/address.interface';

@Component({
  selector: 'app-address2',
  templateUrl: './address2.component.html',
  styleUrls: ['./address2.component.css']
})
export class Address2Component implements OnInit {

  public formHelper = new FormHelper();
  @Input() address: iAddress;
  @Input() isDisabled: boolean = false;
  @Input() addRequiredClass: boolean = false;
  @Output() addressChange = new EventEmitter<iAddress>();

  countries: any;
  country: any;
  postalRegex: RegExp;

  constructor() {
    this.postalRegex = POSTAL_CODE;
    this.countries = COUNTRIES_ADDRESS_2;
    this.country = this.address && this.address.country ? COUNTRIES_ADDRESS_2[this.address.country] : COUNTRIES_ADDRESS_2['Canada'];
  }

  ngOnInit() { }
  onChange() {
    this.addressChange.emit(this.address);
  }
}
