namespace Resources;

public interface IScheduleGRepository
{
    // CRUD
    //Guid Insert(Contract contract);
    //Guid Upsert(Contract contract);
    //FindContractResult FirstOrDefault(FindContractQuery paymentQuery);
    ScheduleGResult Query(ScheduleGQuery paymentQuery);
    //bool Delete(Guid id);

    // Custom queries

}