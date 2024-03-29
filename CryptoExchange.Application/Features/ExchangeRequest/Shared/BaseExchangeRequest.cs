﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Shared
{
    public abstract class BaseExchangeRequest
    {
        public DateTime DateRequested { get; set; }
        public int CurrencyToExchangeId { get; set; }
        public double CurrencyToExchangeAmount { get; set; }
        public int CurrencyForExchangeId { get; set; }
        public double CurrencyForExchangeAmount { get; set; }
    }
}
