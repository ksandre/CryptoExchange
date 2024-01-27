using CryptoExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Contracts.Persistence
{
    public interface IOrdersRepository: IGenericRepository<Order>
    {
        Task<Order> GetOrderWithDetails(int id);
        Task<List<Order>> GetOrdersWithDetails();
        Task<List<Order>> GetOrdersWithDetails(string userId);
        Task<bool> OrderExists(string userId, int currencyId);
        Task AddOrder(Order order);
        Task AddOrders(List<Order> orders);
        Task<Order> GetUserOrders(string userId, int currencyId);
    }
}
