import { iAddress } from "./address.interface";
import { iPerson } from "./person.interface";

export interface iContactInformation {
  boardContact?: iPerson;
  emailAddress: string;
  executiveContact?: iPerson;
  faxNumber: string;
  hasBoardContact?: boolean;
  hasMailingAddress?: boolean;
  mailingAddressSameAsMainAddress?: boolean;
  mailingAddress: iAddress;
  mainAddress: iAddress;
  phoneNumber: string;
}

