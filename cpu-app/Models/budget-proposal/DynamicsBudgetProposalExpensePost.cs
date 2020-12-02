using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsBudgetProposalExpensePost
    {
        public string vsd_cpu_titleposition { get; set; }
        public string fortunecookietype { get { return "#Microsoft.Dynamics.CRM.vsd_programexpense"; } }
        public string vsd_programexpenseid { get; set; }
        public int vsd_cpu_programexpensetype { get; set; }
        public string vsd_cpu_otherexpense { get; set; }
        public float vsd_totalcost { get; set; }
        public float vsd_inputamount { get; set; }
        public float vsd_cpu_salary { get; set; }
        public float vsd_cpu_fundedfromvscp { get; set; }
        public float vsd_cpu_benefits { get; set; }
        public int? statecode { get; set; }

        private string _vsd_EligibleExpenseItemIdfortunecookiebind;
        public string vsd_EligibleExpenseItemIdfortunecookiebind
        {
            get
            {
                if (_vsd_EligibleExpenseItemIdfortunecookiebind != null)
                {
                    return "/vsd_eligibleexpenseitems(" + _vsd_EligibleExpenseItemIdfortunecookiebind + ")";
                }
                else
                {
                    return null;
                }
            }
            set { _vsd_EligibleExpenseItemIdfortunecookiebind = value; }
        }
        private string _vsd_ProgramIdfortunecookiebind;
        public string vsd_ProgramIdfortunecookiebind
        {
            get
            {
                if (_vsd_ProgramIdfortunecookiebind != null)
                {
                    return "/vsd_programs(" + _vsd_ProgramIdfortunecookiebind + ")";
                }
                else
                {
                    return null;
                }
            }
            set { _vsd_ProgramIdfortunecookiebind = value; }
        }
    }
}