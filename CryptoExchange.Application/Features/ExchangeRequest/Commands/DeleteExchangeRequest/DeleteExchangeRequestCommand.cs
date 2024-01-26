using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.DeleteExchangeRequest
{
    public class DeleteExchangeRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
