using Gov.Cscp.Victims.Public.Background;
using Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ScheduleGController(
        IBackgroundTaskQueue taskQueue, IHostApplicationLifetime applicationLifetime, ProgramHandlers programHandlers, ContractHandlers contractHandlers
    ) : Controller
    {
        private readonly CancellationToken _cancellationToken = applicationLifetime.ApplicationStopping;

        [HttpGet("CreateScheduleGTasks/{quarter}")]
        public async Task<IActionResult> CreateScheduleGTasks(int quarter)
        {
            return Json(quarter);
        }
    }
}
