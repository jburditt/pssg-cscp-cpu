import { Address } from "./address.class";
import { iAddress } from "./address.interface";
import { iPerson } from "./person.interface";

export class Person implements iPerson {
  address?: iAddress;
  addressSameAsAgency?: boolean;
  annualSalary?: number;
  baseHourlyWage?: number;
  benefits?: number;
  deactivated?: boolean;
  email: string;
  fax?: string;
  firstName: string;
  hoursWorkedPerWeek?: number;
  lastName: string;
  me?: boolean = false;
  middleName?: string;
  personId: string;
  phone?: string;
  phoneExtension?: string;
  phone2?: string;
  phone2Extension?: string;
  title?: string;
  userId?: string;
  employmentStatus?: string;
  constructor(person?: iPerson) {
    if (person) {
      this.address = new Address(person.address) || new Address();
      this.addressSameAsAgency = person.addressSameAsAgency || false;
      this.deactivated = person.deactivated || false;
      this.email = person.email || null;
      this.fax = person.fax || null;
      this.firstName = person.firstName || null;
      this.lastName = person.lastName || null;
      this.middleName = person.middleName || null;
      this.personId = person.personId || null;
      this.phone = person.phone || null;
      this.phoneExtension = person.phoneExtension || null;
      this.phone2 = person.phone2 || null;
      this.phone2Extension = person.phone2Extension || null;
      this.title = person.title || null;
      this.userId = person.userId || null;
      this.employmentStatus = person.employmentStatus || null;
      this.me = person.me;
    } else {
      this.address = new Address();
      this.addressSameAsAgency = false;
    }
  }

  private REQUIRED_FIELDS = ["firstName", "lastName", "title", "email", "phone2", "address", "address.line1", "address.city", "address.postalCode", "employmentStatus"];
  hasRequiredFields(skipEmploymentStatus: boolean = false) {
    for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
      if (skipEmploymentStatus && this.REQUIRED_FIELDS[i] === "employmentStatus") continue;
      if (!this.fetchFromObject(this, this.REQUIRED_FIELDS[i])) {
        return false;
      }
    }
    return true;
  }

  getMissingFields(skipEmploymentStatus: boolean = false) {
    let ret = [];
    for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
      if (skipEmploymentStatus && this.REQUIRED_FIELDS[i] === "employmentStatus") continue;
      if (!this.fetchFromObject(this, this.REQUIRED_FIELDS[i])) {
        ret.push(this.REQUIRED_FIELDS[i]);
      }
    }
    return ret;
  }

  private fetchFromObject(obj, prop) {
    if (typeof obj === 'undefined') {
      return false;
    }

    var _index = prop.indexOf('.')
    if (_index > -1) {
      return this.fetchFromObject(obj[prop.substring(0, _index)], prop.substr(_index + 1));
    }

    return obj[prop];
  }
}
