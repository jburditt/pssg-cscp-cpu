using Database.ProxyClasses;

public partial class DatabaseContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
{
    public DatabaseContext(Microsoft.Xrm.Sdk.IOrganizationService service) : base(service) { }

    /// <summary>
    /// Gets a binding to the set of all <see cref="Document"/> entities.
    /// </summary>
    public IQueryable<Document> DocumentSet
    {
        get
        {
            return this.CreateQuery<Document>();
        }
    }
}
