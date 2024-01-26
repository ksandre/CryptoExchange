using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.DeleteExchangeRequest
{
    internal class DeleteExchangeRequestCommandHandler : IRequestHandler<DeleteExchangeRequestCommand, Unit>
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepository;

        public DeleteExchangeRequestCommandHandler(IExchangeRequestRepository exchangeRequestRepository)
        {
            _exchangeRequestRepository = exchangeRequestRepository;
        }

        public async Task<Unit> Handle(DeleteExchangeRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _exchangeRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
                throw new NotFoundException(nameof(ExchangeRequest), request.Id);

            await _exchangeRequestRepository.DeleteAsync(leaveRequest);
            return Unit.Value;
        }
    }
}
