namespace Manager;

public class TaskHandlers(ITaskRepository repository) :
    BaseHandlers<ITaskRepository, Contract.Task>(repository),
    IRequestHandler<InsertCommand<Contract.Task>, Guid>
{

}
