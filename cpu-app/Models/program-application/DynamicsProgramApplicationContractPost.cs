using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsProgramApplicationContractPost
    {
        private string _vsd_ContactLookup1fortunecookiebind;
        public string vsd_ContactLookup1fortunecookiebind
        {
            get
            {
                if (_vsd_ContactLookup1fortunecookiebind != null)
                {
                    return "/contacts(" + _vsd_ContactLookup1fortunecookiebind + ")";
                }
                else
                {
                    return null;
                }
            }
            set { _vsd_ContactLookup1fortunecookiebind = value; }
        }
        private string _vsd_ContactLookup2fortunecookiebind;
        public string vsd_ContactLookup2fortunecookiebind
        {
            get
            {
                if (_vsd_ContactLookup2fortunecookiebind != null)
                {
                    return "/contacts(" + _vsd_ContactLookup2fortunecookiebind + ")";
                }
                else
                {
                    return null;
                }
            }
            set { _vsd_ContactLookup2fortunecookiebind = value; }
        }
        public int? vsd_cpu_subcontractedprogramstaff { get; set; }
        public int? vsd_cpu_unionizedstaff { get; set; }
        public int? vsd_cpu_insuranceoptions { get; set; }
        public int? vsd_cpu_memberofcssea { get; set; }
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_contract"; } }
        public string vsd_contractid { get; set; }
        public string vsd_cpu_humanresourcepolices { get; set; }
        public string vsd_cpu_specificunion { get; set; }
        public string vsd_name { get; set; }
        public string vsd_authorizedsigningofficersignature { get; set; }
        public string vsd_signingofficersname { get; set; }
        public string vsd_signingofficertitle { get; set; }
    }
}
