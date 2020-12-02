import { iMultipleChoice } from "./status-report-question.interface";

export interface iAnswer {
    // whatever value we care about
    boolean?: boolean;
    number?: number;
    numberMask?: string;
    string?: string;
  
    // Human readable name
    label: string;
    // Discriminator for type of question
    type: string; // These match JSON types: boolean string number
    uuid: string; // this is used to unique-ify different fields so that html labels can be procedurally generated
    questionNumber?: number; // the number that appears beside the question.
  
    categoryName?: string; // _vsd_categoryid_value field - for determining if this is a drop down box
    multiChoiceAnswers?: iMultipleChoice[];
    parent_id?: string;
    isChildQuestionExplanationRequired?: boolean;
  }  