import { iDynamicsScheduleG, iDynamicsScheduleGLineItem } from "../dynamics-blob";
import { iDynamicsPostScheduleG, iDynamicsScheduleGLineItemPost } from "../dynamics-post";
import { iExpenseReport } from "../expense-report.interface";
import { TransmogrifierExpenseReport } from "../transmogrifier-expense-report.class";

//userId: string, organizationId: string, expenseReportId: string, e: iExpenseReport
export function convertExpenseReportToDynamics(trans: TransmogrifierExpenseReport): iDynamicsPostScheduleG {
  // schedule g's
  const g: iDynamicsScheduleG = {};

  // administration costs
  if (trans.expenseReport.administrationAnnualBudget) g.vsd_programadministrationbudgeted = trans.expenseReport.administrationAnnualBudget;
  if (trans.expenseReport.administrationDescription) g.vsd_programadministrationexplanation = trans.expenseReport.administrationDescription;
  if (trans.expenseReport.administrationQuarterlyBudget) g.vsd_quarterlybudgetedprogramadministration = trans.expenseReport.administrationQuarterlyBudget;
  if (trans.expenseReport.administrationValue) g.vsd_programadministrationcurrentquarter = trans.expenseReport.administrationValue;
  g.vsd_quarterlyvarianceprogramadministration = (trans.expenseReport.administrationQuarterlyBudget || 0) - (trans.expenseReport.administrationValue || 0);
  g.vsd_yeartodateprogramadministration = (trans.expenseReport.administrationValue || 0) + (trans.expenseReport.administrationYearToDate || 0);
  g.vsd_yeartodatevarianceprogramadministration = (trans.expenseReport.administrationAnnualBudget || 0) - g.vsd_yeartodateprogramadministration;

  // program delivery costs
  if (trans.expenseReport.programDeliveryAnnualBudget) g.vsd_programdeliverybudgeted = trans.expenseReport.programDeliveryAnnualBudget;
  if (trans.expenseReport.programDeliveryDescription) g.vsd_programdeliveryexplanations = trans.expenseReport.programDeliveryDescription;
  if (trans.expenseReport.programDeliveryQuarterlyBudget) g.vsd_quarterlybudgetedprogramdelivery = trans.expenseReport.programDeliveryQuarterlyBudget;
  if (trans.expenseReport.programDeliveryValue) g.vsd_programdeliverycurrentquarter = trans.expenseReport.programDeliveryValue;
  g.vsd_quarterlyvarianceprogramdelivery = (trans.expenseReport.programDeliveryQuarterlyBudget || 0) - (trans.expenseReport.programDeliveryValue || 0);
  g.vsd_yeartodateprogramdelivery = (trans.expenseReport.programDeliveryValue || 0) + (trans.expenseReport.programDeliveryYearToDate || 0);
  g.vsd_yeartodatevarianceprogramdelivery = (trans.expenseReport.programDeliveryAnnualBudget || 0) - g.vsd_yeartodateprogramdelivery;

  // salaries and benefits costs
  if (trans.expenseReport.salariesBenefitsAnnualBudget) g.vsd_salaryandbenefitsbudgeted = trans.expenseReport.salariesBenefitsAnnualBudget;
  if (trans.expenseReport.salariesBenefitsDescription) g.vsd_salariesandbenefitsexplanation = trans.expenseReport.salariesBenefitsDescription;
  if (trans.expenseReport.salariesBenefitsQuarterlyBudget) g.vsd_quarterlybudgetedsalariesbenefits = trans.expenseReport.salariesBenefitsQuarterlyBudget;
  if (trans.expenseReport.salariesBenefitsValue) g.vsd_salariesbenefitscurrentquarter = trans.expenseReport.salariesBenefitsValue;
  g.vsd_quarterlyvariancesalariesbenefits = (trans.expenseReport.salariesBenefitsQuarterlyBudget || 0) - (trans.expenseReport.salariesBenefitsValue || 0);
  g.vsd_yeartodatesalariesandbenefits = (trans.expenseReport.salariesBenefitsValue || 0) + (trans.expenseReport.salariesBenefitsYearToDate || 0);
  g.vsd_yeartodatevariancesalariesbenefits = (trans.expenseReport.salariesBenefitsAnnualBudget || 0) - g.vsd_yeartodatesalariesandbenefits;

  // contract service hours
  if (trans.expenseReport.serviceHoursQuarterlyActual) g.vsd_actualhoursthisquarter = trans.expenseReport.serviceHoursQuarterlyActual;
  // if (trans.expenseReport.serviceHours) g.vsd_contractedservicehrsthisquarter = trans.expenseReport.serviceHours;
  // if (trans.expenseReport.onCallStandByHours) g.vsd_contractedservicehrsthisquarter = trans.expenseReport.onCallStandByHours;
  if (trans.expenseReport.executiveReview) {
    g.vsd_reportreviewed = trans.expenseReport.executiveReview;
  }
  else {
    g.vsd_reportreviewed = false;
  }

  // save the identifier for this form
  if (trans.expenseReport.expenseReportId) g.vsd_schedulegid = trans.expenseReport.expenseReportId;

  //save if report has been reviewed
  // if (trans.expenseReport.executiveReview) g.vsd_reportreviewed = trans.expenseReport.executiveReview;

  // schedule g line items;
  const glis: iDynamicsScheduleGLineItemPost[] = [];
  for (let y of trans.expenseReport.programExpenseLineItems) {
    const lineItem: iDynamicsScheduleGLineItemPost = {
      vsd_scheduleglineitemid: y.itemId,
      vsd_actualexpensescurrentquarter: y.actual || 0,
      vsd_quarterlyvariance: (y.quarterlyBudget || 0) - (y.actual || 0),
      vsd_actualexpendituresyeartodate: (y.actual || 0) + (y.actualYearToDate || 0),
      vsd_yeartodatevariance: (y.annualBudget || 0) - ((y.actual || 0) + (y.actualYearToDate || 0))
    };
    glis.push(lineItem);
  }

  return {
    BusinessBCeID: trans.organizationId,
    UserBCeID: trans.userId,
    ScheduleGCollection: [g],
    ScheduleGLineItemCollection: glis,
  } as iDynamicsPostScheduleG;
}
