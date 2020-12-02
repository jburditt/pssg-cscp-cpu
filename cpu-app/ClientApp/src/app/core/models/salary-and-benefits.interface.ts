
export interface iSalaryAndBenefits {
  uuid: string;
  title: string;
  salary: number;
  salaryMask: string;
  benefits: number;
  benefitsMask: string;
  totalCost: number;
  totalCostMask: string
  fundedFromVscp: number;
  fundedFromVscpMask: string;
  isActive: boolean;
}
