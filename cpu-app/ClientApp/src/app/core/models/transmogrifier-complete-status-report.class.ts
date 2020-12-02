import { iDynamicsMonthlyStatisticsQuestions, iDynamicsMonthlyStatisticsAnswers, iDynamicsMonthlyStatisticsAnswersAnswer } from "./dynamics-blob";
import { months } from "../constants/month-codes";
import { boolOptionSet } from "../constants/bool-optionset-values";
import { iAnswerCollection } from "./answer-collection.interface";
import * as _ from 'lodash';
import { iAnswer } from "./status-report-answer.interface";
// a collection of the expense item guids as K/V pairs for generating line items
export class TransmogrifierCompleteStatusReport {
    public organizationId: string;
    public organizationName: string;
    public taskId: string;
    public title: string;
    public userId: string;
    public programId: string;
    public programName: string;
    public programType: string;
    //   public contractedHours: number;
    public contractNumber: string;
    public reportingPeriod: string;
    public statusReportAnswers: iAnswerCollection[] = []; // this is a collection of objects

    constructor(g: iDynamicsMonthlyStatisticsQuestions) {
        this.userId = g.Userbceid;// this is the user's bceid
        this.organizationId = g.Businessbceid;// this is the organization's bceid
        this.organizationName = g.Organization.name;
        this.programId = g.Program.vsd_programid;
        this.reportingPeriod = Object.keys(months).find(key => months[key] === g.ReportingPeriod);
        this.programName = g.Program.vsd_name;
        this.contractNumber = g.Contract.vsd_name;

        this.buildStatusReport(g);
    }
    private buildStatusReport(g: iDynamicsMonthlyStatisticsAnswers): void {
        let categoryGroups: any = _.groupBy(g.AnswerCollection, 'vsd_questioncategory');

        for (let category in categoryGroups) {
            const q: iAnswerCollection = {
                name: category,
                answers: categoryGroups[category].map((d: iDynamicsMonthlyStatisticsAnswersAnswer): iAnswer => {
                    const type = this.fieldType(d.vsd_questiontype1);
                    const a: iAnswer = {
                        label: d.vsd_name,
                        type: type,
                        uuid: d.vsd_datacollectionlineitemid,
                        questionNumber: d.vsd_questionorder,
                        categoryName: d.vsd_questioncategory,
                        // multiChoiceAnswers: this.getMultipleChoice(d.vsd_cpustatisticsmasterdataid, g.MultipleChoiceCollection),
                        isChildQuestionExplanationRequired: false,
                        number: d.vsd_number || null,
                        numberMask: d.vsd_number ? d.vsd_number.toString() : null,
                        boolean: d.vsd_yesno === boolOptionSet.isTrue ? true : d.vsd_yesno === boolOptionSet.isFalse ? false : null,
                        string: d.vsd_textanswer || null,
                    };

                    return a;
                })
            }
            this.statusReportAnswers.push(q);
        }

        g.CategoryCollection.sort(function (a, b) {
            return a.vsd_categoryorder - b.vsd_categoryorder;
        });
        this.statusReportAnswers.sort(function (a, b) {
            return g.CategoryCollection.findIndex(c => c.vsd_name === a.name) - g.CategoryCollection.findIndex(c => c.vsd_name === b.name);
        });
    }

    private fieldType(d: number): string {
        // convert the field type into a string
        let type: string;
        switch (d) {
            case (100000000): {
                type = 'number';
                break;
            }
            case (100000001): {
                type = 'boolean';
                break;
            }
            case (100000002): {
                type = 'string';
                break;
            }
            default: {
                type = undefined;
                break;
            }
        }
        return type;
    }
}

