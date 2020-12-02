import { iAddress } from "./address.interface";

export interface iServiceProvider {
    name: string;
    email: string;
    fax?: string;
    phone?: string;
    address?: iAddress;
}
