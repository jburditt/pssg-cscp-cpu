using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class ProgramApplicationPost
    {
        public string BusinessBCeID { get; set; }
        public string UserBCeID { get; set; }
        public DynamicsProgramApplicationProgramContactPost[] AddProgramContactCollection { get; set; }
        public DynamicsProgramApplicationProgramContactPost[] RemoveProgramContactCollection { get; set; }
        public DynamicsProgramApplicationProgramContactPost[] AddProgramSubContractorCollection { get; set; }
        public DynamicsProgramApplicationProgramContactPost[] RemoveProgramSubContractorCollection { get; set; }
        public DynamicsProgramApplicationOrganizationPost Organization { get; set; }
        public DynamicsProgramApplicationContractPost[] ContractCollection { get; set; }
        public DynamicsProgramApplicationContactPost[] ContactCollection { get; set; }
        // public DynamicsProgramApplicationContactPost[] StaffCollection { get; set; }// doesn't get sent in the data
        public DynamicsProgramApplicationSchedulePost[] ScheduleCollection { get; set; }
        public DynamicsProgramApplicationProgramPost[] ProgramCollection { get; set; }
    }
}
