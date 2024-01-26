using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestDetail
{
    public class GetExchangeRequestDetailQuery : IRequest<ExchangeRequestDetailsDto>
    {
        public int Id { get; set; }
    }
}
