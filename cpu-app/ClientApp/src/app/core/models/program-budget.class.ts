import { ExpenseItem } from "./expense-item.class";
import { SalaryAndBenefits } from "./salary-and-benefits.class";
import { iExpenseItem } from "./expense-item.interface";
import { iProgramBudget } from "./program-budget.interface";
import { iRevenueSource } from "./revenue-source.interface";
import { iSalaryAndBenefits } from "./salary-and-benefits.interface";

export class ProgramBudget implements iProgramBudget {
  contractId: string;
  programId: string;
  name: string;
  type: string;
  formState: string;
  email: string;
  contactLookupId?: string;
  revenueSources: iRevenueSource[];
  salariesAndBenefits: iSalaryAndBenefits[] = [];
  programDeliveryCosts: iExpenseItem[] = [];
  programDeliveryOtherExpenses: iExpenseItem[] = [];
  administrationCosts: iExpenseItem[] = [];
  administrationOtherExpenses: iExpenseItem[] = [];
  currentTab: string;
  constructor(pb?: iProgramBudget) {
    if (pb) {
      this.contractId = pb.contractId || null;
      this.programId = pb.programId || null;
      this.name = pb.name || null;
      this.type = pb.type || null;
      this.formState = pb.formState || null;
      this.email = pb.email || null;
      this.revenueSources = pb.revenueSources || null;
      this.contactLookupId = pb.contactLookupId || null;
      this.currentTab = 'Program Revenue Information';

      pb.salariesAndBenefits ? pb.salariesAndBenefits.forEach(x => this.salariesAndBenefits.push(new SalaryAndBenefits(x))) : this.salariesAndBenefits = [];
      pb.programDeliveryCosts ? pb.programDeliveryCosts.forEach(x => this.programDeliveryCosts.push(new ExpenseItem(x))) : this.programDeliveryCosts = [];
      pb.programDeliveryOtherExpenses ? pb.programDeliveryOtherExpenses.forEach(x => this.programDeliveryOtherExpenses.push(new ExpenseItem(x))) : this.programDeliveryOtherExpenses = [];
      pb.administrationCosts ? pb.administrationCosts.forEach(x => this.administrationCosts.push(new ExpenseItem(x))) : this.administrationCosts = [];
      pb.administrationOtherExpenses ? pb.administrationOtherExpenses.forEach(x => this.administrationOtherExpenses.push(new ExpenseItem(x))) : this.administrationOtherExpenses = [];
    }
  }
}
