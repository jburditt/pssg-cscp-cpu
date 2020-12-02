import { iUserSettings, Roles } from "./user-settings.interface";

export class UserSettings implements iUserSettings {
  userAuthenticated: boolean;
  userId: string;
  siteMinderGuid: string;
  siteMinderBusinessGuid: string;
  userDisplayName: string;
  businessLegalName: string;
  userType: string;
  user: any;
  isNewUserRegistration: boolean;
  isNewUserAndNewOrganizationRegistration: boolean;
  contactExistsButNotApproved: boolean;
  contactId: string;
  accountId: string;
  authenticatedUser: any;
  userRole: Roles;
  constructor(user?: iUserSettings) {
    if (user) {
      Object.assign(this, user);
      // this.userAuthenticated = user.userAuthenticated || false;
      // this.userId = user.userId || "";
      // this.siteMinderGuid = user.siteMinderGuid || null;
      // this.siteMinderBusinessGuid = user.siteMinderBusinessGuid || null;
      // this.userDisplayName = user.userDisplayName || null;
      // this.businessLegalName = user.businessLegalName || null;
      // this.userType = user.userType || null;
      // this.user = user.user || null;
      // this.isNewUserRegistration = user.isNewUserRegistration || false;
      // this.isNewUserAndNewOrganizationRegistration = user.isNewUserAndNewOrganizationRegistration || false;
      // this.contactExistsButNotApproved = user.contactExistsButNotApproved || false;
      // this.contactId = user.contactId || null;
      // this.accountId = user.accountId || null;
      // this.authenticatedUser = user.authenticatedUser || null;
      // this.userRole = user.userRole || Roles.ProgramStaff;
    }
  }
}
