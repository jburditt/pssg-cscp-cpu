import { iRevenueSource } from "./revenue-source.interface";
import { iExpenseItem } from "./expense-item.interface";
import { iSalaryAndBenefits } from "./salary-and-benefits.interface";

export interface iProgramBudget {
  contactLookupId?: string;
  contractId: string;
  email: string;
  formState?: string;
  name: string;
  programId: string;
  type?: string;
  administrationCosts: iExpenseItem[];
  administrationOtherExpenses: iExpenseItem[];
  programDeliveryCosts: iExpenseItem[];
  programDeliveryOtherExpenses: iExpenseItem[];
  revenueSources: iRevenueSource[];
  salariesAndBenefits: iSalaryAndBenefits[];

  currentTab: string;
}
