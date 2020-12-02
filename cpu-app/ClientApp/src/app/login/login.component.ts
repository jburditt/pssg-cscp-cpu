import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserDataService } from '../core/services/user-data.service';
import { StateService } from '../core/services/state.service';
import { NotificationQueueService } from '../core/services/notification-queue.service';
import { UserSettings } from '../core/models/user-settings.class';

@Component({
  selector: 'app-landing-page',
  templateUrl: './login.component.html'
})
export class LoginPageComponent implements OnInit {
  loading: boolean = true;
  constructor(private router: Router,
    private userData: UserDataService,
    private notificationQueueService: NotificationQueueService,
    private stateService: StateService) {

    this.userData.getCurrentUser().subscribe((userInfo: UserSettings) => {
      if (userInfo && userInfo.userId && userInfo.accountId) {
        this.stateService.loggedIn.next(true);
        this.stateService.userSettings.next(userInfo);
        // console.log(userInfo);
        if (userInfo.isNewUserRegistration) {
          // this.notificationQueueService.addNotification(`New User Detected.`, 'success');
          this.router.navigate(['/authenticated/new_user']);
        }
        else if (userInfo.isNewUserAndNewOrganizationRegistration) {
          // this.notificationQueueService.addNotification(`New User Detected.`, 'success');
          this.router.navigate(['/authenticated/new_user_new_organization']);
        }
        else if (userInfo.contactExistsButNotApproved) {
          this.notificationQueueService.addNotification(`User is not approved for portal access. Please contact an administrator.`, 'danger');
          setTimeout(() => {
            this.stateService.logout();
          }, 1200);
        }
        else {
          this.stateService.login();
        }
      }
      else {
        this.notificationQueueService.addNotification(`No associated CRM account. Please contact an administrator.`, 'warning');
        // this.stateService.loggedIn.next(false);
        // this.stateService.logout();
        this.router.navigate([this.stateService.homeRoute.getValue()]);
      }
    });
  }

  ngOnInit() {
  }
}
