import { decodeCcseaMemberType } from '../constants/decode-ccsea-member-type';
import { decodeCglInsurance } from '../constants/decode-cgl-insurance-type';
import { decodeToWeekDays } from '../constants/decode-to-week-days';
import { iAdministrativeInformation } from "./administrative-information.interface";
import { iContactInformation } from "./contact-information.interface";
import { iDynamicsScheduleFCAPResponse, iDynamicsCrmContact, iDynamicsCrmContract, iDynamicsServiceArea } from "./dynamics-blob";
import { iHours } from "./hours.interface";
import { iPerson } from "./person.interface";
import { iProgramApplication } from "./program-application.interface";
import { iSignature } from '../../authenticated/subforms/program-authorizer/program-authorizer.component';
import { makeViewTimeString } from './converters/hours-to-dynamics';
import { ngDevModeResetPerfCounters } from '@angular/core/src/render3/ng_dev_mode';
import { boolOptionSet } from '../constants/bool-optionset-values';
import { perTypeDict } from '../constants/per-type';
import { Person } from './person.class';
import { employmentStatusTypeDict, employmentStatus } from '../constants/employment-status-types';
import * as _ from 'lodash';
import { iCAPProgram } from './cap-program.interface';

export class TransmogrifierCAPApplication {
    accountId: string;// this is the dynamics account
    applicantInformation: iContactInformation;
    applyingForInsurance: boolean;
    insuranceProvider: string;
    // contactInformation: iContactInformation;
    fiscalYear: string;
    contractId: string;
    contractName: string;
    contractNumber: string;
    organizationId: string;
    organizationName: string;
    capPrograms: iCAPProgram[];
    signature: iSignature;
    userId: string;

    constructor(g: iDynamicsScheduleFCAPResponse) {
        this.accountId = g.Organization.accountid;
        this.contractId = g.Contract.vsd_contractid;
        this.contractName = g.Contract.vsd_name;
        this.contractNumber = g.Contract.vsd_name;
        this.applyingForInsurance = g.Contract.vsd_cpu_insuranceoptions === 100000001 ? true : g.Contract.vsd_cpu_insuranceoptions === 100000000 ? false : null;
        this.insuranceProvider = ""; //TODO - get this field added in dynamics
        this.organizationId = g.Businessbceid;
        this.organizationName = g.Organization.name;
        this.userId = g.Userbceid;
        this.fiscalYear = "";
        this.applicantInformation = this.buildApplicantInformation(g);
        this.capPrograms = this.buildPrograms(g);
        this.signature = this.buildSignature(g);
    }
    private buildSignature(b: iDynamicsScheduleFCAPResponse): iSignature {
        return {
            signer: undefined,
            signature: "",
            signatureDate: undefined,
            termsConfirmation: false
        };
    }
    private buildApplicantInformation(b: iDynamicsScheduleFCAPResponse): iContactInformation {
        const c: iContactInformation = {
            emailAddress: b.Organization.emailaddress1 || null,
            faxNumber: b.Organization.fax || null,
            phoneNumber: b.Organization.telephone1 || null,
            mailingAddress: {
                city: b.Organization.address1_city || null,
                country: b.Organization.address1_country || 'Canada',
                line1: b.Organization.address1_line1 || null,
                line2: b.Organization.address1_line2 || null,
                postalCode: b.Organization.address1_postalcode || null,
                province: b.Organization.address1_stateorprovince || null
            },
            mainAddress: {
                city: b.Organization.address2_city || null,
                country: b.Organization.address2_country || 'Canada',
                line1: b.Organization.address2_line1 || null,
                line2: b.Organization.address2_line2 || null,
                postalCode: b.Organization.address2_postalcode || null,
                province: b.Organization.address2_stateorprovince || null
            },
            // if any of the properties besides the country is not null then they have a mailing address (API limitation)
            hasMailingAddress: !!(b.Organization.address2_city || b.Organization.address2_line1 || b.Organization.address2_line2 || b.Organization.address2_stateorprovince || b.Organization.address2_postalcode)

        }

        c.mailingAddressSameAsMainAddress = _.isEqual(c.mainAddress, c.mailingAddress);
        if (c.mailingAddressSameAsMainAddress) c.mailingAddress = c.mainAddress;
        // when the board contact and the executive contact are the same person then we simply don't fill in executive contact information and set the flag to false
        if (b.BoardContact && (b.Contract._vsd_contactlookup1_value !== b.Contract._vsd_contactlookup2_value)) {
            c.boardContact = {
                userId: b.BoardContact.contactid || null,
                email: b.BoardContact.emailaddress1 || null,
                fax: b.BoardContact.fax || null,
                firstName: b.BoardContact.firstname || null,
                lastName: b.BoardContact.lastname || null,
                middleName: b.BoardContact.middlename || null,
                personId: b.BoardContact.contactid || null,
                phone: b.BoardContact.mobilephone || null,
                title: b.BoardContact.jobtitle || null,
                address: {
                    city: b.BoardContact.address1_city || null,
                    country: b.BoardContact.address1_country || 'Canada',
                    line1: b.BoardContact.address1_line1 || null,
                    line2: b.BoardContact.address1_line2 || null,
                    postalCode: b.BoardContact.address1_postalcode || null,
                    province: b.BoardContact.address1_stateorprovince || null
                }
            };
        }
        // the board contact's existence determines whether or not this flag is true or false.
        c.hasBoardContact = !!c.boardContact;

        if (b.ExecutiveContact) c.executiveContact = {
            userId: b.ExecutiveContact.contactid || null,
            email: b.ExecutiveContact.emailaddress1 || null,
            fax: b.ExecutiveContact.fax || null,
            firstName: b.ExecutiveContact.firstname || null,
            lastName: b.ExecutiveContact.lastname || null,
            middleName: b.ExecutiveContact.middlename || null,
            personId: b.ExecutiveContact.contactid || null,
            phone: b.ExecutiveContact.mobilephone || null,
            title: b.ExecutiveContact.jobtitle || null,
            address: {
                city: b.ExecutiveContact.address1_city || null,
                country: b.ExecutiveContact.address1_country || 'Canada',
                line1: b.ExecutiveContact.address1_line1 || null,
                line2: b.ExecutiveContact.address1_line2 || null,
                postalCode: b.ExecutiveContact.address1_postalcode || null,
                province: b.ExecutiveContact.address1_stateorprovince || null,
            }
        }
        return c;
    }
    private buildPrograms(g: iDynamicsScheduleFCAPResponse): iCAPProgram[] {
        const programs: iCAPProgram[] = [];
        for (let p of g.ProgramCollection) {
            let temp: iCAPProgram = {
                contractId: p._vsd_contractid_value,
                formState: 'untouched',
                name: p.vsd_name,
                programId: p.vsd_programid,
                programContact: g.StaffCollection
                    .filter((c: iDynamicsCrmContact): boolean => p._vsd_contactlookup_value === c.contactid)
                    .map(s => this.makePerson(g, s.contactid))[0] || null,
                maxAmount: p.vsd_cpu_estimatedsubtotalcomponentvalue,
                maxAmountMask: p.vsd_cpu_estimatedsubtotalcomponentvalue ? p.vsd_cpu_estimatedsubtotalcomponentvalue.toFixed(2) : "",
                applicationAmount: p.vsd_cpu_subtotalcomponentvalue,
                applicationAmountMask: p.vsd_cpu_subtotalcomponentvalue ? p.vsd_cpu_subtotalcomponentvalue.toString() : "",
                typesOfModels: p.vsd_cpu_programmodeltypes,
                otherModel: p.vsd_otherprogrammodels, //TODO - get this added to API
                evaluation: p.vsd_cpu_programevaluationefforts === 100000001 ? true : p.vsd_cpu_programevaluationefforts === 100000000 ? false : null,
                evaluationDescription: p.vsd_cpu_programevaluationdescription,
                additionalComments: p.vsd_cpu_capprogramoperationscomments,
                additionalStaff: g.ProgramContactCollection
                    .filter((c: iDynamicsCrmContact) => c.vsd_programid === p.vsd_programid)
                    .map(s => this.makePerson(g, s.contactid)) || null,
                removedStaff: [],
            } as iCAPProgram;

            let programType = g.ProgramTypeCollection.find(pt => pt.vsd_programtypeid === p._vsd_programtype_value);
            temp.programLocation = p.vsd_cpu_program_location;
            temp.programTypeName = programType.vsd_name || "";

            programs.push(temp)
        }
        return programs;
    }
    private makePerson(g: iDynamicsScheduleFCAPResponse, personId: string): iPerson {
        return g.StaffCollection
            .filter((p: iDynamicsCrmContact) => p.contactid === personId)
            .map((p: iDynamicsCrmContact): iPerson => {
                return {
                    email: p.emailaddress1 || null,
                    fax: p.fax || null,
                    firstName: p.firstname || null,
                    lastName: p.lastname || null,
                    middleName: p.middlename || null,
                    personId: p.contactid || null,
                    phone: p.mobilephone || null,
                    phoneExtension: p.vsd_mainphoneextension || null,
                    phone2: p.telephone2 || null,
                    phone2Extension: p.vsd_homephoneextension || null,
                    title: p.jobtitle || null,
                    userId: p.vsd_bceid || null,
                    employmentStatus: employmentStatusTypeDict[p.vsd_employmentstatus] || null,
                    address: {
                        line1: p.address1_line1 || null,
                        line2: p.address1_line2 || null,
                        city: p.address1_city || null,
                        postalCode: p.address1_postalcode || null,
                        province: p.address1_stateorprovince || null,
                        country: p.address1_country || 'Canada',
                    },
                }
            })[0];
    }
}
