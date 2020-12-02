export interface iTask {
  status: string;
  isCompleted: boolean;
  taskName: string;
  taskTitle: string;
  taskDescription: string;
  deadline: Date;
  isOverDue?: boolean;
  submittedDate: Date;
  taskId: string;
  formType: string;
}
