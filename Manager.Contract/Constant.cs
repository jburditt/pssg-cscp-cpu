namespace Manager.Contract;

public class Constant
{
    // NOTE verified these are the same on DEV, and TEST. If they are different on PROD, these will need to be moved to secrets
    public static Guid QuarterlyScheduleG = new Guid("9d0ef880-e8f5-e911-b811-00505683fbf4");
    public static Guid ProvinceBc = new Guid("FDE4DBCA-989A-E811-8155-480FCFF4F6A1");
    public static Guid CadCurrency = new Guid("332fffff-f04b-e911-b80c-00505683fbf4");
}
