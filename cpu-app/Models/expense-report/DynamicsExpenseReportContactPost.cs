using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsExpenseReportContact
    {
        public string _parentcustomerid_value { get; set; }
        public string address1_city { get; set; }
        public string address1_composite { get; set; }
        public string address1_line1 { get; set; }
        public string address1_line2 { get; set; }
        public string address1_postalcode { get; set; }
        public string address1_stateorprovince { get; set; }
        public string contactid { get; set; }
        public string emailaddress1 { get; set; }
        public string fax { get; set; }
        public string firstname { get; set; }
        public string fullname { get; set; }
        public string jobtitle { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public string mobilephone { get; set; }
        public string vsd_mainphoneextension { get; set; }
        public string telephone2 { get; set; }
        public string vsd_homephoneextension { get; set; }
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.contact"; } }
        public string vsd_bceid { get; set; }
        // statecode is an optional status value. 1 is soft delete and null shouldn't be included.
        private int? _statecode;
        public int? statecode
        {
            get
            {
                if (_statecode.HasValue)
                {
                    return _statecode.Value;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _statecode = value;
            }
        }
    }
}
