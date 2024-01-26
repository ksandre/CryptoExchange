using CryptoExchange.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Domain;

public class Order: BaseEntity
{
    public double Amount { get; set; }

    public Currency? Currency { get; set; }
    public int CurrencyId { get; set; }

    public string EmployeeId { get; set; } = string.Empty;
}
