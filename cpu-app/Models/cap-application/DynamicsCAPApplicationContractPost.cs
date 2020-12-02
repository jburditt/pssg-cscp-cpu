using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsCAPApplicationContractPost
    {
        public int? vsd_cpu_insuranceoptions { get; set; }
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_contract"; } }
        public string vsd_contractid { get; set; }
        public string vsd_name { get; set; }
        public string vsd_authorizedsigningofficersignature { get; set; }
        public string vsd_signingofficersname { get; set; }
        public string vsd_signingofficertitle { get; set; }
    }
}
