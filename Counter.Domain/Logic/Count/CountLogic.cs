using System;
using System.Threading.Tasks;
using Counter.Domain.Models;
using Counter.Domain.Repositories;
using Counter.Domain.Services.CounterService.Models;

namespace Counter.Domain.Logic.Count
{
    public class CountLogic : ICountLogic
    {
        private readonly ICalculationRepository _calculationRepository;
        public CountLogic(ICalculationRepository calculationRepository)
        {
            _calculationRepository = calculationRepository ?? throw new ArgumentNullException(nameof(calculationRepository));
        }

        public async Task Count(CountRequest request)
        {
            var currentTotalValue = await _calculationRepository.GetCurrentTotal();

            await _calculationRepository.UpdateTotalValue(new TotalValueRecord
            {
                CalculationOperationType = request.CalculationType,
                Value = request.InputValue,
                TotalValue = currentTotalValue + request.InputValue
            });
        }
    }
}