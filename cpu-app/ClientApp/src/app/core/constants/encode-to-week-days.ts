import { iHours } from "../models/hours.interface";

export function encodeToWeekDayCodes(hours: iHours): string {
  let weekdayCodes = '';
  if (hours.monday) weekdayCodes += '100000000,';
  if (hours.tuesday) weekdayCodes += '100000001,';
  if (hours.wednesday) weekdayCodes += '100000002,';
  if (hours.thursday) weekdayCodes += '100000003,';
  if (hours.friday) weekdayCodes += '100000004,';
  if (hours.saturday) weekdayCodes += '100000005,';
  if (hours.sunday) weekdayCodes += '100000006,';

  // the last character is a comma always. remove before returne
  return weekdayCodes.substring(0, weekdayCodes.length - 1);
}
