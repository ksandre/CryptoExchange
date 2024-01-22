using CryptoExchange.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Domain;

public class Currency : BaseEntity
{
    public string Name {  get; set; } = string.Empty;
}
