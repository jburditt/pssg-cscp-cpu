using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
	public class OrganizationPost
	{
		// this is the model that Dynamics expects back to update the organization level information
		public string BusinessBCeID { get; set; }
		public string UserBCeID { get; set; }
		public DynamicsOrganizationPost Organization { get; set; }
		public DynamicsOrganizationContactPost[] StaffCollection { get; set; }
	}
}
