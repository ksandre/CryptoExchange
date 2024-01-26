using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Queries.GetOrderDetails
{
    public class GetOrderDetailQuery : IRequest<OrderDetailsDto>
    {
        public int Id { get; set; }
    }
}
