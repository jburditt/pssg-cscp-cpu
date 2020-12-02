using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
	public class ExpenseReportPost
	{
		public string BusinessBCeID { get; set; }
		public string UserBCeID { get; set; }

		public DynamicsScheduleGCollectionPost[] ScheduleGCollection { get; set; }
		public DynamicsScheduleGLineItemCollectionPost[] ScheduleGLineItemCollection { get; set; }
	}
}
