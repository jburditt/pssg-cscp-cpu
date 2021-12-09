using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsScheduleGCollectionPost
    {
        public decimal vsd_actualhoursthisquarter { get; set; }
        public decimal vsd_contractedservicehrsthisquarter { get; set; }
        // public decimal vsd_cpu_numberofhours { get; set; }
        public decimal vsd_programadministrationcurrentquarter { get; set; }
        public decimal vsd_quarterlyvarianceprogramadministration { get; set; }
        public decimal vsd_yeartodateprogramadministration { get; set; }
        public decimal vsd_yeartodatevarianceprogramadministration { get; set; }

        public decimal vsd_programdeliverycurrentquarter { get; set; }
        public decimal vsd_quarterlyvarianceprogramdelivery { get; set; }
        public decimal vsd_yeartodateprogramdelivery { get; set; }
        public decimal vsd_yeartodatevarianceprogramdelivery { get; set; }

        public decimal vsd_salariesbenefitscurrentquarter { get; set; }
        public decimal vsd_quarterlyvariancesalariesbenefits { get; set; }
        public decimal vsd_yeartodatesalariesandbenefits { get; set; }
        public decimal vsd_yeartodatevariancesalariesbenefits { get; set; }

        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_scheduleg"; } }
        public string vsd_programadministrationexplanation { get; set; }
        public string vsd_programdeliveryexplanations { get; set; }
        public string vsd_salariesandbenefitsexplanation { get; set; }
        public string vsd_schedulegid { get; set; }
        public bool vsd_reportreviewed { get; set; }
    }
}
