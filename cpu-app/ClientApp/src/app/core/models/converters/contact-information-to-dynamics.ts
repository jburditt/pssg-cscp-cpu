import { iDynamicsPostOrg } from "../dynamics-post";
import { Transmogrifier } from "../transmogrifier.class";

// this is a mapper function for posting the organization back to dynamics
export function convertContactInformationToDynamics(trans: Transmogrifier): iDynamicsPostOrg {
  return {
    BusinessBCeID: trans.organizationId,
    UserBCeID: trans.userId,
    Organization: {
      vsd_ExecutiveContactIdfortunecookiebind: trans.contactInformation.executiveContact && trans.contactInformation.executiveContact.personId || null,
      vsd_BoardContactIdfortunecookiebind: trans.contactInformation.hasBoardContact && trans.contactInformation.boardContact && trans.contactInformation.boardContact.personId || null,
      accountid: trans.accountId || null,
      address1_city: trans.contactInformation.mainAddress.city || null,
      address1_line1: trans.contactInformation.mainAddress.line1 || null,
      address1_line2: trans.contactInformation.mainAddress.line2 || null,
      address1_postalcode: trans.contactInformation.mainAddress.postalCode || null,
      address1_stateorprovince: trans.contactInformation.mainAddress.province || null,
      address2_city: trans.contactInformation.mailingAddress.city || null,
      address2_line1: trans.contactInformation.mailingAddress.line1 || null,
      address2_line2: trans.contactInformation.mailingAddress.line2 || null,
      address2_postalcode: trans.contactInformation.mailingAddress.postalCode || null,
      address2_stateorprovince: trans.contactInformation.mailingAddress.province || null,
      emailaddress1: trans.contactInformation.emailAddress || null,
      fax: trans.contactInformation.faxNumber || null,
      name: trans.organizationName,
      telephone1: trans.contactInformation.phoneNumber || null,
    }
  };
}
