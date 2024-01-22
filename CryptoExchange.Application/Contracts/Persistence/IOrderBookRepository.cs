using CryptoExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Contracts.Persistence
{
    public interface IOrderBookRepository: IGenericRepository<OrderBook>
    {
        Task<OrderBook> GetOrderBookWithDetails(int id);
        Task<List<OrderBook>> GetOrderBooksWithDetails();
        Task<List<OrderBook>> GetOrderBooksWithDetails(string userId);
        Task<bool> OrderBookExists(string userId, int leaveTypeId, int period);
        Task AddOrderBooks(List<OrderBook> orderBooks);
        Task<OrderBook> GetUserOrderBooks(string userId, int leaveTypeId);
    }
}
