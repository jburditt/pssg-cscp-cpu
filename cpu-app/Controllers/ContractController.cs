using Manager;
using Manager.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ContractController(IHostApplicationLifetime applicationLifetime, ContractHandlers contractHandlers) : Controller
    {
        private readonly CancellationToken _cancellationToken = applicationLifetime.ApplicationStopping;

        public async Task<IActionResult> Clone()
        {
            var query = new ContractQuery();
            query.StateCode = StateCode.Active;
            query.StatusCode = ContractStatusCode.DulyExecuted;
            query.CpuCloneFlag = true;
            var result = await contractHandlers.Handle(query, _cancellationToken);

            foreach (var contract in result.Contracts)
            {
                // if not cloned
                if (!await contractHandlers.Handle(contract.Id, _cancellationToken))
                {
                    //OrganizationRequest req = new OrganizationRequest("vsd_CloneContract");
                    //req["Target"] = contractEntity.ToEntityReference();
                    //var resp = OrgService.Execute(req);
                }
            }

            return View();
        }
    }
}
