import { PAYMENT_QUARTERS } from '../constants/reporting-period';
import { Roles } from './user-settings.interface';
import { contractStatus } from '../constants/contract-code';
import { decodeTaskType } from '../constants/decode-task-type';
import { employmentStatusTypeDict } from '../constants/employment-status-types';
import { iContactInformation } from './contact-information.interface';
import { iContract } from './contract.interface';
import { iDynamicsBlob, iDynamicsCrmTask } from './dynamics-blob';
import { iMessage } from './message.interface';
import { iMinistryUser } from './ministry-user.interface';
import { iPaymentStatus, PaymentStatusDisplay } from './payment-status.interface';
import { iPerson } from './person.interface';
import { iProgram } from './program.interface';
import { iTask } from './task.interface';
import { nameAssemble } from '../constants/name-assemble';
import { taskCode } from '../constants/task-code';
import * as _ from 'lodash';
import * as moment from 'moment';

export class Transmogrifier {
  public accountId: string; // this is the ID to identify an organization in dynamics. NOT A BCEID
  public contactInformation: iContactInformation;
  public contracts: iContract[];
  public ministryContact: iMinistryUser;
  public organizationId: string;
  public organizationName: string;
  public persons: iPerson[];
  public role: Roles;
  public userId: string;

  private ROLES_LOOKUP = [
    {
      vsd_name: "Program Staff Role",
      vsd_portalroleid: "286d3bd0-22e6-e911-b811-00505683fbf4",
      role: Roles.ProgramStaff,
    },
    {
      vsd_name: "Board Contact Role",
      vsd_portalroleid: "71a24e72-b7f3-ea11-b81d-00505683fbf4",
      role: Roles.BoardContact,
    },
    {
      vsd_name: "Executive Contact Role",
      vsd_portalroleid: "89b84866-b7f3-ea11-b81d-00505683fbf4",
      role: Roles.ExecutiveContact,
    }
  ]

  constructor(b: iDynamicsBlob) {
    this.accountId = b.Organization.accountid || null; // the dynamics id must be included when posting back sometimes.
    this.contracts = [];
    this.organizationId = b.Businessbceid || null;
    this.organizationName = b.Organization.name || null;
    this.userId = b.Userbceid || null;
    this.contactInformation = this.buildContactInformation(b);
    this.persons = this.buildPersons(b);
    this.ministryContact = this.buildMinistryContact(b);
    this.contracts = this.buildContracts(b);
    this.role = Roles.ProgramStaff;
    for (let i = 0; i < b.PortalRoles.length; ++i) {
      let thisRole = this.ROLES_LOOKUP.find(r => r.vsd_portalroleid === b.PortalRoles[i].vsd_portalroleid);
      if (thisRole) {
        let roleLevel = thisRole.role;
        if (roleLevel > this.role) this.role = roleLevel;
      }
    }
  }
  private buildTasks(b: iDynamicsBlob, contractId: string): iTask[] {
    const tasks: iTask[] = [];
    for (let task of b.Tasks) {
      if (task._regardingobjectid_value === contractId) {
        let thisTask: iTask =
        {
          status: taskCode(task.statuscode),
          isCompleted: this.isCompleted(task.statecode),
          taskName: decodeTaskType(task._vsd_tasktypeid_value, true),
          taskTitle: task.subject,
          taskDescription: task.description,
          deadline: task.scheduledend ? new Date(task.scheduledend) : null,
          submittedDate: task.modifiedon ? new Date(task.modifiedon) : null,
          taskId: this.getCorrectTaskIdByDiscriminator(contractId, task._vsd_programid_value, task, decodeTaskType(task._vsd_tasktypeid_value)),
          formType: decodeTaskType(task._vsd_tasktypeid_value)
        };

        if (task._vsd_programid_value && (thisTask.formType === "expense_report" || thisTask.formType === "status_report")) {
          let programInfo = b.Programs.find(p => p.vsd_programid === task._vsd_programid_value);
          if (programInfo) {
            thisTask.taskName += " (" + programInfo.vsd_name + ")";
          }
        }
        tasks.push(thisTask);
      }
    }
    return tasks;
  }

  private buildMessages(b: iDynamicsBlob, contractId: string): iMessage[] {
    const messages: iMessage[] = [];
    for (let message of b.Messages) {
      if (message.regardingobjectid === contractId) {
        messages.push({
          timeStamp: null,
          from: null,
          to: null,
          direction: null,
          regardingObjectId: null,
          program: message.vsd_Program,
          cpuRegionDistrict: message.vsd_cpu_regiondistrict,
          subject: null,
          description: message.Description,
        })
      }
    }
    return messages;
  }

  private getCorrectTaskIdByDiscriminator(contractId: string, programId: string, t: iDynamicsCrmTask, discriminator: string): string {
    // sometimes we look up by a scheduleG ID, sometimes by a contractId, sometimes by a programId. :-(
    if (discriminator === 'budget_proposal') {
      return contractId;
    }
    if (discriminator === 'expense_report') {
      return t._vsd_schedulegid_value;
    }
    if (discriminator === 'status_report' || discriminator === 'sign_contract') {
      return t.activityid;
    }
    if (discriminator === 'program_application' || discriminator === 'cap_program_application') {
      return contractId;
    }
    if (discriminator === 'download_document') {
      return contractId;
    }
    if (discriminator === 'cover_letter') {
      return contractId;
    }
    if (discriminator === 'program_surplus' || discriminator === 'surplus_report') {
      return t._vsd_surplusplanid_value;
    }
    return contractId;
  }
  private buildPrograms(b: iDynamicsBlob, contractId: string): iProgram[] {
    const programs: iProgram[] = [];
    for (let program of b.Programs) {
      if (program._vsd_contractid_value === contractId) {
        let programContact = b.Staff.find(s => s.contactid === program._vsd_contactlookup_value);
        programs.push({
          // build an address
          address: {
            city: program.vsd_city,
            line1: program.vsd_addressline1,
            line2: program.vsd_addressline2,
            postalCode: program.vsd_city,
            province: program.vsd_provincestate,
          },
          email: program.vsd_emailaddress,
          fax: program.vsd_fax,
          // build an address
          mailingAddress: {
            city: program.vsd_mailingcity,
            line1: program.vsd_mailingaddressline1,
            line2: program.vsd_mailingaddressline2,
            postalCode: program.vsd_mailingcity,
            province: program.vsd_mailingprovincestate,
          },
          phone: program.vsd_phonenumber,
          programId: program.vsd_programid,
          programName: program.vsd_name,
          contactName: programContact ? nameAssemble(programContact.firstname, programContact.middlename, programContact.lastname) : "",
          contactTitle: programContact ? programContact.jobtitle : "",
          paymentsStatus: this.buildPaymentsStatus(b, program.vsd_programid),
        });
      }
    }
    return programs;
  }
  private buildPaymentsStatus(b: iDynamicsBlob, programId: string): iPaymentStatus {
    let paymentStatus: iPaymentStatus = {
      Q1: "Pending",
      Q2: "Pending",
      Q3: "Pending",
      Q4: "Pending",
      oneTime: "Pending"
    };

    b.Invoices.filter(inv => inv._vsd_programid_value === programId).forEach(inv => {
      let thisQuarter = this.findQuarter(inv.vsd_cpu_scheduledpaymentdate);
      paymentStatus[thisQuarter] = PaymentStatusDisplay[inv.statuscode];
    });

    return paymentStatus;
  }
  private findQuarter(date): string {
    let thisDate = moment(date);
    let quarter = PAYMENT_QUARTERS.find(q => q.month == thisDate.month() && q.day == thisDate.date());
    if (quarter) return quarter.quarter;
    return "oneTime";
  }
  private isCompleted(code: number): boolean {
    if (code === 1 || code === 2) {
      return true; // 1 = completed, 2 = cancelled
    } else {
      return false; // this is not completed
    }
  }
  private buildContracts(b: iDynamicsBlob): iContract[] {
    const contracts: iContract[] = [];
    if (b.Contracts.length > 0) {
      for (let contract of b.Contracts) {
        contracts.push({
          fiscalYearStart: contract.vsd_fiscalstartdate ? new Date(contract.vsd_fiscalstartdate).getFullYear() : 0,
          contractId: contract.vsd_contractid,
          contractNumber: contract.vsd_name,
          programs: this.buildPrograms(b, contract.vsd_contractid),
          status: contractStatus[contract.statuscode] || 'No Status',
          tasks: this.buildTasks(b, contract.vsd_contractid).filter(t => !t.isCompleted) || [],
          completedTasks: this.buildTasks(b, contract.vsd_contractid).filter(t => t.isCompleted) || [],
        });
      }
    }
    return contracts;
  }
  private buildMinistryContact(b: iDynamicsBlob): iMinistryUser {
    let mc = {
      firstName: b.MinistryUser.firstname,
      lastName: b.MinistryUser.lastname,
      email: b.MinistryUser.internalemailaddress,
      phone: b.MinistryUser.address1_telephone1,
    };
    mc.phone = mc.phone ? mc.phone.replace(/[\s()-]/g, '') : '';
    return mc;
  }
  private buildContactInformation(b: iDynamicsBlob): iContactInformation {
    // collect the organization meta and structure it into a new shape
    const ci: iContactInformation = {
      phoneNumber: b.Organization.telephone1 || null,
      emailAddress: b.Organization.emailaddress1 || null,
      faxNumber: b.Organization.fax || null,
      mainAddress: {
        city: b.Organization.address1_city || null,
        line1: b.Organization.address1_line1 || null,
        line2: b.Organization.address1_line2 || null,
        postalCode: b.Organization.address1_postalcode || null,
        province: b.Organization.address1_stateorprovince || null,
        country: 'Canada'
      }
    } as iContactInformation;

    // if there are any values in the returned data for the
    if (b.Organization && (b.Organization.address2_city || b.Organization.address2_line1 || b.Organization.address2_line2 || b.Organization.address2_postalcode || b.Organization.address2_stateorprovince)) {
      ci.mailingAddress = {
        city: b.Organization.address2_city || null,
        line1: b.Organization.address2_line1 || null,
        line2: b.Organization.address2_line2 || null,
        postalCode: b.Organization.address2_postalcode || null,
        province: b.Organization.address2_stateorprovince || null,
        country: 'Canada'
      };
      // ci.mailingAddressSameAsMainAddress = true;
    } else {
      ci.mailingAddress = {
        city: null,
        line1: null,
        line2: null,
        postalCode: null,
        province: null,
        country: 'Canada'
      };
      // ci.hasMailingAddress = false;
    }
    ci.mailingAddressSameAsMainAddress = _.isEqual(ci.mainAddress, ci.mailingAddress);
    if (ci.mailingAddressSameAsMainAddress) ci.mailingAddress = ci.mainAddress;

    if (b.Organization._vsd_executivecontactid_value)
      if (b.ExecutiveContact) ci.executiveContact = {
        email: b.ExecutiveContact.emailaddress1 || null,
        fax: b.ExecutiveContact.fax || null,
        firstName: b.ExecutiveContact.firstname || null,
        lastName: b.ExecutiveContact.lastname || null,
        middleName: b.ExecutiveContact.middlename || null,
        personId: b.ExecutiveContact.contactid || null,
        phone: b.ExecutiveContact.mobilephone || null,
        title: b.ExecutiveContact.jobtitle || null,
        address: {
          city: b.ExecutiveContact.address1_city || null,
          line1: b.ExecutiveContact.address1_line1 || null,
          line2: b.ExecutiveContact.address1_line2 || null,
          postalCode: b.ExecutiveContact.address1_postalcode || null,
          province: b.ExecutiveContact.address1_stateorprovince || null,
        } || null
      };

    // if there is a contact bound to this organization
    if (b.Organization._vsd_boardcontactid_value) {
      ci.boardContact = {
        email: b.BoardContact.emailaddress1 || null,
        fax: b.BoardContact.fax || null,
        firstName: b.BoardContact.firstname || null,
        lastName: b.BoardContact.lastname || null,
        middleName: b.BoardContact.middlename || null,
        personId: b.BoardContact.contactid || null,
        phone: b.BoardContact.mobilephone || null,
        title: b.BoardContact.jobtitle || null,
        address: {
          city: b.BoardContact.address1_city || null,
          line1: b.BoardContact.address1_line1 || null,
          line2: b.BoardContact.address1_line2 || null,
          postalCode: b.BoardContact.address1_postalcode || null,
          province: b.BoardContact.address1_stateorprovince || null,
        } || null
      };
      // save that this exists
      ci.hasBoardContact = true;
    } else {
      ci.hasBoardContact = false;
    }
    return ci;
  }
  private buildPersons(b: iDynamicsBlob): iPerson[] {
    const personList: iPerson[] = [];
    for (let p of b.Staff) {
      const person: iPerson = {
        address: {
          city: p.address1_city || null,
          line1: p.address1_line1 || null,
          line2: p.address1_line2 || null,
          postalCode: p.address1_postalcode || null,
          province: p.address1_stateorprovince || null,
          country: this.contactInformation.mainAddress.country || null,
        },
        email: p.emailaddress1 || null,
        fax: p.fax || null,
        firstName: p.firstname || null,
        lastName: p.lastname || null,
        middleName: p.middlename || null,
        personId: p.contactid || null,
        userId: p.vsd_bceid || null,
        phone: p.mobilephone || null,
        phoneExtension: p.vsd_mainphoneextension || null,
        phone2: p.telephone2 || null,
        phone2Extension: p.vsd_homephoneextension || null,
        title: p.jobtitle || null,
        employmentStatus: employmentStatusTypeDict[p.vsd_employmentstatus] || null,
        // if this person has the right value it is me.
        me: p.vsd_bceid ? true : false,
        // if the state code is zero or null the user is active
        deactivated: !p.statecode || p.statecode === 0 ? false : true || null,
      }
      if (_.isEqual(person.address, this.contactInformation.mainAddress)) {
        person.addressSameAsAgency = true;
      }
      personList.push(person);
    }
    return personList.sort((a: iPerson, b: iPerson) => {
      // same last name? sort by first name
      if (a.lastName === b.lastName) {
        // same first name? sort by middle name
        if (a.firstName === b.firstName) {
          // same middle name? just give up.
          if (a.middleName === b.middleName) {
            return 0
          }
          // sort by middle name
          if (a.middleName < b.middleName) {
            return -1;
          }
          if (a.middleName > a.middleName) {
            return 1;
          }
        }
        // sort by first name
        if (a.firstName < b.firstName) {
          return -1;
        }
        if (a.firstName > b.firstName) {
          return 1;
        }
      }
      // sort by last name
      if (a.lastName < b.lastName) {
        return -1;
      }
      if (a.lastName > b.lastName) {
        return 1;
      }
      // if there is an edge case return 0 so nothing breaks.
      return 0;
    });
  }
}
