// This is what we expect dynamics to dump on us.
// to make changes to how this comes in. we should make a change here, to the transmogrifier, and to the postback converter function that lives in the transmogrifier file
// The strategy is to get the dynamics things which include a bunch of messy useless information and then construct a matching transmogrifier
// Dynamics is returning giant collections of information that represent the entire collection of data for an entity instead of a single resource.
// the transmogrifier lets us manage the properties in the view models.
// When we want to send it back to dynamics we use a converter function to map the data in a way that Dynamics expects to recieve.

// Basically Dynamics API gets all data from an entity without any restrictions which can result in a massive amount of data coming for minor updates.
// So

export interface iDynamicsOrganization {
  _ownerid_value?: string;
  _vsd_boardcontactid_value?: string;
  _vsd_executivecontactid_value?: string;
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
  fortunecookieetag?: string;
  name?: string;
  telephone1?: string;
  vsd_BoardContactIdfortunecookiebind?: string;
  vsd_ExecutiveContactIdfortunecookiebind?: string;
  vsd_bceid?: string;
}
export interface iDynamicsCrmContact {
  _parentcustomerid_value?: string;
  address1_city?: string;
  address1_country?: string;
  address1_line1?: string;
  address1_line2?: string;
  address1_postalcode?: string;
  address1_stateorprovince?: string;
  contactid?: string;
  emailaddress1?: string;
  fax?: string;
  firstname?: string;
  fortunecookieetag?: string;
  fortunecookietype?: string;
  jobtitle?: string;
  lastname?: string;
  middlename?: string;
  mobilephone?: string;
  vsd_mainphoneextension?: string;
  telephone2?: string;
  vsd_homephoneextension?: string;
  statecode?: number;
  vsd_bceid?: string;
  vsd_contact_vsd_programid?: string; // added when contact is listed in a program. Which program id are they under
  vsd_programid?: string; // added when contact is listed in a program
  vsd_employmentstatus?: number;
  vsd_portalfield?: string;
}
export interface iDynamicsCrmContract {
  _vsd_contactlookup1_value?: string;
  _vsd_contactlookup2_value?: string;
  _vsd_customer_value?: string;
  fortunecookieetag?: string;
  fortunecookietype?: string;
  statuscode?: number;
  vsd_contractid?: string;
  vsd_cpu_humanresourcepolices?: string;
  vsd_cpu_insuranceoptions?: number;
  vsd_cpu_memberofcssea?: string;
  vsd_cpu_specificunion?: string;
  vsd_cpu_subcontractedprogramstaff?: number;
  vsd_cpu_unionizedstaff?: number;
  vsd_fiscalenddate?: string;
  vsd_fiscalstartdate?: string;
  vsd_name?: string;
}
export interface iDynamicsMinistryUser {
  address1_telephone1?: string;
  firstname?: string;
  fortunecookieetag?: string;
  internalemailaddress?: string;
  lastname?: string;
  ownerid?: string;
  systemuserid?: string;
}
export interface iDynamicsCrmProgram {
  _vsd_contactlookup2_value?: string;
  _vsd_contactlookup3_value?: string;
  _vsd_contactlookup_value?: string;
  _vsd_contractid_value?: string;
  _vsd_cpu_regiondistrict_value?: string;
  _vsd_programtype_value?: string;
  _vsd_serviceproviderid_value?: string;
  fortunecookieetag?: string;
  fortunecookietype?: string;
  statecode?: number;
  statuscode?: number;
  vsd_addressline1?: string;
  vsd_addressline2?: string;
  vsd_addresstransitionorsafehome?: boolean;
  vsd_city?: string;
  vsd_costshare?: boolean;
  vsd_country?: string;
  vsd_cpu_estimatedsubtotalcomponentvalue?: number;
  vsd_cpu_numberofhours?: number;
  vsd_cpu_per?: number;
  vsd_cpu_program_location?: string;
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
}
export interface iDynamicsCrmProgramType {
  vsd_programtypeid?: string;
  vsd_name?: string;
  vsd_programcategory?: number;
}
export interface iDynamicsCrmTask {
  _regardingobjectid_value?: string;
  _vsd_programid_value?: string,
  _vsd_schedulegid_value?: string;
  _vsd_tasktypeid_value?: string;
  _vsd_surplusplanid_value?: string;
  activityid?: string;
  description?: string;
  fortunecookieetag: string;
  fortunecookietype: string;
  scheduledend?: string;
  modifiedon?: string;
  statecode?: number;
  statuscode?: number;
  subject?: string;
}
export interface iDynamicsCrmMessage {
  TimeStamp: Date;
  From: Date;
  To: Date;
  vsd_direction: string;
  regardingobjectid: string;
  vsd_Program: string;
  vsd_cpu_regiondistrict: string;
  Subject: string;
  Description: string;
}
export interface iDynamicsBlob {
  BoardContact?: iDynamicsCrmContact,
  Businessbceid?: string; // represents the organization level BCeID.
  Contracts?: iDynamicsCrmContract[];
  ExecutiveContact?: iDynamicsCrmContact,
  Invoices?: iDynamicsCrmInvoice[];
  IsSuccess: boolean;
  Messages?: iDynamicsCrmMessage[];
  MinistryUser?: iDynamicsMinistryUser;
  Organization?: iDynamicsOrganization;
  PortalRoles?: iDynamicsPortalRole[];
  Programs?: iDynamicsCrmProgram[];
  Result: string;
  Staff?: iDynamicsCrmContact[];
  Tasks?: iDynamicsCrmTask[];
  Userbceid?: string; // represents the user's BCeID.
  fortunecookiecontext?: string;
};

export interface iDynamicsPortalRole {
  vsd_name: string;
  vsd_portalroleid: string;
}

export interface iDynamicsProgramManagerLookup {
  systemuserid: string;
  ownerid: string;
}

export interface iDynamicsCrmInvoice {
  fortunecookietype?: string;
  fortunecookieetag?: string;
  _vsd_contractid_value?: string;
  vsd_invoiceid?: string;
  vsd_cpu_scheduledpaymentdate?: string;
  statuscode?: string;
  _vsd_programid_value?: string;
}

export interface iDynamicsScheduleG {
  fortunecookieetag?: string;
  fortunecookietype?: string;
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
  vsd_cpu_reportingperiod?: number;

  // number of hours contracted area
  vsd_contractedservicehrsthisquarter?: number;
  vsd_actualhoursthisquarter?: number;
}
export interface iDynamicsScheduleGLineItem {
  fortunecookieetag?: string;
  fortunecookietype?: string;
  _transactioncurrencyid_value?: string;
  _vsd_expenselineitem_value?: string;
  _vsd_schedulegid_value?: string;
  vsd_actualexpendituresyeartodate?: number;
  vsd_scheduleglineitemid?: string;
  vsd_annualbudgetedamount?: number;
  vsd_quarterlybudgetedamount?: number;
  vsd_actualexpensescurrentquarter?: number;
  vsd_yeartodatevariance?: number;
  vsd_quarterlyvariance?: number;
}
export interface iDynamicsScheduleGResponse {
  fortunecookiecontext?: string;
  IsSuccess: boolean;
  Result: string;
  Userbceid?: string;
  Businessbceid?: string;
  ScheduleG?: iDynamicsScheduleG;
  Program?: iDynamicsCrmProgram;
  ScheduleGLineItems?: iDynamicsScheduleGLineItem[];
}
export interface iDynamicsSchedule {
  vsd_ProgramIdfortunecookiebind?: string;
  _vsd_programid_value?: string;
  fortunecookieetag?: string;
  fortunecookietype?: string;
  vsd_days?: string;
  vsd_scheduledendtime?: string;
  vsd_scheduledstarttime?: string;
  vsd_scheduleid?: string;
  vsd_cpu_scheduletype?: number;
  statecode?: number;
}
export interface iDynamicsServiceArea {
  vsd_programid?: string;
  vsd_regiondistrictid?: string;
  vsd_vsd_regiondistrict_vsd_programid?: string;
}
export interface iDynamicsScheduleFResponse {
  BoardContact?: iDynamicsCrmContact;
  Businessbceid: string;
  Contract?: iDynamicsCrmContract;
  ExecutiveContact?: iDynamicsCrmContact;
  IsSuccess?: boolean;
  Organization?: iDynamicsOrganization;
  ProgramCollection?: iDynamicsCrmProgram[];
  ProgramContactCollection?: iDynamicsCrmContact[];
  ProgramSubContractorCollection?: iDynamicsCrmContact[];
  ProgramTypeCollection?: iDynamicsCrmProgramType[];
  RegionDistrictCollection?: iDynamicsRegionDistrict[];
  Result: string;
  ScheduleCollection?: iDynamicsSchedule[];
  ServiceAreaCollection?: iDynamicsServiceArea[];
  StaffCollection?: iDynamicsCrmContact[];
  Userbceid?: string;
}
export interface iDynamicsScheduleFCAPResponse {
  BoardContact?: iDynamicsCrmContact;
  Businessbceid: string;
  Contract?: iDynamicsCrmContract;
  ExecutiveContact?: iDynamicsCrmContact;
  IsSuccess?: boolean;
  Organization?: iDynamicsOrganization;
  PortalRoles?: iDynamicsPortalRole[];
  ProgramCollection?: iDynamicsCrmProgram[];
  ProgramContactCollection?: iDynamicsCrmContact[];
  ProgramManager?: iDynamicsProgramManagerLookup;
  ProgramTypeCollection?: iDynamicsCrmProgramType[];
  Result: string;
  StaffCollection?: iDynamicsCrmContact[];
  Userbceid?: string;
}

export interface iDynamicsProgramSurplusResponse {
  Businessbceid: string;
  Contract?: iDynamicsCrmContract;
  EligibleExpenseItemCollection?: iDynamicsEligibleExpenseItem[];
  IsSuccess?: boolean;
  Organization?: iDynamicsOrganization;
  Program?: iDynamicsCrmProgram;
  Result: string;
  SurplusPlan?: iDynamicsSurplusPlan;
  SurplusPlanLineItems?: iDynamicsSurplusPlanLineItem[];
  Userbceid?: string;
}

export interface iDynamicsSurplusPlan {
  vsd_surplusplanreportid: string;
  _vsd_programid_value?: string;
  vsd_surplusamount?: number;
  vsd_surplusremittance: boolean;
  vsd_datesubmitted?: Date;
}

export interface iDynamicsSurplusPlanLineItem {
  _vsd_eligibleexpenseitemid_value?: string;
  _vsd_surplusplanid_value?: string;
  vsd_surplusplanid?: string;
  vsd_name?: string;
  vsd_justificationdetails?: string;
  vsd_actualexpenditures?: number;
  vsd_actualexpenditures2?: number;
  vsd_actualexpenditures3?: number;
  vsd_actualexpenditures4?: number;
  vsd_proposedexpenditures?: number;
  vsd_allocatedamount?: number;
  vsd_surpluslineitemid: string;
  vsd_datesubmitted?: Date;
}


export interface iDynamicsRegionDistrict {
  vsd_name: string;
  vsd_regiondistrictid: string;
}

export interface iDynamicsBudgetProposal {
  AdministrationCostCollection: iDynamicsProgramExpense[];
  Businessbceid: string;
  Contract: iDynamicsCrmContract;
  EligibleExpenseItemCollection: iDynamicsEligibleExpenseItem[];
  IsSuccess: boolean;
  Organization: iDynamicsOrganization;
  ProgramCollection: iDynamicsCrmProgramBudget[];
  ProgramDeliveryCostCollection: iDynamicsProgramExpense[];
  ProgramRevenueSourceCollection: iDynamicsCrmProgramRevenueSource[];
  Result: string;
  SalaryAndBenefitCollection: iDynamicsProgramExpense[];
  Userbceid: string;
  ProgramTypeCollection: iDynamicsProgramType[];
}
export interface iDynamicsProgramType {
  vsd_programtypeid: string;
  vsd_name: string;
}
export interface iDynamicsEligibleExpenseItem {
  vsd_eligibleexpenseitemid: string;
  vsd_name: string;
  vsd_programexpensetype: number;
}
export interface iDynamicsProgramExpense {
  _transactioncurrencyid_value?: string;
  _vsd_eligibleexpenseitemid_value?: string;
  _vsd_programid_value?: string;
  vsd_programexpenseid?: string;
  vsd_cpu_titleposition?: string;
  vsd_cpu_benefits?: number;
  vsd_cpu_fundedfromvscp?: number;
  vsd_cpu_programexpensetype?: number;
  vsd_cpu_salary?: number;
  vsd_inputamount?: number;
  vsd_totalcost?: number;
  vsd_cpu_otherexpense?: string;
  statecode?: number;
}
export interface iDynamicsCrmProgramBudget {
  _transactioncurrencyid_value?: string,
  _vsd_contactlookup_value?: string,
  _vsd_contractid_value?: string,
  _vsd_programtype_value?: string,
  _vsd_serviceproviderid_value?: string,
  statuscode?: number,
  vsd_cpu_percentoftotaladmincostsfromvscp?: number,
  vsd_cpu_percentoftotalprogramdeliveryfromvscp?: number,
  vsd_cpu_percentoftotalsalarybenefitsvscp?: number,
  vsd_cpu_totaladministrationcosts?: number,
  vsd_cpu_totaladministrationcostsfromvscp?: number,
  vsd_cpu_totalcashcontributions?: number,
  vsd_cpu_totalinkindcontributions?: number,
  vsd_cpu_totalprogramdeliverycosts?: number,
  vsd_cpu_totalprogramdeliveryfromvscp?: number,
  vsd_cpu_totalrevenueamounts?: number,
  vsd_cpu_totalsalariesandbenefits?: number,
  vsd_cpu_totalsalariesandbenefitsfromvscp?: number,
  vsd_emailaddress?: string,
  vsd_name?: string,
  vsd_programid?: string,
}
export interface iDynamicsCrmProgramRevenueSource {
  _transactioncurrencyid_value?: string;
  _vsd_programid_value?: string;
  vsd_cashcontribution?: number;
  vsd_cpu_otherrevenuesource?: string;
  vsd_cpu_revenuesourcetype?: number;
  vsd_inkindcontribution?: number;
  vsd_programrevenuesourceid?: string;
  statecode?: number;
}

export interface iDynamicsMonthlyStatisticsAnswers {
  AnswerCollection?: iDynamicsMonthlyStatisticsAnswersAnswer[];
  Businessbceid?: string;
  CategoryCollection?: iDynamicsMonthlyStatisticsCategory[];
  Contact?: iDynamicsMonthlyStatisticsQuestionsContact;
  Contract?: iDynamicsMonthlyStatisticsQuestionsContract;
  IsSuccess: boolean;
  Organization?: iDynamicsMonthlyStatisticsQuestionsOrganization;
  Program?: iDynamicsMonthlyStatisticsQuestionsProgram;
  ReportingPeriod?: number;
  Result: string;
  Userbceid?: string;
}

export interface iDynamicsMonthlyStatisticsQuestions {
  Businessbceid?: string;
  CategoryCollection?: iDynamicsMonthlyStatisticsCategory[];
  ChildMultipleChoiceCollection?: iDynamicsMonthlyStatisticsChildMcQuestion[];
  ChildQuestionCollection?: iDynamicsMonthlyStatisticsChildQuestion[];
  Contract?: iDynamicsMonthlyStatisticsQuestionsContract;
  IsSuccess: boolean;
  MultipleChoiceCollection?: iDynamicsMonthlyStatisticsQuestionsMcQuestion[];
  Organization?: iDynamicsMonthlyStatisticsQuestionsOrganization;
  Program?: iDynamicsMonthlyStatisticsQuestionsProgram;
  ProgramTypeCollection?: iDynamicsMonthlyStatisticsProgramType[];
  QuestionCollection?: iDynamicsMonthlyStatisticsQuestionsQuestion[];
  ReportingPeriod?: number;
  Result: string;
  Userbceid?: string;
}
export interface iDynamicsMonthlyStatisticsQuestionsOrganization {
  accountid?: string;
  name?: string;
}
export interface iDynamicsMonthlyStatisticsQuestionsContact {
  contactid?: string;
  firstname?: string;
  lastname?: string;
}
export interface iDynamicsMonthlyStatisticsQuestionsContract {
  _vsd_customer_value?: string;
  vsd_contractid?: string;
  vsd_name?: string;
}
export interface iDynamicsMonthlyStatisticsQuestionsProgram {
  _vsd_contractid_value?: string;
  _vsd_programtype_value?: string;
  _vsd_serviceproviderid_value?: string;
  vsd_name?: string;
  vsd_programid?: string;
  vsd_cpu_numberofhours?: number;
}
export interface iDynamicsMonthlyStatisticsAnswersAnswer {
  vsd_questioncategory?: string;
  vsd_datacollectionlineitemid?: string;
  _vsd_datacollectionid_value?: string;
  createdon?: Date;
  vsd_name?: string;
  vsd_questionorder?: number;
  vsd_questiontype1?: number;
  vsd_tooltip?: string;
  vsd_yesno?: number;
  vsd_textanswer?: string;
  vsd_number?: number;
}

export interface iDynamicsMonthlyStatisticsQuestionsQuestion {
  _vsd_categoryid_value?: string;
  _vsd_cpuprogramtype_value?: string;
  vsd_cpustatisticsmasterdataid?: string;
  vsd_name?: string;
  vsd_questionorder?: number;
  vsd_questiontype?: number;
  vsd_tooltip?: string;
  vsd_yesno?: number;
  vsd_textanswer?: string;
  vsd_number?: number;
}
export interface iDynamicsMonthlyStatisticsChildQuestion {
  _vsd_categoryid_value?: string;
  _vsd_parentid_value?: string;
  vsd_cpustatisticsmasterdataid?: string;
  vsd_name?: string;
  vsd_questionorder?: number;
  vsd_questiontype?: number;
}
export interface iDynamicsMonthlyStatisticsQuestionsMcQuestion {
  vsd_cpustatisticsmasterdataanswerid?: string;
  vsd_name?: string;
  _vsd_questionid_value?: string;
}
export interface iDynamicsMonthlyStatisticsChildMcQuestion {
  vsd_cpustatisticsmasterdataanswerid?: string;
  vsd_name?: string;
  _vsd_questionid_value?: string;
  _vsd_parentid_value?: string;
}
export interface iDynamicsMonthlyStatisticsCategory {
  vsd_categoryorder?: number;
  vsd_monthlystatisticscategoryid?: string;
  vsd_name?: string;
}
export interface iDynamicsMonthlyStatisticsProgramType {
  vsd_name?: string;
  vsd_programtypeid?: string;
}
export interface iDynamicsContactNotApproved {
  Userbceid?: string;
  Businessbceid?: string;
}
export interface iDynamicsScheduleGPost {
  fortunecookieetag?: string;
  fortunecookietype?: string;
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
  vsd_cpu_reportingperiod?: number;

  // number of hours contracted area
  vsd_contractedservicehrsthisquarter?: number;
  vsd_actualhoursthisquarter?: number;
}
export interface iDynamicsFile {
  IsSuccess?: boolean;
  Result?: string;
  Businessbceid: string;
  Userbceid: string;
  DocumentCollection?: iDynamicsDocument[];
}
export interface iDynamicsDocument {
  filename: string;
  subject: string;
  subjectOther: string;
  body: string;
  overwritetime?: string;
  activitymimeattachmentid?: string;
}

export interface iDynamicsMonthlyStatistics {
  fortunecookiecontext?: string;
  Businessbceid?: string;
  ContactCollection?: iDynamicsContact[];
  DataCollection?: iDynamicsDataCollection[];
  IsSuccess: boolean;
  ProgramCollection?: iDynamicsProgram[];
  Result: string;
  Userbceid?: string;
  UserCollection?: iDynamicsUser[];
}

export interface iDynamicsDataCollection {
  fortunecookietype?: string;
  fortunecookieetag?: string;
  _ownerid_value?: string;
  vsd_datacollectionid?: string;
  vsd_reportingperiod?: number;
  reportingPeriod?: string;
  statuscode?: number;
  vsd_submissiondate?: string;
  _vsd_contact_value?: string;
  createdon?: Date;
  _vsd_program_value?: string;
  vsd_name?: string;
}

export interface iDynamicsContact {
  fortunecookietype?: string;
  contactid?: string;
  fullname?: string;
}

export interface iDynamicsProgram {
  fortunecookietype?: string;
  vsd_programid?: string;
  vsd_name?: string;
}

export interface iDynamicsUser {
  fortunecookietype?: string;
  systemuserid?: string;
  fullname?: string;
  ownerid?: string;
}