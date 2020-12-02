using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsBudgetProposalProgramPost
    {
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_program"; } }
        public string vsd_programid { get; set; }
        public string vsd_signingofficersignature { get; set; }
        public string vsd_signingofficerfullname { get; set; }
        public string vsd_signingofficertitle { get; set; }
    }
}
