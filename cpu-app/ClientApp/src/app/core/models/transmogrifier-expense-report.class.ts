import { iDynamicsScheduleGResponse } from "./dynamics-blob";
import { ExpenseItemLabels } from "../constants/expense-item-labels";
import { iExpenseReport } from "./expense-report.interface";
import { REPORTING_PERIODS } from "../constants/reporting-period";

// a collection of the expense item guids as K/V pairs for generating line items
export class TransmogrifierExpenseReport {
  public organizationId: string;
  public userId: string;
  public expenseReport: iExpenseReport;

  constructor(g: iDynamicsScheduleGResponse) {
    this.userId = g.Userbceid;// this is the user's bceid
    this.organizationId = g.Businessbceid; // this is the organization's bceid
    this.expenseReport = this.buildExpenseReport(g);
  }
  buildExpenseReport(g: iDynamicsScheduleGResponse): iExpenseReport {
    // for every item in the schedule g's
    const e: iExpenseReport = {
      expenseReportId: g.ScheduleG.vsd_schedulegid || null,
      reportingPeriod: REPORTING_PERIODS[g.ScheduleG.vsd_cpu_reportingperiod],
      // salaries and benefits costs
      salariesBenefitsDescription: g.ScheduleG.vsd_salariesandbenefitsexplanation || '',
      salariesBenefitsAnnualBudget: g.ScheduleG.vsd_salaryandbenefitsbudgeted || 0,
      salariesBenefitsQuarterlyBudget: g.ScheduleG.vsd_quarterlybudgetedsalariesbenefits || 0,
      salariesBenefitsValue: g.ScheduleG.vsd_salariesbenefitscurrentquarter || 0,
      salariesBenefitsMask: g.ScheduleG.vsd_salariesbenefitscurrentquarter ? g.ScheduleG.vsd_salariesbenefitscurrentquarter.toString() : "0",
      salariesBenefitsQuarterlyVariance: g.ScheduleG.vsd_quarterlyvariancesalariesbenefits || 0,
      salariesBenefitsYearToDate: (g.ScheduleG.vsd_yeartodatesalariesandbenefits || 0) - (g.ScheduleG.vsd_salariesbenefitscurrentquarter || 0),
      salariesBenefitsYearToDateVariance: (g.ScheduleG.vsd_yeartodatevariancesalariesbenefits || 0) - (g.ScheduleG.vsd_salariesbenefitscurrentquarter || 0),

      // program delivery costs
      programDeliveryDescription: g.ScheduleG.vsd_programdeliveryexplanations || '',
      programDeliveryAnnualBudget: g.ScheduleG.vsd_programdeliverybudgeted || 0,
      programDeliveryQuarterlyBudget: g.ScheduleG.vsd_quarterlybudgetedprogramdelivery || 0,
      programDeliveryValue: g.ScheduleG.vsd_programdeliverycurrentquarter || 0,
      programDeliveryMask: g.ScheduleG.vsd_programdeliverycurrentquarter ? g.ScheduleG.vsd_programdeliverycurrentquarter.toString() : "0",
      programDeliveryQuarterlyVariance: g.ScheduleG.vsd_quarterlyvarianceprogramdelivery || 0,
      programDeliveryYearToDate: (g.ScheduleG.vsd_yeartodateprogramdelivery || 0) - (g.ScheduleG.vsd_programdeliverycurrentquarter || 0),
      programDeliveryYearToDateVariance: (g.ScheduleG.vsd_yeartodatevarianceprogramdelivery || 0) - (g.ScheduleG.vsd_programdeliverycurrentquarter || 0),

      // administration costs
      administrationDescription: g.ScheduleG.vsd_programadministrationexplanation || '',
      administrationAnnualBudget: g.ScheduleG.vsd_programadministrationbudgeted || 0,
      administrationQuarterlyBudget: g.ScheduleG.vsd_quarterlybudgetedprogramadministration || 0,
      administrationValue: g.ScheduleG.vsd_programadministrationcurrentquarter || 0,
      administrationMask: g.ScheduleG.vsd_programadministrationcurrentquarter ? g.ScheduleG.vsd_programadministrationcurrentquarter.toString() : "0",
      administrationQuarterlyVariance: g.ScheduleG.vsd_quarterlyvarianceprogramadministration || 0,
      administrationYearToDate: (g.ScheduleG.vsd_yeartodateprogramadministration || 0) - (g.ScheduleG.vsd_programadministrationcurrentquarter || 0),
      administrationYearToDateVariance: (g.ScheduleG.vsd_yeartodatevarianceprogramadministration || 0) - (g.ScheduleG.vsd_programadministrationcurrentquarter || 0),

      // contract service hours
      serviceHoursQuarterlyActual: g.ScheduleG.vsd_actualhoursthisquarter || 0,
      serviceHours: g.Program.vsd_cpu_numberofhours || 0,
      perType: g.Program.vsd_cpu_per || 100000000,
      onCallStandByHours: g.Program.vsd_totaloncallstandbyhours || 0,
      executiveReview: g.ScheduleG.vsd_reportreviewed || false,
      // placeholder
      programExpenseLineItems: [],
    };
    // for every item in the schedule g line items
    for (let item of g.ScheduleGLineItems) {
      // if the schedule G identifier guid matches the guid for the line items
      if (item._vsd_schedulegid_value === g.ScheduleG.vsd_schedulegid) {
        e.programExpenseLineItems.push({
          // get the correct label for the line from the list of constant values
          itemId: item.vsd_scheduleglineitemid,
          label: ExpenseItemLabels[item._vsd_expenselineitem_value.toUpperCase()] || "Unknown Line Item Type",
          annualBudget: item.vsd_annualbudgetedamount || 0,
          quarterlyBudget: item.vsd_quarterlybudgetedamount || 0,
          actual: item.vsd_actualexpensescurrentquarter || 0,
          mask: item.vsd_actualexpensescurrentquarter ? item.vsd_actualexpensescurrentquarter.toString() : "0",
          quarterlyVariance: item.vsd_quarterlyvariance || 0,
          actualYearToDate: (item.vsd_actualexpendituresyeartodate || 0) - (item.vsd_actualexpensescurrentquarter || 0),
          yearToDateVariance: (item.vsd_yeartodatevariance || 0) - (item.vsd_actualexpensescurrentquarter || 0),
          description: ''
        });
      }
    }
    return e;
  }
}

