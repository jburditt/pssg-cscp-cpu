import { iDynamicsSurplusPlanLineItem } from "../dynamics-blob";
import { iDynamicsPostSurplusPlan } from "../dynamics-post";
import { TransmogrifierProgramSurplus } from "../transmogrifier-program-surplus.class";

export enum SurplusTypes {
    Plan,
    Report,
}

export function convertProgramSurplusToDynamics(trans: TransmogrifierProgramSurplus, type: SurplusTypes, isSubmit: boolean = false): iDynamicsPostSurplusPlan {
    let ret: iDynamicsPostSurplusPlan = {
        BusinessBCeID: trans.organizationId,
        UserBCeID: trans.userId,
        SurplusPlanCollection: [{
            vsd_surplusplanreportid: trans.surplusPlanId,
            vsd_surplusremittance: trans.pay_with_cheque
        }],
        SurplusPlanLineItemCollection: []
    };

    if (isSubmit) {
        ret.SurplusPlanCollection[0].vsd_datesubmitted = new Date();
    }

    trans.lineItems.forEach(item => {
        let dynamicsItem: iDynamicsSurplusPlanLineItem = {
            vsd_surpluslineitemid: item.id,
            vsd_surplusplanid: item.surplus_plan_id,
            vsd_allocatedamount: item.allocated_amount,
            // vsd_justificationdetails: item.justification,
            // vsd_proposedexpenditures: item.proposed_amount,
        };

        if (type === SurplusTypes.Plan) {
            dynamicsItem.vsd_justificationdetails = item.justification;
            dynamicsItem.vsd_proposedexpenditures = item.proposed_amount;
        }
        if (type === SurplusTypes.Report) {
            dynamicsItem.vsd_actualexpenditures = item.expenditures_q1;
            dynamicsItem.vsd_actualexpenditures2 = item.expenditures_q2;
            dynamicsItem.vsd_actualexpenditures3 = item.expenditures_q3;
            dynamicsItem.vsd_actualexpenditures4 = item.expenditures_q4;
        }
        if (isSubmit) {
            dynamicsItem.vsd_datesubmitted = new Date();
        }
        ret.SurplusPlanLineItemCollection.push(dynamicsItem);
    });

    return ret;
}