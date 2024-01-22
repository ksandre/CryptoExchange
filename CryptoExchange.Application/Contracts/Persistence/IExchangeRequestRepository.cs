using CryptoExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Contracts.Persistence
{
    public interface IExchangeRequestRepository : IGenericRepository<ExchangeRequest>
    {
        Task<ExchangeRequest> GetExchangeRequestWithDetails(int id);
        Task<List<ExchangeRequest>> GetExchangeRequestsWithDetails();
        Task<List<ExchangeRequest>> GetExchangeRequestsWithDetails(string userId);
    }
}
