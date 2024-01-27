using CryptoExchange.Application.Features.Currency.Queries.GetAllCurrencies;
using CryptoExchange.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestList
{
    public class ExchangeRequestListDto
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string RequestingCustomerId { get; set; }
        public CurrencyDto Currency { get; set; }
        public DateTime DateRequested { get; set; }
        public bool? Approved { get; set; }
        public bool? Cancelled { get; set; }
    }
}
