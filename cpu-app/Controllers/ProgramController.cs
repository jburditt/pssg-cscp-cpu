using Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProgramController(CurrencyHandlers currencyHandler) : Controller
    {
        [HttpGet("Approved")]
        public IActionResult GetApproved()
        {
            var currencyLookup = currencyHandler.Handle();
            //var invoiceDate = GetInvoiceDate();
            //var programs = _programManager.GetApprovedPrograms();

            return new JsonResult(currencyLookup);
        }
    }
}
