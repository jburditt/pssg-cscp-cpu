import { iTask } from "./task.interface";
import { iProgram } from "./program.interface";
import { iMessage } from "./message.interface";

export interface iContract {
  contractId: string;
  contractNumber: string;
  status: string;
  programs: iProgram[];
  tasks: iTask[];
  completedTasks: iTask[];
  fiscalYearStart?: number;
  //messages: iMessage[]; // TODO: Will need to add this to the model
}
