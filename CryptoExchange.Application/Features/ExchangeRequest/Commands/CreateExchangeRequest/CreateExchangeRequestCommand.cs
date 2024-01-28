using CryptoExchange.Application.Features.ExchangeRequest.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.CreateExchangeRequest
{
    public class CreateExchangeRequestCommand : BaseExchangeRequest, IRequest<Unit>
    {
        public string Comments { get; set; } = string.Empty;
    }
}
