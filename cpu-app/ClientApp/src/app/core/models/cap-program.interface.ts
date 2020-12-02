import { iPerson } from "./person.interface";

export interface iCAPProgram {
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
}
