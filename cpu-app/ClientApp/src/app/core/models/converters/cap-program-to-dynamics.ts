import { iDynamicsPostScheduleFCAP, iDynamicsProgramContactPost, iDynamicsRemoveProgramContactPost, iDynamicsCrmProgramPost } from "../dynamics-post";
import { iPerson } from "../person.interface";
import { nameAssemble } from "../../constants/name-assemble";
import * as _ from "lodash";
import { TransmogrifierCAPApplication } from "../transmogrifier-cap-application.class";
import { iCAPProgram } from "../cap-program.interface";
import { boolOptionSet } from "../../constants/bool-optionset-values";

export function convertCAPProgramToDynamics(trans: TransmogrifierCAPApplication): iDynamicsPostScheduleFCAP {
    const post: iDynamicsPostScheduleFCAP = {
        BusinessBCeID: trans.organizationId,
        UserBCeID: trans.userId,
        ContractCollection: [{
            vsd_ContactLookup1fortunecookiebind: trans.applicantInformation.executiveContact ? trans.applicantInformation.executiveContact.personId : null,
            vsd_contractid: trans.contractId,
            vsd_name: trans.contractNumber,
            vsd_authorizedsigningofficersignature: trans.signature.signature ? trans.signature.signature : null,
            vsd_signingofficersname: trans.signature.signer ? nameAssemble(trans.signature.signer.firstName, trans.signature.signer.middleName, trans.signature.signer.lastName) : null,
            vsd_signingofficertitle: trans.signature.signer ? trans.signature.signer.title : null,
            vsd_cpu_insuranceoptions: trans.applyingForInsurance ? 100000001 : 100000000,
        }],
        Organization: {
            accountid: trans.accountId,
            address1_city: trans.applicantInformation.mailingAddress.city,
            address1_country: trans.applicantInformation.mailingAddress.country,
            address1_line1: trans.applicantInformation.mailingAddress.line1,
            address1_line2: trans.applicantInformation.mailingAddress.line2,
            address1_postalcode: trans.applicantInformation.mailingAddress.postalCode,
            address1_stateorprovince: trans.applicantInformation.mailingAddress.province,
            name: trans.organizationName,
        },
    };

    const programContactCollection: iDynamicsProgramContactPost[] = [];
    const removeProgramContactCollection: iDynamicsRemoveProgramContactPost[] = [];
    trans.capPrograms.forEach((program: iCAPProgram) => {
        // in each program add the list of staff by their id
        program.additionalStaff.forEach((s: iPerson): void => {
            const contact: iDynamicsProgramContactPost = {
                contactid: s.personId,
                vsd_programid: program.programId,
            };
            programContactCollection.push(contact);
        });

        program.removedStaff.forEach((s: iPerson): void => {
            const contact: iDynamicsRemoveProgramContactPost = {
                contactid: s.personId,
                vsd_programid: program.programId,
            };
            removeProgramContactCollection.push(contact);
        });
    });
    if (programContactCollection.length) post.AddProgramContactCollection = programContactCollection;
    if (removeProgramContactCollection.length) post.RemoveProgramContactCollection = removeProgramContactCollection;

    const programCollection: iDynamicsCrmProgramPost[] = [];
    trans.capPrograms.forEach((program: iCAPProgram) => {
        programCollection.push({
            vsd_ContactLookupfortunecookiebind: program.programContact ? program.programContact.personId : null,
            vsd_programid: program.programId,
            vsd_cpu_subtotalcomponentvalue: parseFloat(program.applicationAmount ? program.applicationAmount.toString() : "0"),
            vsd_cpu_programmodeltypes: program.typesOfModels,
            vsd_otherprogrammodels: program.otherModel,
            vsd_cpu_programevaluationefforts: program.evaluation ? boolOptionSet.isTrue : boolOptionSet.isFalse,
            vsd_cpu_programevaluationdescription: program.evaluationDescription,
            vsd_cpu_capprogramoperationscomments: program.additionalComments,
        });
        if (programCollection.length) post.ProgramCollection = programCollection;
    });
    return post;
}