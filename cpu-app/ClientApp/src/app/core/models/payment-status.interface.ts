export interface iPaymentStatus {
    Q1: string;
    Q2: string;
    Q3: string;
    Q4: string;
    oneTime: string;
}

export enum CRMPaymentStatusCode {
    Draft = 1,
    Submitted = 100000000,
    Exception = 100000001,
    Cancel_Submission = 100000006,
    Resubmit = 100000007,
    On_Hold = 100000008,
    Paid = 2,
    Not_Applicable = 0,
}

export const PaymentStatusDisplay = {
    1: "Pending",
    100000001: "Pending",
    100000007: "Pending",
    100000000: "Processing",
    100000006: "Not Applicable",
    100000008: "On Hold",
    2: "Paid"
}