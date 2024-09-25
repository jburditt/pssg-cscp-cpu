namespace Manager.Contract;

public interface IDto
{
    public Guid Id { get; set; }
    public StateCode StateCode { get; set; }
}
