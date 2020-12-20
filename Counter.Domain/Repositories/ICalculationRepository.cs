using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Counter.Domain.Models;

namespace Counter.Domain.Repositories
{
    public interface ICalculationRepository
    {
        Task<int> GetCurrentTotal();
        Task UpdateTotalValue(TotalValueRecord newTotalValue);
    }
}
