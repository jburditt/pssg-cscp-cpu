namespace Manager.Contract;

public class Task : IDto
{
    public Guid Id { get; set; }
    public StateCode StateCode { get; set; }
    public string? Subject { get; set; }
    public DateTime? ScheduledEnd { get; set; }

    // References
    public Guid? TaskTypeId { get; set; }
    public Guid? RegardingObjectId { get; set; }
    public Guid? ProgramId { get; set; }
    public Guid? ScheduleGId { get; set; }
}
