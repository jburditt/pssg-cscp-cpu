import { cglInsuranceDict } from "./cgl-insurance";

export function encodeCglInsurance(description: string): number {
  const reverseDict = {};
  for (let property in cglInsuranceDict) {
    if (cglInsuranceDict.hasOwnProperty(property)) {
      reverseDict[cglInsuranceDict[property]] = property;
    }
  }
  return parseInt(reverseDict[description]) || null;
}
