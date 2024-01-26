using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int CurrencyId { get; set; }
    }
}
