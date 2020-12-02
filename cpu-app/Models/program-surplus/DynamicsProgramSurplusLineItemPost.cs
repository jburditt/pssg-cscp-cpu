using System;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsProgramSurplusLineItemPost
    {
        public string vsd_justificationdetails { get; set; }
        public float? vsd_actualexpenditures { get; set; }
        public float? vsd_actualexpenditures2 { get; set; }
        public float? vsd_actualexpenditures3 { get; set; }
        public float? vsd_actualexpenditures4 { get; set; }
        public float? vsd_allocatedamount { get; set; }
        public float? vsd_proposedexpenditures { get; set; }
        public string vsd_surpluslineitemid { get; set; }
        public DateTime? vsd_datesubmitted { get; set; }
        // public string vsd_surplusplanid { get; set; }
        public string fortunecookietype { get { return "#Microsoft.Dynamics.CRM.vsd_surpluslineitem"; } }
    }
}
