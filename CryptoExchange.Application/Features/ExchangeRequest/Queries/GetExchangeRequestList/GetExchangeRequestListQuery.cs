using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestList
{
    public record GetExchangeRequestListQuery : IRequest<List<ExchangeRequestListDto>>;
}
