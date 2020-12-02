import { iExpenseItem } from "./expense-item.interface";
import { uuidv4 } from "../constants/uuidv4";

export class ExpenseItem implements iExpenseItem {
  itemName: string;
  tooltip: string;
  cost: number;
  costMask: string;
  fundedFromVscp: number;
  fundedFromVscpMask: string;
  uuid: string;
  otherExpenseDescription: string;
  isActive: boolean;
  constructor(xi?: iExpenseItem) {
    if (xi) {
      this.itemName = xi.itemName || null;
      this.tooltip = xi.tooltip || null;
      this.cost = xi.cost || null;
      this.costMask = xi.cost ? xi.cost.toString() : "0";
      this.fundedFromVscp = xi.fundedFromVscp || null;
      this.fundedFromVscpMask = xi.fundedFromVscp ? xi.fundedFromVscp.toString() : "0";
      this.uuid = xi.uuid || null;
      this.otherExpenseDescription = xi.otherExpenseDescription || null;
      this.isActive = xi.isActive || true;
    } else {
      this.uuid = null;
      this.fundedFromVscpMask = "0";
      this.costMask = "0";
      this.isActive = true;
    }
  }
}
