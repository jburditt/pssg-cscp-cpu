using Microsoft.Extensions.DependencyInjection;
using Gov.Cscp.Victims.Public;

namespace Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHandlers();
    }
}
