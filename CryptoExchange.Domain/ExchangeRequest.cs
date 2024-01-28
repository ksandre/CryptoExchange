using CryptoExchange.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Domain;

public class ExchangeRequest : BaseEntity
{
    public DateTime DateRequested { get; set; }

    public string RequestedCustomerId { get; set; } = string.Empty;
    public double CurrencyToExchangeAmount { get; set; }
    public Currency? CurrencyToExchange { get; set; }
    public int CurrencyToExchangeId { get; set; }

    public string ReceivedCustomerId { get; set; } = string.Empty;
    public double CurrencyForExchangeAmount { get; set; }
    public Currency? CurrencyForExchange { get; set; }
    public int CurrencyForExchangeId { get; set; }

    public string? Comments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
}
