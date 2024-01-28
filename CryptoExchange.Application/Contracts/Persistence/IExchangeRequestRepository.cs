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
        Task<List<ExchangeRequest>> GetExchangeRequestByUserIds(string userId1,string userId2);
        Task<List<ExchangeRequest>> GetRelatedExchangeRequests(double currencyToExchangeAmount, double currencyForExchangeAmount, int currencyToExchangeId, int currencyForExchangeId);
        Task<ExchangeRequest> GetRelatedExchangeRequestByUserIdsAndCurrencies(string userId1, string userId2, int currencyToExchangeId, int currencyForExchangeId);
    }
}
