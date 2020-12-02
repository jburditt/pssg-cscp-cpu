using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsBudgetProposalRevenueSourcePost
    {
        public string fortunecookietype { get { return "#Microsoft.Dynamics.CRM.vsd_programrevenuesource"; } }
        public string vsd_programrevenuesourceid { get; set; }
        public string vsd_cpu_otherrevenuesource { get; set; }
        public int vsd_cpu_revenuesourcetype { get; set; }
        public float? vsd_cashcontribution { get; set; }
        public float? vsd_inkindcontribution { get; set; }
        public int? statecode { get; set; }

        private string _vsd_ProgramIdfortunecookiebind;
        public string vsd_ProgramIdfortunecookiebind
        {
            // executive contact on schedule f
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