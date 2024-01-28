using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Domain;
using CryptoExchange.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Persistence.Repositories
{
    public class ExchangeRequestRepository : GenericRepository<ExchangeRequest>, IExchangeRequestRepository
    {
        public ExchangeRequestRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<List<ExchangeRequest>> GetExchangeRequestsWithDetails()
        {
            var exchangeRequests = await _context.ExchangeRequests
                .Where(q => !string.IsNullOrEmpty(q.RequestedCustomerId))
                .Include(q => q.CurrencyToExchange)
                .Include(q => q.CurrencyForExchange)
                .ToListAsync();

            return exchangeRequests;
        }

        public async Task<List<ExchangeRequest>> GetExchangeRequestsWithDetails(string userId)
        {
            var exchangeRequests = await _context.ExchangeRequests
                .Where(q => q.RequestedCustomerId == userId)
                .Include(q => q.CurrencyToExchange)
                .Include(q => q.CurrencyForExchange)
                .ToListAsync();

            return exchangeRequests;
        }

        public async Task<ExchangeRequest> GetExchangeRequestWithDetails(int id)
        {
            var exchangeRequest = await _context.ExchangeRequests
                .Include(q => q.CurrencyToExchange)
                .Include(q => q.CurrencyForExchange)
                .FirstOrDefaultAsync(q => q.Id == id);

            return exchangeRequest;
        }

        public async Task<List<ExchangeRequest>> GetExchangeRequestByUserIds(string userId1,string userId2)
        {
            var exchangeRequests = await _context.ExchangeRequests
                .Where(q => q.RequestedCustomerId == userId1)
                .Where(q => q.ReceivedCustomerId == userId2)
                .Include(q => q.CurrencyToExchange)
                .Include(q => q.CurrencyForExchange)
                .ToListAsync();

            return exchangeRequests;
        }

        public async Task<List<ExchangeRequest>> GetRelatedExchangeRequests(
            double currencyToExchangeAmount,
            double currencyForExchangeAmount,
            int currencyToExchangeId,
            int currencyForExchangeId
            )
        {
            // We are getting related first request.

            var exchangeRequest = await _context.ExchangeRequests
                .Where(q => (q.CurrencyToExchangeId == currencyToExchangeId && q.CurrencyForExchangeId == currencyForExchangeId) || (q.CurrencyToExchangeId == currencyForExchangeId && q.CurrencyForExchangeId == currencyToExchangeId))
                .Where(q => (q.CurrencyToExchangeAmount == currencyToExchangeAmount && q.CurrencyForExchangeAmount == currencyForExchangeAmount) || (q.CurrencyToExchangeAmount == currencyForExchangeAmount && q.CurrencyForExchangeAmount == currencyToExchangeAmount))
                .Include(q => q.CurrencyToExchange)
                .Include(q => q.CurrencyForExchange)
                .ToListAsync();

            return exchangeRequest;
        }

        public async Task<ExchangeRequest> GetRelatedExchangeRequestByUserIdsAndCurrencies(
            string userId1,
            string userId2,
            int currencyToExchangeId,
            int currencyForExchangeId
            )
        {
            // We are getting related first second users request.

            var exchangeRequest = await _context.ExchangeRequests
                .Where(q => q.RequestedCustomerId == userId2)
                .Where(q => q.ReceivedCustomerId == userId1)
                .Where(q => q.CurrencyToExchangeId == currencyToExchangeId)
                .Where(q => q.CurrencyForExchangeId == currencyForExchangeId)
                .Include(q => q.CurrencyToExchange)
                .Include(q => q.CurrencyForExchange)
                .FirstOrDefaultAsync();

            return exchangeRequest;
        }
    }
}
