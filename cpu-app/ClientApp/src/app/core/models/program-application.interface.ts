import { iPerson } from "./person.interface";
import { iHours } from './hours.interface';
import { iAddress } from "./address.interface";
import { iContactInformation } from "./contact-information.interface";

export interface iProgramApplication extends iContactInformation {
  contractId: string;
  emailAddress: string;
  faxNumber: string;
  formState: string;
  name: string;
  programTypeName: string;
  phoneNumber: string;
  programId: string;
  assignmentArea: string;
  programLocation: string;
  serviceAreas: string[];
  hasMailingAddress?: boolean;
  isPoliceBased: boolean;
  governmentFunder: string;
  estimatedContractValue: number;
  estimatedContractValueMask: string;

  additionalStaff: iPerson[];
  subContractedStaff: iPerson[];
  removedStaff: iPerson[];
  removedSubContractedStaff: iPerson[];
  isTransitionHouse: boolean;
  mailingAddress: iAddress;
  mainAddress: iAddress;
  mailingAddressSameAsMainAddress: boolean;
  programContact: iPerson;
  policeContact: iPerson;
  hasPoliceContact: boolean;
  sharedCostContact: iPerson;
  hasSubContractedStaff: boolean;
  hasSharedCostContact: boolean;
  numberOfHours: number;
  scheduledHours: number;
  onCallHours: number;
  operationHours: iHours[];
  standbyHours: iHours[];
  perType: number;

  currentTab: string;
}
