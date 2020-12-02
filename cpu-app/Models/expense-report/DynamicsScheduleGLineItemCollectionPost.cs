using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
	public class DynamicsScheduleGLineItemCollectionPost
	{
		public float vsd_actualexpensescurrentquarter { get; set; }
		public float vsd_quarterlyvariance { get; set; }
		public float vsd_actualexpendituresyeartodate { get; set; }
		public float vsd_yeartodatevariance { get; set; }
		public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_scheduleglineitem"; } }
		public string vsd_scheduleglineitemid { get; set; }
	}
}
