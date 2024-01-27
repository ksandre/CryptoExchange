using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Unit>
    {
        public int Amount { get; set; }
        public int CurrencyId { get; set; }
        public string CustomerId { get; set; } = string.Empty;
    }
}
