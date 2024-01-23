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
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task AddOrders(List<Order> orders)
        {
            await _context.AddRangeAsync(orders);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> OrderExists(string userId, int currencyId)
        {
            return await _context.Orders
                .AnyAsync(q => q.EmployeeId == userId && q.CurrencyId == currencyId);
        }

        public async Task<List<Order>> GetOrdersWithDetails()
        {
            var orders = await _context.Orders
               .Include(q => q.Currency)
               .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetOrdersWithDetails(string userId)
        {
            var orders = await _context.Orders
                .Where(q => q.EmployeeId == userId)
               .Include(q => q.Currency)
               .ToListAsync();

            return orders;
        }

        public async Task<Order> GetOrderWithDetails(int id)
        {
            var orders = await _context.Orders
                .Include(q => q.Currency)
                .FirstOrDefaultAsync(q => q.Id == id);

            return orders;
        }

        public async Task<Order> GetUserOrders(string userId, int currencyId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(q => q.EmployeeId == userId && q.CurrencyId == currencyId);
        }
    }
}
