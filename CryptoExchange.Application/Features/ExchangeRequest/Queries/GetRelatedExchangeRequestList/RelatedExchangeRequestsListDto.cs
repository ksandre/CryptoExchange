using CryptoExchange.Application.Features.Currency.Queries.GetAllCurrencies;
using CryptoExchange.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Queries.GetRelatedExchangeRequestList
{
    public class RelatedExchangeRequestsListDto
    {
        public int Id { get; set; }
        public Customer RequestedCustomer { get; set; }
        public string RequestedCustomerId { get; set; }
        public CurrencyDto CurrencyToExchange { get; set; }
        public int CurrencyToExchangeId { get; set; }
        public double CurrencyToExchangeAmount { get; set; }

        public Customer ReceivedCustomer { get; set; }
        public string ReceivedCustomerId { get; set; }
        public CurrencyDto CurrencyForExchange { get; set; }
        public int CurrencyForExchangeId { get; set; }
        public double CurrencyForExchangeAmount { get; set; }

        public DateTime DateRequested { get; set; }
        public bool? Approved { get; set; }
        public bool? Cancelled { get; set; }
    }
}
