public partial class DatabaseContext
{
    private bool isInTransaction;

    public new void SaveChanges()
    {
        if (!isInTransaction)
        {
            base.SaveChanges();
        }
    }

    public TransactionContext BeginTransaction()
    {
        if (isInTransaction) throw new InvalidOperationException("Already in a transaction");
        isInTransaction = true;
        return new TransactionContext(this);
    }

    public void CommitTransaction()
    {
        if (isInTransaction)
        {
            base.SaveChanges();
            isInTransaction = false;
        }
    }
}

public class TransactionContext(DatabaseContext context)
{
    public void Commit() => context.CommitTransaction();
}
