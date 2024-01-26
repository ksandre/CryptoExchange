using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestList
{
    public class GetExchangeRequestListQuery : IRequest<List<ExchangeRequestListDto>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
