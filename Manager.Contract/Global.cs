namespace Manager.Contract;

#region Fake MediatR

// NOTE delete this region if you end up using MediatR
public interface IRequestHandler<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public interface IRequest<TResponse> { }

#endregion

public enum StateCode
{
    Active = 0,
    Inactive = 1
}

public enum StatusCode
{
    Active = 0,
    Inactive = 1
}

public enum CpuInvoiceType
{
    Deprecated = 100000002,
    OneTimePayment = 100000001,
    ScheduledPayment = 100000000,
}

public enum InvoiceType
{
    CounsCourtPsychEd = 100000000,
    DoNotUseMedicalSessions = 100000002,
    OtherPayments = 100000001,
}

public enum ProgramUnit
{
    Cpu = 100000003,
    Csu = 100000002,
    Cvap = 100000000,
    Gangs = 100000005,
    Rest = 100000004,
    Vsu = 100000001,
}
