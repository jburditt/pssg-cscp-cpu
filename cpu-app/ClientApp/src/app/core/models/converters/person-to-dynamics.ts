import { iPerson } from "../person.interface";
import { iDynamicsCrmContact } from "../dynamics-blob";
import { employmentStatusTypeDict } from "../../constants/employment-status-types";

// this is a mapper function that converts one person into a crm contact
export function convertPersonToDynamics(person: iPerson): iDynamicsCrmContact {
  const p: iDynamicsCrmContact = {};
  // add all properties that are non null
  if (person.address && person.address.city) p.address1_city = person.address.city;
  if (person.address && person.address.line1) p.address1_line1 = person.address.line1;
  if (person.address && person.address.line2) p.address1_line2 = person.address.line2;
  if (person.address && person.address.postalCode) p.address1_postalcode = person.address.postalCode;
  if (person.address && person.address.province) p.address1_stateorprovince = person.address.province;
  if (person.personId) p.contactid = person.personId;
  if (person.email) p.emailaddress1 = person.email;
  if (person.fax) p.fax = person.fax;
  if (person.firstName) p.firstname = person.firstName;
  if (person.title) p.jobtitle = person.title;
  if (person.lastName) p.lastname = person.lastName;
  if (person.middleName) p.middlename = person.middleName;
  if (person.phone) p.mobilephone = person.phone;
  if (person.phoneExtension) p.vsd_mainphoneextension = person.phoneExtension;
  if (person.phone2) p.telephone2 = person.phone2;
  if (person.phone2Extension) p.vsd_homephoneextension = person.phone2Extension;
  if (person.employmentStatus) p.vsd_employmentstatus = parseInt(Object.keys(employmentStatusTypeDict).find(key => employmentStatusTypeDict[key] === person.employmentStatus));
  if (person.deactivated === true) p.statecode = 1;
  if (person.deactivated === false) p.statecode = 0; // sending a 1 statuscode means soft delete the record
  // return the person
  return p;
}
