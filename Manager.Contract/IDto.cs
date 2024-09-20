namespace Manager.Contract;

public interface IDto
{
    Guid Id { get; set; }
    StateCode StateCode { get; set; }
}
