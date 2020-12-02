using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsRegisterNewUserServiceProviderPost
    {
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.account"; } }
        public string name { get; set; }
        public string address1_city { get; set; }
        public string address1_composite { get; set; }
        public string address1_line1 { get; set; }
        public string address1_line2 { get; set; }
        public string address1_postalcode { get; set; }
        public string address1_stateorprovince { get; set; }
        public string emailaddress1 { get; set; }
        public string fax { get; set; }
        public string telephone1 { get; set; }
    }
}
