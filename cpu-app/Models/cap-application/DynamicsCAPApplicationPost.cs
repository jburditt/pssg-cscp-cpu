using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class CAPApplicationPost
    {
        public string BusinessBCeID { get; set; }
        public string UserBCeID { get; set; }
        public DynamicsCAPApplicationContractPost[] ContractCollection { get; set; }
        public DynamicsCAPApplicationOrganizationPost Organization { get; set; }
        public DynamicsCAPApplicationProgramContactPost[] AddProgramContactCollection { get; set; }
        public DynamicsCAPApplicationProgramContactPost[] RemoveProgramContactCollection { get; set; }
        public DynamicsCAPApplicationProgramPost[] ProgramCollection { get; set; }
    }
}
