using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsCAPApplicationProgramPost
    {
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_program"; } }
        public string vsd_programid { get; set; }
        public float vsd_cpu_subtotalcomponentvalue { get; set; }
        public string vsd_cpu_programmodeltypes { get; set; }
        public string vsd_otherprogrammodels { get; set; }
        public int vsd_cpu_programevaluationefforts { get; set; }
        public string vsd_cpu_programevaluationdescription { get; set; }
        public string vsd_cpu_capprogramoperationscomments { get; set; }

        //program contact
        private string _vsd_ContactLookupfortunecookiebind;
        public string vsd_ContactLookupfortunecookiebind
        {
            get
            {
                if (!String.IsNullOrEmpty(_vsd_ContactLookupfortunecookiebind))
                {
                    return "/contacts(" + _vsd_ContactLookupfortunecookiebind + ")";
                }
                else
                {
                    return _vsd_ContactLookupfortunecookiebind;
                }
            }
            set { _vsd_ContactLookupfortunecookiebind = value; }
        }
    }
}