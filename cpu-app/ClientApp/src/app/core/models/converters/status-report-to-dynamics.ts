import { TransmogrifierStatusReport } from "../transmogrifier-status-report.class";
import { iDynamicsPostStatusReport, iDynamicsAnswer } from "../dynamics-post";
import { iQuestionCollection } from "../question-collection.interface";
import { iQuestion } from "../status-report-question.interface";
import { months as monthDict } from "../../constants/month-codes";
import { boolOptionSet } from "../../constants/bool-optionset-values";

export function convertStatusReportToDynamics(trans: TransmogrifierStatusReport): iDynamicsPostStatusReport {
  const types = {
    'number': 100000000,
    'boolean': 100000001,
    'string': 100000002,
  };
  // build the answers into a flatter dynamics form
  const answers: iDynamicsAnswer[] = [];

  trans.statusReportQuestions.forEach((srq: iQuestionCollection) => {
    // for each question assemble shared elements
    srq.questions.forEach((q: iQuestion) => {
      const lineItem: iDynamicsAnswer = {
        vsd_name: q.label,
        vsd_questioncategory: srq.name,
        vsd_QuestionIdfortunecookiebind: q.uuid,
        vsd_CategoryIdfortunecookiebind: q.categoryID,
        vsd_questionorder: Math.floor(q.questionNumber),
        vsd_questiontype1: types[q.type]
      }
      // depending on types we add another property
      if (q.type === 'number') lineItem['vsd_number'] = q.number || 0;
      if (q.type === 'boolean') lineItem['vsd_yesno'] = q.boolean ? boolOptionSet.isTrue : boolOptionSet.isFalse;
      if (q.type === 'string') lineItem['vsd_textanswer'] = q.string || "";
      // add the line item to the answers list
      answers.push(lineItem);
    });
  });

  return {
    BusinessBCeID: trans.organizationId,
    UserBCeID: trans.userId,
    // ReportingPeriod: monthDict[trans.reportingPeriod] || 0,
    // get rid of any answers are missing a value. Otherwise dynamics 204's.
    AnswerCollection: answers,//.filter(v => v.vsd_yesno || v.vsd_textanswer || v.vsd_number)
  };
}
