using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.CancelExchangeRequest
{
    public class CancelExchangeRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
