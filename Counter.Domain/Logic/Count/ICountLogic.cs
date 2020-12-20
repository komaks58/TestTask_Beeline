using System.Threading.Tasks;
using Counter.Domain.Services.CounterService.Models;

namespace Counter.Domain.Logic.Count
{
    public interface ICountLogic
    {
        Task Count(CountRequest request);
    }
}
