import { Component, OnInit } from '@angular/core';
import { FormHelper } from '../../core/form-helper';
import { EMAIL, PHONE_NUMBER, LETTERS_SPACES, NAME_REGEX } from '../../core/constants/regex.constants';
import { StateService } from '../../core/services/state.service';
import { Router, ActivatedRoute } from '@angular/router';
import { TransmogrifierNewUser } from '../../core/models/converters/transmogrifier-new-user.class';
import { convertNewUserToDynamics } from '../../core/models/converters/new-user-to-dynamics';
import { NewUserService } from '../../core/services/new-user.service';
import { NotificationQueueService } from '../../core/services/notification-queue.service';

@Component({
  selector: 'app-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.css']
})
export class NewUserComponent implements OnInit {

  emailRegex: RegExp = EMAIL;
  phoneRegex: RegExp = PHONE_NUMBER;
  wordRegex: RegExp = LETTERS_SPACES;
  nameRegex: RegExp = NAME_REGEX;
  organizationName: string;
  saving: boolean = false;
  trans: TransmogrifierNewUser = new TransmogrifierNewUser();
  public formHelper = new FormHelper();
  constructor(private stateService: StateService,
    private newUserService: NewUserService,
    private router: Router,
    private notificationQueueService: NotificationQueueService
  ) { }

  ngOnInit() {

  }

  registerNewUser() {
    try {
      let userSettings = this.stateService.userSettings.getValue();
      this.trans.organizationId = userSettings.accountId;
      this.trans.userId = userSettings.userId;

      if (!this.trans.hasRequiredFields()) {
        this.notificationQueueService.addNotification('Please fill in required fields.', 'warning');
        return;
      }

      let data = convertNewUserToDynamics(this.trans);
      this.newUserService.saveNewUser(data).subscribe((res) => {
        if (res.IsSuccess) {
          this.notificationQueueService.addNotification(`You have successfully registered a new user.`, 'success');
          this.saving = false;
          setTimeout(() => {
            this.stateService.logout();
          }, 4000);
        }
        else {
          this.notificationQueueService.addNotification('The new user could not be saved. If this problem is persisting please contact your ministry representative.', 'danger');
          this.saving = false;
        }
      },
        (err) => {
          console.log(err);
          this.notificationQueueService.addNotification('The new user could not be saved. If this problem is persisting please contact your ministry representative.', 'danger');
          this.saving = false;
        });
    }
    catch (err) {
      console.log(err);
      // console.log("some error happened...");
    }
  }
  exit() {
    if (this.formHelper.isFormDirty() && confirm("Are you sure you want to exit?")) {
      this.router.navigate(['']);
    }
    else {
      this.router.navigate(['']);
    }
  }
}
