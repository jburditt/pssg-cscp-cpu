using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
	public class DynamicsProgramApplicationOrganizationPost
	{
		public string _ownerid_value { get; set; }
		public string accountid { get; set; }
		public string address1_city { get; set; }
		public string address1_composite { get; set; }
		public string address1_country { get; set; }
		public string address1_line1 { get; set; }
		public string address1_line2 { get; set; }
		public string address1_postalcode { get; set; }
		public string address1_stateorprovince { get; set; }
		public string address2_city { get; set; }
		public string address2_composite { get; set; }
		public string address2_country { get; set; }
		public string address2_line1 { get; set; }
		public string address2_line2 { get; set; }
		public string address2_postalcode { get; set; }
		public string address2_stateorprovince { get; set; }
		public string emailaddress1 { get; set; }
		public string fax { get; set; }
		public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.account"; } }
		public string name { get; set; }
		public string telephone1 { get; set; }
		private string _vsd_ExecutiveContactIdfortunecookiebind;

		public string vsd_ExecutiveContactIdfortunecookiebind
		{
			get
			{
				if (_vsd_ExecutiveContactIdfortunecookiebind != null)
				{
					return "/contacts(" + _vsd_ExecutiveContactIdfortunecookiebind + ")";
				}
				else
				{
					return null;
				}
			}
			set { _vsd_ExecutiveContactIdfortunecookiebind = value; }
		}
		private string _vsd_BoardContactIdfortunecookiebind;
		public string vsd_BoardContactIdfortunecookiebind
		{
			get
			{
				if (_vsd_BoardContactIdfortunecookiebind != null)
				{
					return "/contacts(" + _vsd_BoardContactIdfortunecookiebind + ")";
				}
				else
				{
					return null;
				}
			}
			set { _vsd_BoardContactIdfortunecookiebind = value; }
		}

	}
}
