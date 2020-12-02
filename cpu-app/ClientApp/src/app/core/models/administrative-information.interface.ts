import { iPerson } from './person.interface';

export interface iAdministrativeInformation {
  compliantEmploymentStandardsAct: boolean;
  compliantHumanRights: boolean;
  compliantWorkersCompensation: boolean;
  staffUnionized?: boolean;
  staffUnion: string;
  ccseaMemberType: string;
  staffSubcontracted: boolean;
  staffSubcontractedPersons: iPerson[];
  awareOfCriminalRecordCheckRequirement: boolean;
}
