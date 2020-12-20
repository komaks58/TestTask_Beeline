using System.Threading.Tasks;
using Counter.Domain.Services.CounterService.Models;

namespace Counter.Domain.Services.CounterService
{
    public interface ICounterService
    {
        Task<int> GetCurrentTotalValue();

        Task Count(CountRequest request);
    }
}
