import { cglInsuranceDict } from "./cgl-insurance";

export function decodeCglInsurance(code: number): string {
  return cglInsuranceDict[code] || null;
}
