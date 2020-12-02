import { iHours } from "./hours.interface";

export class Hours implements iHours {
  monday: boolean;
  tuesday: boolean;
  wednesday: boolean;
  thursday: boolean;
  friday: boolean;
  saturday: boolean;
  sunday: boolean;
  open: string;
  isAMOpen: boolean;
  openMask: string;
  closed: string;
  isAMClosed: boolean;
  closedMask: string;
  hoursId: string;
  isActive: boolean;
  constructor(hours?: iHours) {
    if (hours) {
      this.monday = hours.monday || null;
      this.tuesday = hours.tuesday || null;
      this.wednesday = hours.wednesday || null;
      this.thursday = hours.thursday || null;
      this.friday = hours.friday || null;
      this.saturday = hours.saturday || null;
      this.sunday = hours.sunday || null;
      this.open = hours.open || null;
      this.isAMOpen = hours.isAMOpen || true;
      this.closed = hours.closed || null;
      this.isAMClosed = hours.isAMClosed || true;
      this.isActive = hours.isActive || true;
      this.openMask = this.open;
      this.closedMask = this.closed;
    }
    else {
      this.isActive = true;
      this.isAMOpen = true;
      this.isAMClosed = false;
    }
  }
}
