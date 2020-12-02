export interface iExpenseItem {
  cost: number;
  costMask: string;
  fundedFromVscp: number;
  fundedFromVscpMask: string;
  itemName: string;
  uuid: string;
  tooltip?: string;
  otherExpenseDescription?: string;
  isActive?: boolean;
}
