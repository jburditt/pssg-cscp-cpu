import { iRevenueSource } from "./revenue-source.interface";

export class RevenueSource implements iRevenueSource {
  revenueSourceName: string;
  cash: number;
  cashMask: string;
  inKindContribution: number;
  inKindContributionMask: string;
  total: number;
  totalMask: string;
  other: string;
  revenueSourceId: string;
  isActive: boolean;
  constructor(rs?: iRevenueSource) {
    if (rs) {
      this.revenueSourceName = rs.revenueSourceName || null;
      this.cash = rs.cash || null;
      this.cashMask = rs.cash ? rs.cash.toString() : "0";
      this.inKindContribution = rs.inKindContribution || null;
      this.inKindContributionMask = rs.inKindContribution ? rs.inKindContribution.toString() : "0";
      this.total = this.cash + this.inKindContribution || null;
      this.totalMask = this.total ? this.total.toString() : "0";
      this.other = rs.other || null;
      this.revenueSourceId = rs.revenueSourceId || null;
      this.isActive = rs.isActive || true;
    }
    else {
      this.isActive = true;
      this.cash = 0;
      this.cashMask = "0";
      this.inKindContribution = 0;
      this.inKindContributionMask = "0";
      this.total = 0;
      this.totalMask = "0";
    }
  }
}
