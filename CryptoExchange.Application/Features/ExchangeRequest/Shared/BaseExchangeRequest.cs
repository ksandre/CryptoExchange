using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Shared
{
    public abstract class BaseExchangeRequest
    {
        public DateTime DateRequested { get; set; }
        public int CurrencyId { get; set; }
    }
}
