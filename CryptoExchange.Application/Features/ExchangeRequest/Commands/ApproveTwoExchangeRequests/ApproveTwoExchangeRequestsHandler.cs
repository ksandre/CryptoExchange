using AutoMapper;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using CryptoExchange.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.ApproveExchangeRequest
{
    public class ApproveTwoExchangeRequestsHandler : IRequestHandler<ApproveTwoExchangeRequestsCommand, Unit>
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public ApproveTwoExchangeRequestsHandler(
            IExchangeRequestRepository exchangeRequestRepository,
            ICurrencyRepository currencyRepository,
            IOrdersRepository ordersRepository,
            IMapper mapper
            )
        {
            _exchangeRequestRepository = exchangeRequestRepository;
            _currencyRepository = currencyRepository;
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(ApproveTwoExchangeRequestsCommand request, CancellationToken cancellationToken)
        {
            var exchangeRequest1 = await _exchangeRequestRepository.GetByIdAsync(request.exchangeRequestId1);
            var exchangeRequest2 = await _exchangeRequestRepository.GetByIdAsync(request.exchangeRequestId2);

            if (exchangeRequest1 is null || exchangeRequest2 is null)
                throw new NotFoundException(nameof(ExchangeRequest), $"Id1: {request.exchangeRequestId1}, Id2: {request.exchangeRequestId2}");


            if (exchangeRequest1.Approved != null || exchangeRequest2.Approved != null)
                throw new BadRequestException("Exchange already approved");

            // if request is not already approved, get and update the cutomer's orders

            double firstUserSellAmount = exchangeRequest1.CurrencyToExchangeAmount;
            double firstUserBuyAmount = exchangeRequest1.CurrencyForExchangeAmount;

            var firstUserSellCurrencyOrder = await _ordersRepository
                    .GetUserOrders(exchangeRequest1.RequestedCustomerId, exchangeRequest1.CurrencyToExchangeId);
            var firstUserBuyCurrencyOrder = await _ordersRepository
                    .GetUserOrders(exchangeRequest1.RequestedCustomerId, exchangeRequest1.CurrencyForExchangeId);

            firstUserSellCurrencyOrder.Amount -= firstUserSellAmount;
            firstUserBuyCurrencyOrder.Amount += firstUserBuyAmount;

            await _ordersRepository.UpdateAsync(firstUserSellCurrencyOrder);
            await _ordersRepository.UpdateAsync(firstUserBuyCurrencyOrder);

            //

            double secondUserSellAmount = exchangeRequest2.CurrencyToExchangeAmount;
            double secondUserBuyAmount = exchangeRequest2.CurrencyForExchangeAmount;

            var secondUserBuyCurrencyOrder = await _ordersRepository
                    .GetUserOrders(exchangeRequest2.RequestedCustomerId, exchangeRequest2.CurrencyToExchangeId);
            var secondUserSellCurrencyOrder = await _ordersRepository
                    .GetUserOrders(exchangeRequest2.RequestedCustomerId, exchangeRequest2.CurrencyForExchangeId);

            secondUserSellCurrencyOrder.Amount -= secondUserSellAmount;
            secondUserBuyCurrencyOrder.Amount += secondUserBuyAmount;

            await _ordersRepository.UpdateAsync(secondUserSellCurrencyOrder);
            await _ordersRepository.UpdateAsync(secondUserBuyCurrencyOrder);

            //

            exchangeRequest1.Approved = true;
            await _exchangeRequestRepository.UpdateAsync(exchangeRequest1);

            exchangeRequest2.Approved = true;
            await _exchangeRequestRepository.UpdateAsync(exchangeRequest2);

            return Unit.Value;
        }
    }
}
