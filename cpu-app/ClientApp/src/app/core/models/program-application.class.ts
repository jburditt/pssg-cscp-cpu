import { iProgramApplication } from "./program-application.interface";
import { iPerson } from "./person.interface";
import { iAddress } from "./address.interface";
import { iHours } from "./hours.interface";
import { FormHelper } from "../form-helper";
import { Person } from "./person.class";


export class ProgramApplication implements iProgramApplication {
    contractId: string;
    emailAddress: string;
    faxNumber: string;
    formState: string;
    name: string;
    programTypeName: string;
    phoneNumber: string;
    programId: string;
    assignmentArea: string;
    programLocation: string;
    serviceAreas: string[];
    hasMailingAddress?: boolean;
    isPoliceBased: boolean;
    governmentFunder: string;
    estimatedContractValue: number;
    estimatedContractValueMask: string;

    additionalStaff: iPerson[];
    subContractedStaff: iPerson[];
    removedStaff: iPerson[];
    removedSubContractedStaff: iPerson[];
    isTransitionHouse: boolean = false;
    mailingAddress: iAddress;
    mainAddress: iAddress;
    mailingAddressSameAsMainAddress: boolean;
    programContact: iPerson;
    policeContact: iPerson;
    hasPoliceContact: boolean;
    sharedCostContact: iPerson;
    hasSubContractedStaff: boolean;
    hasSharedCostContact: boolean;
    numberOfHours: number;
    scheduledHours: number;
    onCallHours: number;
    operationHours: iHours[];
    standbyHours: iHours[];
    perType: number;

    currentTab: string;
    private formHelper = new FormHelper();
    constructor(pa?: iProgramApplication) {
        if (pa) Object.assign(this, pa);
    }

    private REQUIRED_FIELDS: string[] = ["emailAddress", "phoneNumber", "mainAddress", "mainAddress.line1", "mainAddress.city", "mainAddress.postalCode",
        "mailingAddress", "mailingAddress.line1", "mailingAddress.city", "mailingAddress.postalCode"];

    private MAIN_ADDRESS: string[] = ["mainAddress", "mainAddress.line1", "mainAddress.city", "mainAddress.postalCode"];

    private MAILING_ADDRESS: string[] = ["mailingAddress", "mailingAddress.line1", "mailingAddress.city", "mailingAddress.postalCode"];

    hasRequiredFields() {
        let req_fields = this.REQUIRED_FIELDS;
        if (this.isTransitionHouse) {
            //main address not required
            req_fields = req_fields.filter((field) => !this.MAIN_ADDRESS.includes(field));

            //mailing address not required
            if (this.mailingAddressSameAsMainAddress) req_fields = req_fields.filter((field) => !this.MAILING_ADDRESS.includes(field));
        }

        for (let i = 0; i < req_fields.length; ++i) {
            if (!this.formHelper.fetchFromObject(this, req_fields[i])) {
                return false;
            }
        }
        let skip_employment_status_validation = true;

        if (this.hasPoliceContact) {
            let policeContact = new Person(this.policeContact);

            if (!policeContact.hasRequiredFields(skip_employment_status_validation)) {
                return false;
            }
        }

        if (this.hasSharedCostContact) {
            let sharedCostContact = new Person(this.sharedCostContact);
            if (!sharedCostContact.hasRequiredFields(skip_employment_status_validation)) {
                return false;
            }
        }
        if (this.hasSubContractedStaff) {
            if (this.subContractedStaff.length <= 0) {
                return false;
            }
        }
        return true;
    }
    getMissingFields() {
        let req_fields = this.REQUIRED_FIELDS;
        if (this.isTransitionHouse) {
            //main address not required
            req_fields = req_fields.filter((field) => !this.MAIN_ADDRESS.includes(field));

            //mailing address not required
            if (this.mailingAddressSameAsMainAddress) req_fields = req_fields.filter((field) => !this.MAILING_ADDRESS.includes(field));
        }
        let ret = [];
        for (let i = 0; i < req_fields.length; ++i) {
            if (!this.formHelper.fetchFromObject(this, req_fields[i])) {
                ret.push(req_fields[i]);
            }
        }
        let skip_employment_status_validation = true;

        if (this.hasPoliceContact) {
            let policeContact = new Person(this.policeContact);

            if (!policeContact.hasRequiredFields(skip_employment_status_validation)) {
                ret = ret.concat(policeContact.getMissingFields(skip_employment_status_validation));
            }
        }

        if (this.hasSharedCostContact) {
            let sharedCostContact = new Person(this.sharedCostContact);
            if (!sharedCostContact.hasRequiredFields(skip_employment_status_validation)) {
                ret = ret.concat(sharedCostContact.getMissingFields(skip_employment_status_validation));
            }
        }
        if (this.hasSubContractedStaff) {
            if (this.subContractedStaff.length <= 0) {
                ret.push("subContractedStaff.length");
            }
        }
        return ret;
    }

}
