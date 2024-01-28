using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.CancelExchangeRequest
{
    public class CancelExchangeRequestHandler : IRequestHandler<CancelExchangeRequestCommand, Unit>
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepository;
        private readonly IOrdersRepository _ordersRepository;

        public CancelExchangeRequestHandler(IExchangeRequestRepository exchangeRequestRepository, IOrdersRepository ordersRepository)
        {
            _exchangeRequestRepository = exchangeRequestRepository;
            _ordersRepository = ordersRepository;
        }

        public async Task<Unit> Handle(CancelExchangeRequestCommand request, CancellationToken cancellationToken)
        {
            var exchangeRequest = await _exchangeRequestRepository.GetByIdAsync(request.Id);

            if (exchangeRequest is null)
                throw new NotFoundException(nameof(exchangeRequest), request.Id);

            exchangeRequest.Cancelled = true;
            await _exchangeRequestRepository.UpdateAsync(exchangeRequest);

            // if any of exchange requests already approved, re-evaluate the customer's orders for the currency
            if (exchangeRequest.Approved == true)
            {
                double amountRequested = exchangeRequest.CurrencyToExchangeAmount;
                var firstUserOrder = await _ordersRepository.GetUserOrders(exchangeRequest.RequestedCustomerId, exchangeRequest.CurrencyToExchangeId);
                firstUserOrder.Amount += amountRequested;

                double amountRecieved = exchangeRequest.CurrencyForExchangeAmount;
                var secondUserOrder = await _ordersRepository.GetUserOrders(exchangeRequest.ReceivedCustomerId, exchangeRequest.CurrencyForExchangeId);
                secondUserOrder.Amount += amountRecieved;

                await _ordersRepository.UpdateAsync(firstUserOrder);
                await _ordersRepository.UpdateAsync(secondUserOrder);
            }

            return Unit.Value;
        }
    }
}
