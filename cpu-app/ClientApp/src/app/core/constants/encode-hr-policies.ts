import { iAdministrativeInformation } from "../models/administrative-information.interface";

export function encodeHrPolicies(a: iAdministrativeInformation): string {
  // convert back to a human readable array
  let temp: string = '';
  if (a.compliantEmploymentStandardsAct) temp += '100000000,';
  if (a.compliantHumanRights) temp += '100000001,';
  if (a.compliantWorkersCompensation) temp += '100000002,';
  // if the temp string is still 0 length return a blank string
  return temp.length ? temp.substr(0, temp.length - 1) : null;
}
