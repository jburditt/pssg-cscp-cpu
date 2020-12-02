using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
	public class MonthlyStatisticsAnswers
	{
		public string BusinessBCeID { get; set; }
		public string UserBCeID { get; set; }
		// public int ReportingPeriod { get; set; }
		public DynamicsDataCollectionLineItemPost[] AnswerCollection { get; set; }
	}
}
