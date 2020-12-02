import { iHours } from "../models/hours.interface";

export function decodeToWeekDays(csvString: string): Partial<iHours> {
  return {
    monday: csvString.indexOf('100000000') >= 0 ? true : false,
    tuesday: csvString.indexOf('100000001') >= 0 ? true : false,
    wednesday: csvString.indexOf('100000002') >= 0 ? true : false,
    thursday: csvString.indexOf('100000003') >= 0 ? true : false,
    friday: csvString.indexOf('100000004') >= 0 ? true : false,
    saturday: csvString.indexOf('100000005') >= 0 ? true : false,
    sunday: csvString.indexOf('100000006') >= 0 ? true : false,
  }
}
