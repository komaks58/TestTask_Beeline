using System;
using System.Threading.Tasks;
using System.Web.Http;
using Counter.Dal.Repositories;
using Counter.Domain.Logic.Count;
using Counter.Domain.Repositories;
using Counter.Domain.Services.CounterService;
using Counter.Domain.Services.CounterService.Models;

namespace Counter.Api.Controllers
{
    public class CounterController : ApiController, ICounterService
    {
        private readonly ICalculationRepository _calculationRepository;
        private readonly ICountLogic _countLogic;

        public CounterController(ICalculationRepository calculationRepository, ICountLogic countLogic)
        {
            _calculationRepository = calculationRepository ?? throw new ArgumentNullException(nameof(calculationRepository));
            _countLogic = countLogic ?? throw new ArgumentNullException(nameof(countLogic));
        }

        [HttpGet]
        public async Task<int> GetCurrentTotalValue()
        {
            var currentTotalValue = await _calculationRepository.GetCurrentTotal();
            return currentTotalValue;
        }

        [HttpPost]
        public async Task Count(CountRequest request)
        {
            await _countLogic.Count(request);
        }
    }
}
