import { iPerson } from './person.interface';
import { iAdministrativeInformation } from './administrative-information.interface';
import { Person } from './person.class';
import { FormHelper } from '../form-helper';

export class AdministrativeInformation implements iAdministrativeInformation {
  compliantEmploymentStandardsAct: boolean;
  compliantHumanRights: boolean;
  compliantWorkersCompensation: boolean;
  staffUnionized: boolean;
  staffUnion: string;
  ccseaMemberType: string;
  staffSubcontracted: boolean;
  staffSubcontractedPersons: iPerson[] = [];
  awareOfCriminalRecordCheckRequirement: boolean;
  private formHelper = new FormHelper();

  constructor(ai?: iAdministrativeInformation) {
    if (ai) {
      Object.assign(this, ai);
      this.staffSubcontractedPersons = [];
      ai.staffSubcontractedPersons ? ai.staffSubcontractedPersons.forEach(x => this.staffSubcontractedPersons.push(new Person(x))) : this.staffSubcontractedPersons = [];
    }
  }

  private REQUIRED_FIELDS = ["compliantEmploymentStandardsAct", "compliantHumanRights", "compliantWorkersCompensation", "staffUnionized", "awareOfCriminalRecordCheckRequirement"];
  hasRequiredFields() {
    for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
      let val = this.formHelper.fetchFromObject(this, this.REQUIRED_FIELDS[i]);
      //a bit weird because staff unionized can be false, but the other 3 check boxes must be true
      if (val === null || val === undefined || (this.REQUIRED_FIELDS[i] !== "staffUnionized" && val === "false")) {
        return false;
      }
    }
    return true;
  }
  getMissingFields() {
    let ret = [];
    for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
      let val = this.formHelper.fetchFromObject(this, this.REQUIRED_FIELDS[i]);
      //a bit weird because staff unionized can be false, but the other 3 check boxes must be true
      if (val === null || val === undefined || (this.REQUIRED_FIELDS[i] !== "staffUnionized" && val === "false")) {
        ret.push(this.REQUIRED_FIELDS[i]);
      }
    }
    return ret;
  }
}
