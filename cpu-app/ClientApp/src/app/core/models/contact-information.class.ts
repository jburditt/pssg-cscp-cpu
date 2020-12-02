import { Person } from "./person.class";
import { Address } from "./address.class";
import { iContactInformation } from "./contact-information.interface";
import { iAddress } from "./address.interface";
import { iPerson } from "./person.interface";
import { FormHelper } from "../form-helper";

export class ContactInformation implements iContactInformation {
  emailAddress: string;
  phoneNumber: string;
  faxNumber: string;
  mainAddress: iAddress;
  mailingAddress: iAddress;

  executiveContact: iPerson;
  boardContact: iPerson;
  private formHelper = new FormHelper();

  constructor(info?: iContactInformation) {
    if (info) {
      this.emailAddress = info.emailAddress || null;
      this.phoneNumber = info.phoneNumber || null;
      this.faxNumber = info.faxNumber || null;
      this.mainAddress = new Address(info.mainAddress) || new Address();
      this.mailingAddress = new Address(info.mailingAddress) || new Address();
      this.executiveContact = new Person(info.executiveContact) || new Person();
      this.boardContact = new Person(info.boardContact) || new Person();
    } else {
      this.mainAddress = new Address();
      this.mailingAddress = new Address();
      this.executiveContact = new Person();
      this.boardContact = new Person();
    }
  }

  private REQUIRED_FIELDS = ["emailAddress", "phoneNumber", "mainAddress", "mainAddress.line1", "mainAddress.city", "mainAddress.postalCode",
    "mailingAddress", "mailingAddress.line1", "mailingAddress.city", "mailingAddress.postalCode", "executiveContact.personId"];
  hasRequiredFields() {
    for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
      if (!this.formHelper.fetchFromObject(this, this.REQUIRED_FIELDS[i])) {
        return false;
      }
    }
    return true;
  }
  getMissingFields() {
    let ret = [];
    for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
      if (!this.formHelper.fetchFromObject(this, this.REQUIRED_FIELDS[i])) {
        ret.push(this.REQUIRED_FIELDS[i]);
      }
    }
    return ret;
  }
}
