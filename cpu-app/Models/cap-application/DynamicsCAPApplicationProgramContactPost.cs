using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsCAPApplicationProgramContactPost
    {
        public string contactid { get; set; }
        public string vsd_programid { get; set; }
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_contact_vsd_program"; } }
    }
}
