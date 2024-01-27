using CryptoExchange.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Domain;

public class ExchangeRequest : BaseEntity
{
    public double Amount { get; set; }
    public DateTime DateRequested { get; set; }

    public Currency? Currency { get; set; }
    public int CurrencyTypeId { get; set; }

    public string? Comments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public string RequestingCustomerId { get; set; } = string.Empty;
}
