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
    public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<bool> IsCurrencyNameUnique(string name)
        {
            return await _context.Currencies.AnyAsync(q => q.Name == name) == false;
        }
    }
}
