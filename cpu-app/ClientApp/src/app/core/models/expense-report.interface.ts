
export interface iExpenseReport {
  expenseReportId?: string;
  reportingPeriod?: any;

  administrationDescription?: string;
  administrationAnnualBudget?: number;
  administrationQuarterlyBudget?: number;
  administrationValue?: number;
  administrationMask?: string;
  administrationQuarterlyVariance?: number;
  administrationYearToDate?: number;
  administrationYearToDateVariance?: number;

  programDeliveryDescription?: string;
  programDeliveryAnnualBudget?: number;
  programDeliveryQuarterlyBudget?: number;
  programDeliveryValue?: number;
  programDeliveryMask?: string;
  programDeliveryQuarterlyVariance?: number;
  programDeliveryYearToDate?: number;
  programDeliveryYearToDateVariance?: number;

  salariesBenefitsDescription?: string;
  salariesBenefitsAnnualBudget?: number;
  salariesBenefitsQuarterlyBudget?: number;
  salariesBenefitsValue?: number;
  salariesBenefitsMask?: string;
  salariesBenefitsQuarterlyVariance?: number;
  salariesBenefitsYearToDate?: number;
  salariesBenefitsYearToDateVariance?: number;

  programExpenseLineItems: iExpenseReportLineItem[];

  serviceHours?: number;
  perType?: number,
  onCallStandByHours?: number;
  serviceHoursQuarterlyActual?: number;
  executiveReview?: boolean;
}

export interface iExpenseReportLineItem {
  itemId: string;
  label: string;
  annualBudget: number;
  quarterlyBudget: number;
  actual: number;
  mask: string;
  quarterlyVariance: number;
  actualYearToDate: number;
  yearToDateVariance: number;
  description: string;
}
