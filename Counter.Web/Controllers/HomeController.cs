using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Counter.Domain.Gateways.CounterServiceAgent;
using Counter.Domain.Services.CounterService.Models;
using Microsoft.Ajax.Utilities;

namespace Counter.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICounterServiceAgent _counterServiceAgent;

        public HomeController(ICounterServiceAgent counterServiceAgent)
        {
            _counterServiceAgent = counterServiceAgent ?? throw new ArgumentNullException(nameof(counterServiceAgent));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> UpdateCurrentValue()
        {
            string currentTotalValue;

            try
            {
                var res = await _counterServiceAgent.GetCurrentTotalValue();
                currentTotalValue = res.ToString();
            }
            catch (Exception e)
            {
                currentTotalValue = "Ошибка при запросе";
            }

            return currentTotalValue;
        }

        [HttpPost]
        public async Task Count(int value, CalculationOperationType calculationType)
        {
            try
            {
                await _counterServiceAgent.Count(new CountRequest
                {
                    InputValue = value,
                    CalculationType = calculationType
                });
            }
            catch (Exception e)
            {

            }
        }
    }
}