using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Warmur.Development.Test.Models;
using Warmur.Development.Test.Models.Leads;
using Warmur.Development.Test.Services;

namespace Warmur.Development.Test.Controllers
{
    public class LeadController(ILeadsService leadsService) : Controller
    {
        // GET: LeadController
        [HttpGet]
        [Route("[controller]/{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            var lead = await leadsService.GetLead(id);

            if (lead.IsSuccess)
            {
                var indicators = await leadsService.GetLeadIndicators(id);

                if (indicators.IsSuccess)
                {
                    var vm = new LeadViewModel
                    {
                        Name = lead.Value.Name,
                        Company = lead.Value.Company,
                        JobTitle = lead.Value.JobTitle,
                        Location = lead.Value.Location,
                        Indicators = indicators.Value.Items.Select(x => new IndicatorsViewModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Value = x.Value
                        }).ToList()
                    };
                    
                    return View(vm);
                }

                return Error();
            }

            return Error();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
