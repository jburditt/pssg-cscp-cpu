import { iAddress } from "./address.interface";

export class Address implements iAddress {
  line1: string;
  line2: string;
  city: string;
  postalCode: string;
  province: string;
  country: string;
  constructor(address?: iAddress) {
    if (address) {
      this.line1 = address.line1 || null;
      this.line2 = address.line2 || null;
      this.postalCode = address.postalCode || null;
      this.city = address.city || null;
      this.province = address.province || 'British Columbia';
      this.country = address.country || 'Canada';
    } else {
      this.country = 'Canada';
      this.province = 'British Columbia';
    }
  }
}
