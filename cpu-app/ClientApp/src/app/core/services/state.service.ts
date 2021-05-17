import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { Transmogrifier } from '../models/transmogrifier.class';
import { MainService } from './main.service';
import { NotificationQueueService } from './notification-queue.service';
import { iDynamicsBlob } from '../models/dynamics-blob';
import { iPerson } from '../models/person.interface';
import { UserDataService } from './user-data.service';
import { Router } from '@angular/router';
import { iUserSettings, Roles } from '../models/user-settings.interface';
import { UserSettings } from '../models/user-settings.class';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StateService {
  // these are observable states for the application load these in at login time.
  // main is the whole blob of data from the get request
  public main: BehaviorSubject<Transmogrifier> = new BehaviorSubject(null);
  // the user as an object
  public currentUser: BehaviorSubject<iPerson> = new BehaviorSubject<iPerson>(null);
  // global state of the login
  public loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public newUser: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public homeRoute: BehaviorSubject<string> = new BehaviorSubject<string>('');
  public loading: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public userSettings: BehaviorSubject<iUserSettings> = new BehaviorSubject<iUserSettings>(new UserSettings());

  // public userId: string = "";
  // public orgId: string = "";
  // public loggedIn: boolean = false;
  // public isNewAccount: boolean = false;

  constructor(
    private mainService: MainService,
    private notificationQueueService: NotificationQueueService,
    private userData: UserDataService,
    private router: Router,
    private http: HttpClient,
  ) { }

  login() {
    let userId = "";// = 'FB55AB99F20E471186B8143B3F21F6E7';
    let orgId = "";// = 'E4637B1557A6457891D7549067B20635';

    if (window.location.href.includes("localhost")) {
      //Victimservices
      userId = 'D7742187EC2347378704A200273F87D9';
      orgId = '8859118D4FB54A74AEDFC4CD368C36B1';

      //Victimservices1
      // userId = 'FB55AB99F20E471186B8143B3F21F6E7';
      // orgId = 'E4637B1557A6457891D7549067B20635';

      //Victimservices2
      // userId = 'C0FD151410544705A39FAF2A5504D4E7';
      // orgId = 'D25E31FFC5AA4C3A9B7557CED3A5DDA5';

      //Victimservices4
      // userId = '136339471ABD4770A82227AF6AD2C01C';
      // orgId = '53AFBD4EB74B4156BAC2042593FBC5F1';

      //Victimservices18
      // userId = '968458E00053498798D0D8D815A2DCB8';
      // orgId = '7B0C03E9C4C84068865655EE8EDD7DA3';

      let settings = new UserSettings();
      settings.userId = userId;
      settings.accountId = orgId;
      // settings.userRole = Roles.ServiceProvider;
      // settings.userRole = Roles.ProgramStaff;
      this.userSettings.next(settings);
    }

    // console.log("logging in");
    let userInfo = this.userSettings.getValue();
    // console.log(this.loggedIn.getValue(), userInfo.userId, userInfo.accountId);


    if (this.loggedIn.getValue() && !userInfo.isNewUserRegistration) {
      //in this case we have id's from siteminder login
      userId = userInfo.userId;
      orgId = userInfo.accountId;
    }

    if (!userId || !orgId) {
      this.notificationQueueService.addNotification('Your user BCeID does not match one in our records.', 'danger');
      return;
    }

    this.loading.next(true);
    // on login collect the information from the organization id
    this.mainService.getBlob(userId, orgId).subscribe(
      (m: iDynamicsBlob) => {
        if (!m || !m.Result) {
          this.notificationQueueService.addNotification('Error getting result from COAST', 'danger');
          this.loading.next(false);
          return;
        }

        // check for actual error message
        if (m.Result.includes('BusinessBCeID doesn\'t match to which the Contact belongs to')) {
          this.notificationQueueService.addNotification('Your organization\'s BCeID does not match one in our records.', 'danger');
          this.currentUser.next({
            userId,
            orgId,
            firstName: 'New',
            lastName: 'User',
            email: ''
          });
          // set the logged in state
          this.homeRoute.next('authenticated/new_user');
          this.loggedIn.next(true);
        } else if (m.Result.includes('No contact found with the supplied BCeID')) {
          this.notificationQueueService.addNotification('Your user BCeID does not match one in our records.', 'danger');
          this.currentUser.next({
            userId,
            orgId,
            firstName: 'New',
            lastName: 'User',
            email: ''
          });
          // set the logged in state
          this.homeRoute.next('authenticated/new_user');
          this.loggedIn.next(true);
        } else if (m.Result.includes('Error: Principal user')) {
          console.log(m);
          console.log("Did you update user secrets??");
          this.notificationQueueService.addNotification('User does not have required privileges to access portal.', 'danger');
          this.loading.next(false);
          return;
        }
        else if (m.Result.includes('No roles assigned to the contact')) {
          console.log(m);
          console.log("Need to assign roles in CRM");
          this.notificationQueueService.addNotification('User does not have any roles assigned.', 'danger');
          this.loading.next(false);
          return;
        }
        else {
          // console.log("settings");
          // console.log(this.userSettings.getValue());

          // collect the blob into a useful object
          console.log("Dynamics blob");
          console.log(JSON.parse(JSON.stringify(m)));
          const mainData = new Transmogrifier(m);

          let updatedSettings = new UserSettings(userInfo);
          updatedSettings.userRole = mainData.role;

          if (window.location.href.includes("localhost")) {
            // updatedSettings.userRole = Roles.ProgramStaff;
            console.log("setting localhost role to ExecutiveContact");
            updatedSettings.userRole = Roles.ExecutiveContact;
          }
          this.userSettings.next(updatedSettings);

          // save the useful blob of viewmodels
          this.main.next(mainData);
          // save the user that matches the current bceid
          this.currentUser.next(mainData.persons.filter(p => p.userId === userId)[0]);
          // give a notification
          this.notificationQueueService.addNotification(`${mainData.organizationName} has been logged in successfully.`, 'success');

          // set the home button link and set log in to true (IN THAT ORDER)
          this.homeRoute.next('authenticated/dashboard');
          this.router.navigate(['/authenticated/dashboard']);
          this.loggedIn.next(true);
        }
      },
      err => { },
      () => this.loading.next(false)
    );
  }
  logout() {
    // clear the state and route to the homepage
    if (window.location.href.includes("localhost")) {
      this.main.next(null);
      this.currentUser.next(null);
      this.userSettings.next(new UserSettings);
      //notification about the login
      // this.notificationQueueService.addNotification('User has logged out.', 'warning');

      // set the home button link and set logout to false (IN THAT ORDER)
      this.homeRoute.next('');
      this.loggedIn.next(false);
    }
    else {
      this.userData.getLogoutUrl().subscribe((data: any) => {
        this.main.next(null);
        this.currentUser.next(null);
        this.userSettings.next(new UserSettings);
        //notification about the login
        // this.notificationQueueService.addNotification('User has logged out.', 'warning');

        // set the home button link and set logout to false (IN THAT ORDER)
        // this.homeRoute.next('');
        this.loggedIn.next(false);
        window.location.href = data.logoutUrl;
      });
    }
  }
  refresh() {
    // quick refresh of data
    const userId = this.main.getValue().userId;
    const organizationId = this.main.getValue().organizationId;
    // only perform this get blob if the required information has been returned at least once
    // we need the user and organization id to do this
    if (userId && organizationId) {
      this.mainService.getBlob(userId, organizationId).subscribe(
        (m: iDynamicsBlob) => {
          if (!m.IsSuccess) {
            this.notificationQueueService.addNotification('There was a problem loading dashboard data. If this problem is persisting please contact your ministry representative.', 'danger');
          }
          else {
            // collect the blob into a useful object
            const mainData = new Transmogrifier(m);
            // save the useful blob in a behaviourSubject
            this.main.next(mainData);
          }
        }
      );
    }
  }
  getUserName() {
    let userId = "";// = 'FB55AB99F20E471186B8143B3F21F6E7';
    let orgId = "";// = 'E4637B1557A6457891D7549067B20635';

    if (window.location.href.includes("localhost")) {
      userId = 'FB55AB99F20E471186B8143B3F21F6E7';
      orgId = 'E4637B1557A6457891D7549067B20635';
    }

    // console.log("lookup logged in user name");
    let userInfo = this.userSettings.getValue();
    // console.log(this.loggedIn.getValue(), userInfo.userId, userInfo.accountId);


    if (this.loggedIn.getValue()) {
      //in this case we have id's from siteminder login
      userId = userInfo.userId;
      orgId = userInfo.accountId;
    }

    if (!userId || !orgId) {
      return
    }

    // this.loading.next(true);
    // on login collect the information from the organization id
    this.mainService.getBlob(userId, orgId).subscribe(
      (m: iDynamicsBlob) => {
        // check for actual error message
        if (m.Result.includes('BusinessBCeID doesn\'t match to which the Contact belongs to')) {
          // console.log("BusinessBCeID doesn't match");
          let firstName = "New";
          let lastName = "User";
          let userSettings = this.userSettings.getValue();
          if (userSettings.userDisplayName) {
            let userDisplayName = this.userSettings.getValue().userDisplayName
            let nameParts = userDisplayName.split(' ');
            if (nameParts.length > 0) {
              firstName = nameParts.splice(0, 1)[0];
              lastName = nameParts.join(' ');
            }
          }

          this.currentUser.next({
            userId,
            orgId,
            firstName: firstName,
            lastName: lastName,
            email: ''
          });

        } else if (m.Result.includes('No contact found with the supplied BCeID')) {
          // console.log("BCeID doesn't match");
          let firstName = "New";
          let lastName = "User";
          let userSettings = this.userSettings.getValue();
          if (userSettings.userDisplayName) {
            let userDisplayName = this.userSettings.getValue().userDisplayName
            let nameParts = userDisplayName.split(' ');
            if (nameParts.length > 0) {
              firstName = nameParts.splice(0, 1)[0];
              lastName = nameParts.join(' ');
            }
          }

          this.currentUser.next({
            userId,
            orgId,
            firstName: firstName,
            lastName: lastName,
            email: ''
          });

        } else {
          if (!m.IsSuccess) {
            this.notificationQueueService.addNotification('There was a problem loading dashboard data. If this problem is persisting please contact your ministry representative.', 'danger');
          }
          else {
            const mainData = new Transmogrifier(m);
            // console.log("we did get some data");
            // console.log(mainData);
            this.currentUser.next(mainData.persons.filter(p => p.userId === userId)[0]);
          }
        }
      },
      err => { },
      () => this.loading.next(false)
    );
  }
}
