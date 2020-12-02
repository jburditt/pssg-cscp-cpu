using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class BudgetProposalPost
    {
        public string BusinessBCeID { get; set; }
        public string UserBCeID { get; set; }
        public DynamicsBudgetProposalExpensePost[] ProgramExpenseCollection { get; set; }
        public DynamicsBudgetProposalRevenueSourcePost[] ProgramRevenueSourceCollection { get; set; }
        public DynamicsBudgetProposalProgramPost[] ProgramCollection { get; set; }

    }
}
