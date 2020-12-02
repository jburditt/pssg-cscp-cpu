import { iProgramBudget } from './program-budget.interface';

export interface iBudgetProposal {
  organizationId: string;
  contractId: string;
  programs: iProgramBudget[];
  formState: string;
}




