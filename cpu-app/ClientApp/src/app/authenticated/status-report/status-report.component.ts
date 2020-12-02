import { Component, OnInit, OnDestroy } from '@angular/core';
import { IconStepperService, iStepperElement } from '../../shared/icon-stepper/icon-stepper.service';
import { NotificationQueueService } from '../../core/services/notification-queue.service';
import { Router, ActivatedRoute } from '@angular/router';
import { StatusReportService } from '../../core/services/status-report.service';
import { TransmogrifierStatusReport } from '../../core/models/transmogrifier-status-report.class';
import { convertStatusReportToDynamics } from '../../core/models/converters/status-report-to-dynamics';
import { iDynamicsPostStatusReport } from '../../core/models/dynamics-post';
import { iQuestionCollection } from '../../core/models/question-collection.interface';
import { StateService } from '../../core/services/state.service';
import { FormHelper } from '../../core/form-helper';
import { iQuestion } from '../../core/models/status-report-question.interface';
import * as _ from 'lodash';
import { Transmogrifier } from '../../core/models/transmogrifier.class';

@Component({
  selector: 'app-status-report',
  templateUrl: './status-report.component.html',
  styleUrls: ['./status-report.component.css']
})
export class StatusReportComponent implements OnInit, OnDestroy {
  data: any;
  trans: TransmogrifierStatusReport;
  mainTrans: Transmogrifier;
  stepperElements: iStepperElement[];
  currentStepperElement: iStepperElement;
  stepperIndex: number = 0;
  saving: boolean = false;
  didload: boolean = false;

  public formHelper = new FormHelper();
  constructor(
    private notificationQueueService: NotificationQueueService,
    private route: ActivatedRoute,
    private router: Router,
    private statusReportService: StatusReportService,
    private stateService: StateService,
    private stepperService: IconStepperService,
  ) { }

  ngOnInit() {
    this.didload = false;
    this.mainTrans = this.stateService.main.getValue();
    this.route.params.subscribe(p => {
      const organizationId: string = this.stateService.main.getValue().organizationId;
      const userId: string = this.stateService.main.getValue().userId;
      console.log(p['taskId']);

      this.statusReportService.getStatusReportQuestions(organizationId, userId, p['taskId'])
        .subscribe(r => {
          if (!r.IsSuccess) {
            this.notificationQueueService.addNotification('An attempt at getting this status report was unsuccessful. If this problem persists please notify your ministry contact.', 'danger');
            this.router.navigate(['/authenticated/dashboard']);
          } else {
            this.data = r;
            this.trans = new TransmogrifierStatusReport(r);
            this.trans.taskId = p['taskId'];
            let contract = this.mainTrans.contracts.find(c => c.contractNumber === this.trans.contractNumber);
            let title = (this.trans.reportingPeriod ? this.trans.reportingPeriod : 'Monthly') + 'Status Report';
            if (contract) {
              let thisTask = contract.tasks.find(t => t.taskId === this.trans.taskId);
              if (thisTask) title = thisTask.taskTitle;
            }

            this.trans.title = title;
            this.constructDefaultstepperElements();
          }
        });
    });

    this.stepperService.stepperElements.subscribe(e => this.stepperElements = e);
    this.stepperService.currentStepperElement.subscribe(e => {
      if (this.currentStepperElement) {
        let originalStepper = _.cloneDeep(this.currentStepperElement);
        let formState = this.formHelper.getFormState();
        if (originalStepper.formState === "valid" && formState === "untouched") {
        }
        else if (this.didload) {
          let index = this.stepperElements.findIndex(s => s.id === originalStepper.id);
          if (index >= 0) {
            if (!this.validateCurrentQuestionsAreFilledIn(index)) {
              this.currentStepperElement.formState = "invalid";
            }
            else {
              this.currentStepperElement.formState = "valid";
            }
          }
        }
      }

      this.currentStepperElement = e;

      if (this.currentStepperElement && this.stepperElements) {
        this.stepperIndex = this.stepperElements.findIndex(e => e.id === this.currentStepperElement.id);
      }
    });
  }
  ngOnDestroy() {
    this.didload = false;
  }
  constructDefaultstepperElements() {
    this.stepperService.reset();
    this.trans.statusReportQuestions
      .map((srq: iQuestionCollection): iStepperElement => {
        return {
          itemName: srq.name,
          formState: 'untouched',
          object: null,
          discriminator: null,
        }
      })
      .forEach((f: iStepperElement) => {
        this.stepperService.addStepperElement(f.object, f.itemName, f.formState, f.discriminator);
      });
    this.stepperService.setToFirstStepperElement();
    this.didload = true;
  }
  isCurrentStepperElement(item: iStepperElement): boolean {
    if (item.id === this.currentStepperElement.id) {
      return true;
    }
    return false;
  }
  submit() {
    try {
      this.stepperElements.forEach(s => {
        this.stepperService.setStepperElementProperty(s.id, "formState", "untouched");
      });

      let isValid = true;
      this.trans.statusReportQuestions.forEach((srq: iQuestionCollection) => {
        srq.questions.forEach((q: iQuestion) => {
          if (q.type === 'number' && (q.number === null || q.numberMask === "")) {
            let stepperWithError = this.stepperElements.find(s => s.itemName === srq.name);
            if (stepperWithError) {
              this.stepperService.setStepperElementProperty(stepperWithError.id, "formState", "invalid");
            }
            isValid = false;
          }
          if (q.type === 'boolean' && q.boolean === null) {
            let stepperWithError = this.stepperElements.find(s => s.itemName === srq.name);
            if (stepperWithError) {
              this.stepperService.setStepperElementProperty(stepperWithError.id, "formState", "invalid");
            }
            isValid = false;
          }
          if (q.type === 'string' && q.isChildQuestionExplanationRequired && !q.string) {
            let stepperWithError = this.stepperElements.find(s => s.itemName === srq.name);
            if (stepperWithError) {
              this.stepperService.setStepperElementProperty(stepperWithError.id, "formState", "invalid");
            }
            isValid = false;
          }
        });
      });

      if (!isValid) {
        this.notificationQueueService.addNotification(`Please answer all required questions.`, 'warning');
        return;
      }

      if (confirm('I have confirmed that all of the figures are accurate to the best of my knowledge. I wish to submit these monthly figures for ' + this.trans.reportingPeriod + '.')) {
        this.saving = true;
        const statusReport: iDynamicsPostStatusReport = convertStatusReportToDynamics(this.trans);
        if (!statusReport.AnswerCollection.length) {
          alert('Please ensure that you have filled out the statistics to the best of your ability before attempting to submit.');
          this.saving = false;
          return;
        }

        this.statusReportService.setStatusReportAnswers(this.trans.taskId, statusReport)
          .subscribe(
            r => {
              if (r.IsSuccess) {
                this.saving = false;
                this.notificationQueueService.addNotification(`You have successfully submitted ${this.trans.reportingPeriod} statistics.`, 'success');
                this.stateService.refresh();
                this.router.navigate(['/authenticated/dashboard']);
              }
              else {
                this.saving = false;
                this.notificationQueueService.addNotification('Monthly statistics could not be submitted. If this problem is persisting please contact your ministry representative.', 'danger');
              }
            },
            err => {
              console.log(err);
              this.saving = false;
              this.notificationQueueService.addNotification('Monthly statistics could not be submitted. If this problem is persisting please contact your ministry representative.', 'danger');
            }
          );
      }
    }
    catch (err) {
      console.log(err);
      this.notificationQueueService.addNotification('The monthly statistics could not be saved. If this problem is persisting please contact your ministry representative.', 'danger');
      this.saving = false;
    }
  }
  exit() {
    if (this.formHelper.isFormDirty()) {
      if (confirm("Are you sure you want to return to the dashboard? All unsaved work will be lost.")) {
        this.stateService.refresh();
        this.router.navigate(['/authenticated/dashboard']);
      }
    }
    else {
      this.stateService.refresh();
      this.router.navigate(['/authenticated/dashboard']);
    }
  }

  checkForRequiredChildQuestion(q: iQuestion, questions: iQuestion[]) {
    let childQuestion = questions.find(ques => ques.parent_id === q.uuid);
    if (childQuestion) {
      if (q.boolean) {
        childQuestion.isChildQuestionExplanationRequired = true;
      }
      else {
        childQuestion.isChildQuestionExplanationRequired = false;
      }
    }
  }
  validateCurrentQuestionsAreFilledIn(index = this.stepperIndex) {
    let isValid = true;
    let questionCollection: iQuestionCollection = this.trans.statusReportQuestions[index];
    let questions: iQuestion[] = [];

    if (questionCollection) questions = questionCollection.questions;
    questions.forEach((q: iQuestion) => {
      if (q.type === 'number' && (q.number === null || q.numberMask === "")) {
        let stepperWithError = this.stepperElements[index];
        if (stepperWithError) {
          this.stepperService.setStepperElementProperty(stepperWithError.id, "formState", "invalid");
        }
        isValid = false;
      }
      if (q.type === 'boolean' && q.boolean === null) {
        let stepperWithError = this.stepperElements[index];
        if (stepperWithError) {
          this.stepperService.setStepperElementProperty(stepperWithError.id, "formState", "invalid");
        }
        isValid = false;
      }
      if (q.type === 'string' && q.isChildQuestionExplanationRequired && !q.string) {
        let stepperWithError = this.stepperElements[index];
        if (stepperWithError) {
          this.stepperService.setStepperElementProperty(stepperWithError.id, "formState", "invalid");
        }
        isValid = false;
      }
    });

    return isValid;
  }
  setNextStepper() {
    let originalStepper = _.cloneDeep(this.currentStepperElement);

    if (!this.validateCurrentQuestionsAreFilledIn()) {
      this.stepperService.setStepperElementProperty(originalStepper.id, 'formState', 'invalid');
      this.notificationQueueService.addNotification(`Please answer all required questions.`, 'warning');
      return;
    }
   
    setTimeout(() => {
      this.stepperService.setStepperElementProperty(originalStepper.id, 'formState', 'valid');
    }, 0);

    ++this.stepperIndex;
    this.stepperService.setCurrentStepperElement(this.stepperElements[this.stepperIndex].id);
  }
  setPreviousStepper() {
    --this.stepperIndex;
    this.stepperService.setCurrentStepperElement(this.stepperElements[this.stepperIndex].id);
  }
}
