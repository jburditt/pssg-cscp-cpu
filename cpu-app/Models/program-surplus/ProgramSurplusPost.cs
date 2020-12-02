namespace Gov.Cscp.Victims.Public.Models
{
    public class ProgramSurplusPost
    {
        public string BusinessBCeID { get; set; }
        public string UserBCeID { get; set; }
        public DynamicsProgramSurplusLineItemPost[] SurplusPlanLineItemCollection { get; set; }
        public DynamicsProgramSurplus[] SurplusPlanCollection { get; set; }

    }
}
