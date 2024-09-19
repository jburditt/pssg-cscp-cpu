using Manager;
using Manager.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ContractController(IHostApplicationLifetime applicationLifetime, ContractHandlers contractHandlers) : Controller
    {
        private readonly CancellationToken _cancellationToken = applicationLifetime.ApplicationStopping;

        [HttpGet("Clone")]
        public async Task<IActionResult> Clone()
        {
            var query = new ContractQuery();
            query.StateCode = StateCode.Active;
            query.StatusCode = ContractStatusCode.DulyExecuted;
            query.CpuCloneFlag = true;
            var result = await contractHandlers.Handle(query, _cancellationToken);

            Guid? id = null;
            foreach (var contract in result.Contracts)
            {
                // if not cloned
                if (!await contractHandlers.Handle(contract.Id, _cancellationToken))
                {
                    // clone the contract
                    id = await contractHandlers.Handle(contract, _cancellationToken);
                }
            }

            return Json(id);
        }
    }
}
