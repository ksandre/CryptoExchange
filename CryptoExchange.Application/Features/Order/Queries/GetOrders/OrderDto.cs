using CryptoExchange.Application.Features.Currency.Queries.GetAllCurrencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Queries.GetOrders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }

        public CurrencyDto? Currency { get; set; }
        public int CurrencyId { get; set; }
    }
}
