import { iAddress } from "./address.interface";
import { iCAPProgram } from "./cap-program.interface";
import { iPerson } from "./person.interface";

export class CAPProgram implements iCAPProgram {
    contractId: string;
    formState: string;
    name: string;
    programTypeName: string;
    programId: string;
    programLocation: string;
    maxAmount: number;
    maxAmountMask: string;
    applicationAmount: number;
    applicationAmountMask: string;
    typesOfModels: string;
    otherModel: string;
    evaluation: boolean;
    evaluationDescription: string;
    additionalComments: string;

    additionalStaff: iPerson[];
    removedStaff: iPerson[];
    programContact: iPerson;

    constructor(program?: iCAPProgram) {
        if (program) Object.assign(this, program);
    }

    private REQUIRED_FIELDS = ["applicationAmount",];
    hasRequiredFields() {
        for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
            if (!this.fetchFromObject(this, this.REQUIRED_FIELDS[i])) {
                return false;
            }
        }
        return true;
    }

    getMissingFields() {
        let ret = [];
        for (let i = 0; i < this.REQUIRED_FIELDS.length; ++i) {
            if (!this.fetchFromObject(this, this.REQUIRED_FIELDS[i])) {
                ret.push(this.REQUIRED_FIELDS[i]);
            }
        }
        return ret;
    }

    private fetchFromObject(obj, prop) {
        if (typeof obj === 'undefined') {
            return false;
        }

        var _index = prop.indexOf('.')
        if (_index > -1) {
            return this.fetchFromObject(obj[prop.substring(0, _index)], prop.substr(_index + 1));
        }

        return obj[prop];
    }
}