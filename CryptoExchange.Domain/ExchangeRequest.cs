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

    public Currency? Currency { get; set; }

    public int CurrencyTypeId { get; set; }

    public string? Comments { get; set; }

    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public string RequestingEmployeeId { get; set; } = string.Empty;
}
