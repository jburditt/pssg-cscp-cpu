export interface iUserSettings {
  userAuthenticated: boolean;
  userId: string;
  siteMinderGuid: string;
  siteMinderBusinessGuid: string;
  userDisplayName: string;
  businessLegalName: string;
  userType: string;
  user?: any;
  isNewUserRegistration: boolean;
  isNewUserAndNewOrganizationRegistration: boolean;
  contactExistsButNotApproved: boolean;
  noRolesAssigned: boolean;
  contactId: string;
  accountId: string;
  authenticatedUser: any;
  userRole: Roles;
}

export enum Roles {
  ProgramStaff,
  BoardContact,
  ExecutiveContact,
}
