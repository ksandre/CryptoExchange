﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.Application.Models.Identity
{
    public class Customer
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
