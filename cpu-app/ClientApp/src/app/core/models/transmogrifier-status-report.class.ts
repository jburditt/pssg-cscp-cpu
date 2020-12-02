import { iDynamicsMonthlyStatisticsQuestions, iDynamicsMonthlyStatisticsQuestionsQuestion, iDynamicsMonthlyStatisticsQuestionsMcQuestion, iDynamicsMonthlyStatisticsChildQuestion } from "./dynamics-blob";
import { iQuestion, iMultipleChoice } from "./status-report-question.interface"
import { iQuestionCollection } from "./question-collection.interface";
import { iDynamicsPostStatusReport } from "./dynamics-post";
import { iContract } from "./contract.interface";
import { months } from "../constants/month-codes";
import { boolOptionSet } from "../constants/bool-optionset-values";
// a collection of the expense item guids as K/V pairs for generating line items
export class TransmogrifierStatusReport {
  public organizationId: string;
  public organizationName: string;
  public taskId: string;
  public title: string;
  public userId: string;
  public programId: string;
  public programName: string;
  public programType: string;
  public contractedHours: number;
  public contractNumber: string;
  public reportingPeriod: string;
  public statusReportQuestions: iQuestionCollection[] = []; // this is a collection of objects

  constructor(g: iDynamicsMonthlyStatisticsQuestions) {
    this.userId = g.Userbceid;// this is the user's bceid
    this.organizationId = g.Businessbceid;// this is the organization's bceid
    this.organizationName = g.Organization.name;
    this.programId = g.Program.vsd_programid;
    this.reportingPeriod = Object.keys(months).find(key => months[key] === g.ReportingPeriod);
    this.programType = g.ProgramTypeCollection ? g.ProgramTypeCollection.filter(f => g.Program._vsd_programtype_value === f.vsd_programtypeid).map(f => f.vsd_name)[0] : null;
    this.programName = g.Program.vsd_name;
    this.contractNumber = g.Contract.vsd_name;
    this.contractedHours = g.Program.vsd_cpu_numberofhours;

    this.buildStatusReport(g);
  }
  private buildStatusReport(g: iDynamicsMonthlyStatisticsQuestions): void {
    g.CategoryCollection.sort(function (a, b) {
      return a.vsd_categoryorder - b.vsd_categoryorder;
    });
    // for every category of questions collect the matching items
    for (let category of g.CategoryCollection) {
      const q: iQuestionCollection = {
        name: category.vsd_name,
        questions: g.QuestionCollection
          .filter((q: iDynamicsMonthlyStatisticsQuestionsQuestion) => category.vsd_monthlystatisticscategoryid === q._vsd_categoryid_value)
          .map((d: iDynamicsMonthlyStatisticsQuestionsQuestion): iQuestion => {
            // look up the value once
            const type = this.fieldType(d.vsd_questiontype);
            const q: iQuestion = {
              label: d.vsd_name,
              type,
              uuid: d.vsd_cpustatisticsmasterdataid,
              questionNumber: d.vsd_questionorder,
              categoryID: d._vsd_categoryid_value,
              multiChoiceAnswers: this.getMultipleChoice(d.vsd_cpustatisticsmasterdataid, g.MultipleChoiceCollection),
              isChildQuestionExplanationRequired: false,
              number: d.vsd_number || null,
              numberMask: d.vsd_number ? d.vsd_number.toString() : null,
              boolean: d.vsd_yesno === boolOptionSet.isTrue ? true : d.vsd_yesno === boolOptionSet.isFalse ? false : null,
              string: d.vsd_textanswer || null,
            }
            // instantiate the correct property with the freshest null value
            q[type] = null;
            // return the object
            return q;
          })
      };

      let childQuestions: iQuestion[] = g.ChildQuestionCollection.filter((q: iDynamicsMonthlyStatisticsChildQuestion) => category.vsd_monthlystatisticscategoryid === q._vsd_categoryid_value)
        .map((d: iDynamicsMonthlyStatisticsChildQuestion): iQuestion => {
          // look up the value once
          const type = this.fieldType(d.vsd_questiontype);
          const q: iQuestion = {
            label: d.vsd_name,
            type,
            uuid: d.vsd_cpustatisticsmasterdataid, // I was generating it but may as well use the one from master data.
            questionNumber: d.vsd_questionorder + 0.5, //child questions are being given the same number for order as the parent question, so add 0.5 to push this after the parent question and remain before the next question
            categoryID: d._vsd_categoryid_value,
            multiChoiceAnswers: this.getMultipleChoice(d.vsd_cpustatisticsmasterdataid, g.MultipleChoiceCollection),
            parent_id: d._vsd_parentid_value,
            isChildQuestionExplanationRequired: false,
          }
          // instantiate the correct property with the freshest null value
          q[type] = null;
          // return the object
          return q;
        });

      q.questions = q.questions.concat(childQuestions).sort(function (a, b) {
        return a.questionNumber > b.questionNumber ? 1 : -1;
      });

      // push the status report questions
      this.statusReportQuestions.push(q);
    }
  }

  private findParentId(question_id: string, childQuestions: iDynamicsMonthlyStatisticsChildQuestion[]) {
    let parent_id = "";
    let thisQuestion = childQuestions.find(q => q.vsd_cpustatisticsmasterdataid === question_id);
    if (thisQuestion) {
      parent_id = thisQuestion._vsd_parentid_value;
    }
    return parent_id;
  }

  private getMultipleChoice(id: string, questionCollection: iDynamicsMonthlyStatisticsQuestionsMcQuestion[]): iMultipleChoice[] {
    // Get multiple choice options for this question - returns only ones related to this question
    let tempQuestionCollection: iMultipleChoice[] = [];
    for (let mcQuestion of questionCollection) {
      const mc: iMultipleChoice = {
        label: mcQuestion.vsd_name,
        masterDataID: mcQuestion.vsd_cpustatisticsmasterdataanswerid,
        uuid: mcQuestion._vsd_questionid_value,
      }
      if (mc.uuid == id) {
        tempQuestionCollection.push(mc);
      }
    }
    if (tempQuestionCollection.length > 0) {
      return tempQuestionCollection;
    }
    else {
      return;
    }
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

