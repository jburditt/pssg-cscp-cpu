namespace Manager.Contract;

public record PayloadCommand<TPayload>(TPayload Payload)
{
    public TPayload Payload { get; set; } = Payload;
}

public record class IdCommand(Guid Id)
{
    public Guid Id { get; set; } = Id;
}