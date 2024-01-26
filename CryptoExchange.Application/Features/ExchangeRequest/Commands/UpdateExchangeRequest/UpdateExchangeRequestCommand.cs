using CryptoExchange.Application.Features.ExchangeRequest.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.UpdateExchangeRequest
{
    public class UpdateExchangeRequestCommand : BaseExchangeRequest, IRequest<Unit>
    {
        public int Id { get; set; }
        public string Comments { get; set; } = string.Empty;
        public bool Cancelled { get; set; }
    }
}
