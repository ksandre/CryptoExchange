using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.ApproveExchangeRequest
{
    public class ApproveTwoExchangeRequestsCommand : IRequest<Unit>
    {
        public int exchangeRequestId1 { get; set; }
        public int exchangeRequestId2 { get; set; }
    }
}
