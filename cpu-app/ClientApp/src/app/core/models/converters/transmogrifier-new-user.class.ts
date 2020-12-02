import { iDynamicsContactNotApproved } from "../dynamics-blob";
import { iPerson } from "../person.interface";
import { iServiceProvider } from "../service-provider.interface";
import { Person } from "../person.class";
import { ServiceProvider } from "../service-provider.class";
import { FormHelper } from "../../form-helper";

// a collection of the expense item guids as K/V pairs for generating line items
export class TransmogrifierNewUser {
    public organizationId: string;
    public userId: string;
    public person: iPerson;
    public serviceProvider: iServiceProvider;
    public isContractorContact: boolean;
    public formHelper = new FormHelper();

    private REQUIRED_FIELDS: string[] = ["person.firstName", "person.lastName", "person.title", "person.email", "person.phone", "person.address.line1", "person.address.city", "person.address.postalCode",];
    private REQUIRED_ORGANIZATION_FIELDS: string[] = ["serviceProvider.name", "serviceProvider.email", "serviceProvider.phone", "serviceProvider.address.line1", "serviceProvider.address.city", "serviceProvider.address.postalCode",];
    constructor(g: iDynamicsContactNotApproved = {}) {
        if (g) {
            this.userId = g.Userbceid;// this is the user's bceid
            this.organizationId = g.Businessbceid;// this is the organization's bceid
        }
        this.person = new Person();
        this.serviceProvider = new ServiceProvider();
        this.isContractorContact = false;
    }

    hasRequiredFields() {
        for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
            if (!this.formHelper.fetchFromObject(this, this.REQUIRED_FIELDS[i])) {
                return false;
            }
        }
        return true;
    }

    getMissingFields() {
        let ret = [];
        for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
            if (!this.formHelper.fetchFromObject(this, this.REQUIRED_FIELDS[i])) {
                ret.push(this.REQUIRED_FIELDS[i]);
            }
        }
        return ret;
    }

    hasRequiredOrganizationFields() {
        for (let i = 0; i < this.REQUIRED_ORGANIZATION_FIELDS.length; ++i) {
            if (!this.formHelper.fetchFromObject(this, this.REQUIRED_ORGANIZATION_FIELDS[i])) {
                return false;
            }
        }
        return true;
    }

    getMissingOrganizationFields() {
        let ret = [];
        for (let i = 0; i < this.REQUIRED_ORGANIZATION_FIELDS.length; ++i) {
            if (!this.formHelper.fetchFromObject(this, this.REQUIRED_ORGANIZATION_FIELDS[i])) {
                ret.push(this.REQUIRED_ORGANIZATION_FIELDS[i]);
            }
        }
        return ret;
    }

}

