using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace Gov.Cscp.Victims.Public.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class DynamicsBlobController : Controller
	{
		private readonly IDynamicsResultService _dynamicsResultService;

		public DynamicsBlobController(IDynamicsResultService dynamicsResultService)
		{
			this._dynamicsResultService = dynamicsResultService;
		}

		[HttpGet("{businessBceid}/{userBceid}")]
		public async Task<IActionResult> GetBlob(string userBceid, string businessBceid)
		{
			try
			{
				// convert the parameters to a json string
				string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
				// set the endpoint action
				string endpointUrl = "vsd_GetCPUOrgContracts";

				// get the response
				DynamicsResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

				return StatusCode((int)result.statusCode, result.result.ToString());
			}
			finally { }
		}
	}
}