import { iServiceProvider } from "./service-provider.interface";
import { iAddress } from "./address.interface";
import { Address } from "./address.class";

export class ServiceProvider implements iServiceProvider {
    address?: iAddress;
    name: string;
    email: string;
    fax?: string;
    phone?: string;
    constructor(sp?: iServiceProvider) {
        if (sp) {
            this.address = new Address(sp.address) || new Address();
            this.name = sp.name || "";
            this.email = sp.email || null;
            this.phone = sp.phone || null;
            this.fax = sp.fax || null;
        } else {
            this.name = "";
            this.address = new Address();
        }
    }
}
