import { iDynamicsSurplusPlan, iDynamicsSurplusPlanLineItem } from "./dynamics-blob";

export interface iDynamicsPostScheduleG { //no controller setup to receive this yet
  UserBCeID: string;
  BusinessBCeID: string;
  ScheduleGCollection: iDynamicsScheduleGPost[];
  ScheduleGLineItemCollection: iDynamicsScheduleGLineItemPost[];
}
export interface iDynamicsPostOrg { //maps to OrganizationPost
  UserBCeID: string;
  BusinessBCeID: string;
  Organization: iDynamicsOrganizationPost;
}
export interface iDynamicsPostUsers { //maps to OrganizationPost
  UserBCeID: string;
  BusinessBCeID: string;
  StaffCollection: iDynamicsCrmContactPost[];
}
export interface iDynamicsPostStatusReport { //maps to MonthlyStatisticsAnswers
  BusinessBCeID: string;
  UserBCeID: string;
  AnswerCollection: iDynamicsAnswer[];
}
export interface iDynamicsPostFile { //maps to FilePost
  Businessbceid: string;
  Userbceid: string;
  DocumentCollection?: iDynamicsDocumentPost[];
}
export interface iDynamicsPostSignedContract { //maps to FilePost
  Businessbceid: string;
  Userbceid: string;
  DocumentCollection?: iDynamicsDocumentPost[];
  Signature?: iDynamicsSignaturePost;
}
export interface iDynamicsSignaturePost {
  vsd_authorizedsigningofficersignature?: string;
  vsd_signingofficertitle?: string;
  vsd_signingofficersname?: string;
}
export interface iDynamicsPostRegisterNewUser {
  BusinessBCeID: string;
  UserBCeID: string;
  NewContact?: iDynamicsCrmContactPost;
  NewServiceProvider?: iDynamicsCrmServiceProviderPost;
}
export interface iDynamicsPostScheduleF { //maps to ProgramApplicationPost
  BusinessBCeID: string;
  UserBCeID: string;
  AddProgramContactCollection?: iDynamicsProgramContactPost[];
  AddProgramSubContractorCollection?: iDynamicsProgramContactPost[];
  ContactCollection?: iDynamicsCrmContactPost[];
  ContractCollection?: iDynamicsCrmContractPost[];
  Organization?: iDynamicsOrganizationPost;
  ProgramCollection?: iDynamicsCrmProgramPost[];
  RemoveProgramContactCollection?: iDynamicsRemoveProgramContactPost[];
  RemoveProgramSubContractorCollection?: iDynamicsRemoveProgramContactPost[];
  ScheduleCollection?: iDynamicsSchedulePost[];     //commented out on the controller side...
  StaffCollection?: iDynamicsProgramContactPost[];  //commented out on the controller side...
}
export interface iDynamicsPostScheduleFCAP { //maps to ProgramApplicationPost
  BusinessBCeID: string;
  UserBCeID: string;
  AddProgramContactCollection?: iDynamicsProgramContactPost[];
  ContactCollection?: iDynamicsCrmContactPost[];
  ContractCollection?: iDynamicsCrmContractPost[];
  Organization?: iDynamicsOrganizationPost;
  ProgramCollection?: iDynamicsCrmProgramPost[];
  RemoveProgramContactCollection?: iDynamicsRemoveProgramContactPost[];
  StaffCollection?: iDynamicsProgramContactPost[];  //commented out on the controller side...
}
export interface iDynamicsPostBudgetProposal { //maps to BudgetProposalPost
  BusinessBCeID: string;
  UserBCeID: string;
  ProgramExpenseCollection: iDynamicsProgramExpense[];
  ProgramRevenueSourceCollection: iDynamicsProgramRevenueSource[];
  ProgramCollection: iDynamicsBudgetProposalProgram[];
}
export interface iDynamicsPostSurplusPlan {
  BusinessBCeID: string;
  UserBCeID: string;
  SurplusPlanCollection: iDynamicsSurplusPlan[];
  SurplusPlanLineItemCollection: iDynamicsSurplusPlanLineItem[];
}
//------------------------------------------------------------------------------
export interface iDynamicsAnswer {
  vsd_QuestionIdfortunecookiebind?: string;
  vsd_CategoryIdfortunecookiebind?: string;
  // the text of the question
  vsd_name: string;
  // text description of the question category
  vsd_questioncategory: string;
  vsd_questionorder: number;
  // is this a number boolean or string? Numerically encoded to dynamics.
  vsd_questiontype1: number;
  // answer
  vsd_number?: number;
  vsd_yesno?: number;
  vsd_textanswer?: string;
}
export interface iDynamicsProgramExpense {
  vsd_EligibleExpenseItemIdfortunecookiebind?: string;
  vsd_ProgramIdfortunecookiebind: string;
  vsd_cpu_benefits?: number;
  vsd_cpu_fundedfromvscp: number;
  vsd_cpu_otherexpense?: string;
  vsd_cpu_programexpensetype: number;
  vsd_cpu_salary?: number;
  vsd_cpu_titleposition?: string;
  vsd_inputamount?: number;
  vsd_programexpenseid?: string;
  vsd_totalcost?: number;
  statecode?: number;
}
export interface iDynamicsProgramRevenueSource {
  vsd_ProgramIdfortunecookiebind: string;
  vsd_cashcontribution: number;
  vsd_cpu_revenuesourcetype: number;
  vsd_cpu_otherrevenuesource: string;
  vsd_inkindcontribution: number;
  vsd_programrevenuesourceid?: string;
  statecode?: number;
}
export interface iDynamicsBudgetProposalProgram {
  vsd_programid?: string;
  vsd_signingofficersignature?: string;
  vsd_signingofficerfullname?: string;
  vsd_signingofficertitle?: string;
}
export interface iDynamicsCrmContractPost {
  _vsd_customer_value?: string;
  statuscode?: number;
  vsd_ContactLookup1fortunecookiebind?: string;
  vsd_ContactLookup2fortunecookiebind?: string;
  vsd_contractid?: string;
  vsd_cpu_humanresourcepolices?: string; // this is actually an array that comes in as a comma seperated string
  vsd_cpu_insuranceoptions?: number;
  vsd_cpu_memberofcssea?: number;
  vsd_cpu_subcontractedprogramstaff?: number;
  vsd_cpu_specificunion?: string;
  vsd_cpu_unionizedstaff?: number;
  vsd_name?: string;
  vsd_authorizedsigningofficersignature?: string;
  vsd_signingofficertitle?: string;
  vsd_signingofficersname?: string;
  vsd_collaborationwithkeystakeholders?: number;
  vsd_complaintandfeedbackprocessforparticipant?: number;
  vsd_criminalrecordchecks?: boolean;
  vsd_letterofreferencefromreferralsources?: number;
  vsd_establishedconfidentialityguidelines?: number;
}
export interface iDynamicsDocumentPost {
  filename: string;
  body: string;
}
export interface iDynamicsSignedContract {
  activitymimeattachmentid: string;
  filename: string;
  body: string;
}
export interface iDynamicsProgramContactPost {
  contactid: string;
  vsd_programid: string; // added when contact is listed in a program
}
export interface iDynamicsRemoveProgramContactPost {
  contactid: string;
  vsd_programid: string; // added when contact is listed in a program
}
export interface iDynamicsOrganizationPost {
  _ownerid_value?: string;
  vsd_ExecutiveContactIdfortunecookiebind?: string;
  vsd_BoardContactIdfortunecookiebind?: string;
  // _vsd_boardcontactid_value?: string;
  // _vsd_executivecontactid_value?: string;
  accountid?: string;
  address1_city?: string;
  address1_country?: string;
  address1_line1?: string;
  address1_line2?: string;
  address1_postalcode?: string;
  address1_stateorprovince?: string;
  address2_city?: string;
  address2_country?: string;
  address2_line1?: string;
  address2_line2?: string;
  address2_postalcode?: string;
  address2_stateorprovince?: string;
  emailaddress1?: string;
  fax?: string;
  name?: string;
  telephone1?: string;
  vsd_bceid?: string;
}
export interface iDynamicsScheduleGPost {
  _vsd_serviceprovider_value?: string;
  _vsd_program_value?: string;
  _vsd_contract_value?: string;
  _vsd_contact_value?: string;
  _transactioncurrencyid_value?: string;

  vsd_programadministrationbudgeted?: number;
  vsd_programadministrationcurrentquarter?: number;
  vsd_programadministrationexplanation?: string;
  vsd_quarterlybudgetedprogramadministration?: number;
  vsd_yeartodateprogramadministration?: number;
  vsd_yeartodatevarianceprogramadministration?: number;
  vsd_quarterlyvarianceprogramadministration?: number;

  vsd_programdeliverybudgeted?: number;
  vsd_programdeliverycurrentquarter?: number;
  vsd_programdeliveryexplanations?: string;
  vsd_quarterlybudgetedprogramdelivery?: number;
  vsd_yeartodateprogramdelivery?: number;
  vsd_yeartodatevarianceprogramdelivery?: number;
  vsd_quarterlyvarianceprogramdelivery?: number;

  vsd_quarterlybudgetedsalariesbenefits?: number;
  vsd_salariesandbenefitsexplanation?: string;
  vsd_salariesbenefitscurrentquarter?: number;
  vsd_salaryandbenefitsbudgeted?: number;
  vsd_yeartodatesalariesandbenefits?: number;
  vsd_yeartodatevariancesalariesbenefits?: number;
  vsd_quarterlyvariancesalariesbenefits?: number;

  vsd_submitteddate?: string;

  vsd_schedulegid?: string;
  vsd_reportreviewed?: boolean;
  // vsd_cpu_reportingperiod?: number;

  // number of hours contracted area
  vsd_contractedservicehrsthisquarter?: number;
  vsd_actualhoursthisquarter?: number;
}
export interface iDynamicsScheduleGLineItemPost {
  _transactioncurrencyid_value?: string;
  _vsd_expenselineitem_value?: string;
  // _vsd_schedulegid_value?: string;
  vsd_actualexpendituresyeartodate?: number;
  vsd_scheduleglineitemid?: string;
  vsd_annualbudgetedamount?: number;
  vsd_quarterlybudgetedamount?: number;
  vsd_actualexpensescurrentquarter?: number;
  vsd_yeartodatevariance?: number;
  vsd_quarterlyvariance?: number;
}
export interface iDynamicsCrmContactPost {
  _parentcustomerid_value?: string;
  address1_city?: string;
  address1_country?: string; //doesn't exist on C# model, so doesn't matter if we don't send this...
  address1_line1?: string;
  address1_line2?: string;
  address1_postalcode?: string;
  address1_stateorprovince?: string;
  contactid?: string;
  emailaddress1?: string;
  fax?: string;
  firstname?: string;
  jobtitle?: string;
  lastname?: string;
  middlename?: string;
  mobilephone?: string;
  statecode?: number;
  vsd_bceid?: string;
  vsd_contact_vsd_programid?: string; // added when contact is listed in a program. Which program id are they under
  vsd_programid?: string; // added when contact is listed in a program
  vsd_portalfield?: string;
  vsd_contactrole?: number;
}
export interface iDynamicsCrmServiceProviderPost {
  name?: string;
  address1_line1?: string;
  address1_line2?: string;
  address1_city?: string;
  address1_country?: string;
  address1_stateorprovince?: string;
  emailaddress1?: string;
  fax?: string;
  telephone1?: string;
  address1_postalcode?: string;
}
export interface iDynamicsCrmProgramPost {
  _vsd_contractid_value?: string;
  _vsd_cpu_regiondistrict_value?: string;
  _vsd_programtype_value?: string;
  _vsd_serviceproviderid_value?: string;
  statecode?: number;
  statuscode?: number;
  vsd_ContactLookup2fortunecookiebind?: string;
  vsd_ContactLookup3fortunecookiebind?: string;
  vsd_ContactLookupfortunecookiebind?: string;
  vsd_addressline1?: string;
  vsd_addressline2?: string;
  vsd_city?: string;
  vsd_costshare?: boolean;
  vsd_country?: string;
  vsd_cpu_numberofhours?: number;
  vsd_cpu_per?: number;
  vsd_cpu_programstaffsubcontracted?: boolean;
  vsd_emailaddress?: string;
  vsd_fax?: string;
  vsd_governmentfunderagency?: string;
  vsd_mailingaddressline1?: string;
  vsd_mailingaddressline2?: string;
  vsd_mailingcity?: string;
  vsd_mailingcountry?: string;
  vsd_mailingpostalcodezip?: string;
  vsd_mailingprovincestate?: string;
  vsd_name?: string;
  vsd_phonenumber?: string;
  vsd_postalcodezip?: string;
  vsd_programid?: string;
  vsd_provincestate?: string;
  vsd_totaloncallstandbyhours?: number;
  vsd_totalscheduledhours?: number;
  vsd_cpu_fundingamountrequested?: number;
  vsd_cpu_subtotalcomponentvalue?: number;
  vsd_cpu_programmodeltypes?: string;
  vsd_otherprogrammodels?: string;
  vsd_cpu_programevaluationefforts?: number;
  vsd_cpu_programevaluationdescription?: string;
  vsd_cpu_capprogramoperationscomments?: string;
  vsd_addresstransitionorsafehome?: boolean;
}
export interface iDynamicsSchedulePost {
  _vsd_programid_value?: string;
  vsd_days?: string;
  vsd_scheduledendtime?: string;
  vsd_scheduledstarttime?: string;
  vsd_scheduleid?: string;
  vsd_cpu_scheduletype?: number;
}