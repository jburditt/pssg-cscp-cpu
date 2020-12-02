export interface iHours {
  monday?: boolean;
  tuesday?: boolean;
  wednesday?: boolean;
  thursday?: boolean;
  friday?: boolean;
  saturday?: boolean;
  sunday?: boolean;
  open: string;
  isAMOpen: boolean;
  openMask: string;
  closed: string;
  isAMClosed: boolean;
  closedMask: string;
  hoursId: string;
  isActive: boolean;
}
