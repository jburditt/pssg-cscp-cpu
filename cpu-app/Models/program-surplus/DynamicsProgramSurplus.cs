using System;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsProgramSurplus
    {
        public string vsd_surplusplanreportid { get; set; }
        public bool vsd_surplusremittance { get; set; }
        public DateTime? vsd_datesubmitted { get; set; }
        // public string vsd_surplusplanid { get; set; }
        public string fortunecookietype { get { return "#Microsoft.Dynamics.CRM.vsd_surplusplanreport"; } }
    }
}
