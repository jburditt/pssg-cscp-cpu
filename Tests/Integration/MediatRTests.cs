using Manager;
using MediatR;
using Task = System.Threading.Tasks.Task;

public class MediatRTests(IMediator mediator)
{
    [Fact]
    public async Task Send()
    {
        var command = new InsertCommand<ScheduleG>(new ScheduleG());
        var response = await mediator.Send(command);
    }
}

