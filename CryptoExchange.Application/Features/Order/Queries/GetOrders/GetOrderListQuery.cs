using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Queries.GetOrders
{
    public class GetOrderListQuery : IRequest<List<OrderDto>>
    {
    }
}
