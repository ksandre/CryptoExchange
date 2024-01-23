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
                .Where(q => !string.IsNullOrEmpty(q.RequestingEmployeeId))
                .Include(q => q.Currency)
                .ToListAsync();

            return exchangeRequests;
        }

        public async Task<List<ExchangeRequest>> GetExchangeRequestsWithDetails(string userId)
        {
            var exchangeRequests = await _context.ExchangeRequests
                .Where(q => q.RequestingEmployeeId == userId)
                .Include(q => q.Currency)
                .ToListAsync();

            return exchangeRequests;
        }

        public async Task<ExchangeRequest> GetExchangeRequestWithDetails(int id)
        {
            var exchangeRequest = await _context.ExchangeRequests
                .Include(q => q.Currency)
                .FirstOrDefaultAsync(q => q.Id == id);

            return exchangeRequest;
        }
    }
}
