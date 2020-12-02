using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsProgramApplicationProgramPost
    {
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_program"; } }
        public bool vsd_costshare { get; set; }
        public bool vsd_cpu_programstaffsubcontracted { get; set; }
        public int? vsd_cpu_numberofhours { get; set; }
        public int? vsd_cpu_per { get; set; }
        public int? vsd_totaloncallstandbyhours { get; set; }
        public int? vsd_totalscheduledhours { get; set; }
        public string vsd_addressline1 { get; set; }
        public string vsd_addressline2 { get; set; }
        public string vsd_city { get; set; }
        public string vsd_country { get; set; }
        public string vsd_emailaddress { get; set; }
        public string vsd_fax { get; set; }
        public string vsd_governmentfunderagency { get; set; }
        public string vsd_mailingaddressline1 { get; set; }
        public string vsd_mailingaddressline2 { get; set; }
        public string vsd_mailingcity { get; set; }
        public string vsd_mailingcountry { get; set; }
        public string vsd_mailingpostalcodezip { get; set; }
        public string vsd_mailingprovincestate { get; set; }
        public string vsd_phonenumber { get; set; }
        public string vsd_postalcodezip { get; set; }
        public string vsd_programid { get; set; }
        public string vsd_provincestate { get; set; }
        public bool vsd_addresstransitionorsafehome { get; set; }

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

        //police contact
        private string _vsd_ContactLookup2fortunecookiebind;
        public string vsd_ContactLookup2fortunecookiebind
        {
            get
            {
                if (!String.IsNullOrEmpty(_vsd_ContactLookup2fortunecookiebind))
                {
                    return "/contacts(" + _vsd_ContactLookup2fortunecookiebind + ")";
                }
                else
                {
                    return _vsd_ContactLookup2fortunecookiebind;
                }
            }
            set { _vsd_ContactLookup2fortunecookiebind = value; }
        }

        //shared cost contact
        private string _vsd_ContactLookup3fortunecookiebind;
        public string vsd_ContactLookup3fortunecookiebind
        {
            get
            {
                if (!String.IsNullOrEmpty(_vsd_ContactLookup3fortunecookiebind))
                {
                    return "/contacts(" + _vsd_ContactLookup3fortunecookiebind + ")";
                }
                else
                {
                    return _vsd_ContactLookup3fortunecookiebind;
                }
            }
            set { _vsd_ContactLookup3fortunecookiebind = value; }
        }
    }
}