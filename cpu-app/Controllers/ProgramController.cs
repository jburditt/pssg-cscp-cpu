using Manager;
using Manager.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProgramController(CurrencyHandlers currencyHandler, ProgramHandlers programHandlers) : Controller
    {
        [HttpGet("Approved")]
        public IActionResult GetApproved()
        {
            // use CurrencyQuery to query "all the currencies that match this one currency code...don't ask me..."
            var currencyQuery = new CurrencyQuery();
            var currencyLookup = currencyHandler.Handle(currencyQuery);
            
            var invoiceDate = GetInvoiceDate();

            var emptyMessage = new ProgramResultEmptyMessage();
            var programs = programHandlers.Handle(emptyMessage);

            return new JsonResult(programs);
        }

        private DateTime GetInvoiceDate()
        {
            var firstQuarterDate = new DateTime(DateTime.Today.Year, 1, 15, DateTime.Today.Hour, DateTime.Today.Minute, DateTime.Today.Second, DateTimeKind.Local); //15th January
            var secondQuarterDate = new DateTime(DateTime.Today.Year, 4, 15, DateTime.Today.Hour, DateTime.Today.Minute, DateTime.Today.Second, DateTimeKind.Local); //15th April
            var thirdQuarterDate = new DateTime(DateTime.Today.Year, 7, 15, DateTime.Today.Hour, DateTime.Today.Minute, DateTime.Today.Second, DateTimeKind.Local); //15th July
            var fourthQuarterDate = new DateTime(DateTime.Today.Year, 10, 15, DateTime.Today.Hour, DateTime.Today.Minute, DateTime.Today.Second, DateTimeKind.Local); //15th October
            var fifthQuarterDate = new DateTime(DateTime.Today.Year + 1, 1, 15, DateTime.Today.Hour, DateTime.Today.Minute, DateTime.Today.Second, DateTimeKind.Local); //15th January next year

            if (DateTime.Today > firstQuarterDate && DateTime.Today <= secondQuarterDate)
            {
                return secondQuarterDate.AddDays(-3);
            }
            else if (DateTime.Today > secondQuarterDate && DateTime.Today <= thirdQuarterDate)
            {
                return thirdQuarterDate.AddDays(-3);
            }
            else if (DateTime.Today > thirdQuarterDate && DateTime.Today <= fourthQuarterDate)
            {
                return fourthQuarterDate.AddDays(-3);
            }
            else if (DateTime.Today <= firstQuarterDate)
            {
                return firstQuarterDate.AddDays(-3);
            }
            else
            {
                return fifthQuarterDate.AddDays(-3);
            }
        }
    }
}
