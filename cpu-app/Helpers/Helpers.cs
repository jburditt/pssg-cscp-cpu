using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Helpers
{
    public class Helpers
    {
        public static string updateFortunecookieBindNull(string modelString)
        {
            string ret = "";
            string toCheck = modelString;
            var regex = new System.Text.RegularExpressions.Regex("\"[a-zA-Z0-9_]*fortunecookiebind\"");
            var matches = regex.Matches(modelString);

            foreach (var match in matches)
            {
                string key = match.ToString();
                int index = toCheck.IndexOf(key);
                string check = toCheck.Substring(index + key.Length, 5);

                if (check.Equals(":null"))
                {
                    string val = key.Replace("fortunecookiebind", "_value").ToLower();
                    val = val.Insert(1, "_");
                    var regex2 = new System.Text.RegularExpressions.Regex(System.Text.RegularExpressions.Regex.Escape(key));
                    toCheck = regex2.Replace(toCheck, val, 1);
                }
                else
                {
                    ret += toCheck.Substring(0, index + key.Length);
                    toCheck = toCheck.Substring(index + key.Length);
                }
            }

            ret += toCheck;

            ret = ret.Replace(",\"statecode\":null", "");
            ret = ret.Replace("\"statecode\":null,", "");
            ret = ret.Replace("\"statecode\":null", "");

            ret = ret.Replace(",\"statuscode\":null", "");
            ret = ret.Replace("\"statuscode\":null,", "");
            ret = ret.Replace("\"statuscode\":null", "");
            return ret;
        }

        public static string removeNullsForProgramApplication(string modelString)
        {
            string ret = modelString.Replace("\"AddProgramContactCollection\":null,", "");
            ret = ret.Replace("\"AddProgramContactCollection\":null", "");
            ret = ret.Replace("\"RemoveProgramContactCollection\":null,", "");
            ret = ret.Replace("\"RemoveProgramContactCollection\":null", "");
            ret = ret.Replace("\"RemoveProgramSubContractorCollection\":null,", "");
            ret = ret.Replace("\"RemoveProgramSubContractorCollection\":null", "");
            ret = ret.Replace("\"Organization\":null,", "");
            ret = ret.Replace("\"Organization\":null", "");
            ret = ret.Replace("\"ProgramCollection\":null,", "");
            ret = ret.Replace("\"ProgramCollection\":null", "");
            ret = ret.Replace("\"ContractCollection\":null,", "");
            ret = ret.Replace("\"ContractCollection\":null", "");
            ret = ret.Replace("\"ContactCollection\":null,", "");
            ret = ret.Replace("\"ContactCollection\":null", "");
            ret = ret.Replace("\"ScheduleCollection\":null,", "");
            ret = ret.Replace("\"ScheduleCollection\":null", "");
            ret = ret.Replace("\"vsd_scheduleid\":null,", "");
            ret = ret.Replace("\"vsd_scheduleid\":null", "");
            ret = ret.Replace("\"vsd_bceid\":null,", "");
            ret = ret.Replace("\"contactid\":null,", "");

            ret = ret.Replace("\"vsd_ContactLookup2fortunecookiebind\":\"\",", "");
            ret = ret.Replace("\"vsd_ContactLookup2fortunecookiebind\":\"\"", "");

            ret = ret.Replace("\"vsd_ContactLookup3fortunecookiebind\":\"\",", "");
            ret = ret.Replace("\"vsd_ContactLookup3fortunecookiebind\":\"\"", "");

            // if (ret.Contains("\"_vsd_contactlookup2_value\":null") && ret.Contains("vsd_contactlookup2="))
            // {
            //     ret = ret.Replace("\"_vsd_contactlookup2_value\":null,", "");
            //     ret = ret.Replace("\"_vsd_contactlookup2_value\":null", "");
            // }

            // if (ret.Contains("\"_vsd_contactlookup3_value\":null") && ret.Contains("vsd_contactlookup3="))
            // {
            //     ret = ret.Replace("\"_vsd_contactlookup3_value\":null,", "");
            //     ret = ret.Replace("\"_vsd_contactlookup3_value\":null", "");
            // }

            ret = ret.Replace(",}", "}");

            return ret;
        }

        public static string removeNullsForStaffUpdate(string modelString)
        {
            string ret = modelString.Replace("\"_parentcustomerid_value\":null,", "");
            ret = ret.Replace("\"Organization\":null,", "");
            ret = ret.Replace("\"Organization\":null", "");
            ret = ret.Replace("\"vsd_bceid\":null,", "");
            ret = ret.Replace("\"contactid\":null,", "");
            return ret;
        }

        public static string removeNullsForBudgetProposal(string modelString)
        {
            string ret = modelString.Replace("\"ProgramExpenseCollection\":[],", "");
            ret = ret.Replace("\"ProgramExpenseCollection\":[]", "");
            ret = ret.Replace("\"ProgramRevenueSourceCollection\":[],", "");
            ret = ret.Replace("\"ProgramRevenueSourceCollection\":[]", "");
            ret = ret.Replace("\"vsd_programrevenuesourceid\":null,", "");
            ret = ret.Replace("\"vsd_programexpenseid\":null,", "");
            return ret;
        }

    }
}
