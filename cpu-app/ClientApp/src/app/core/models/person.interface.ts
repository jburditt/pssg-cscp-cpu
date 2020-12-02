import { iAddress } from "./address.interface";

export interface iPerson {
  address?: iAddress;
  addressSameAsAgency?: boolean;
  deactivated?: boolean; // if true this deactivates
  email: string;
  fax?: string;
  firstName: string;
  lastName: string;
  me?: boolean; // is this the current user? If so this is "me".
  middleName?: string;
  orgId?: string;
  personId?: string;
  phone?: string;
  phoneExtension?: string;
  phone2?: string;
  phone2Extension?: string;
  title?: string;
  userId?: string;
  employmentStatus?: string;
  vsd_portalfield?: string;
}
