namespace Resources;

public class TaskRepository : BaseRepository<Database.Model.Task, Manager.Contract.Task>, ITaskRepository
{
    public TaskRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper) { }
}
