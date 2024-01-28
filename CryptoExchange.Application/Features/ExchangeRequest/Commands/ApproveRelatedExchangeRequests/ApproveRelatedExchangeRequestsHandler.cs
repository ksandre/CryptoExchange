using AutoMapper;
using CryptoExchange.Application.Contracts.Identity;
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
    public class ApproveRelatedExchangeRequestsHandler : IRequestHandler<ApproveRelatedExchangeRequestsCommand, Unit>
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ApproveRelatedExchangeRequestsHandler(
            IExchangeRequestRepository exchangeRequestRepository,
            ICurrencyRepository currencyRepository,
            IOrdersRepository ordersRepository,
            IUserService userService,
            IMapper mapper
            )
        {
            _exchangeRequestRepository = exchangeRequestRepository;
            _currencyRepository = currencyRepository;
            _ordersRepository = ordersRepository;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(ApproveRelatedExchangeRequestsCommand request, CancellationToken cancellationToken)
        {
            var exchangeRequest1 = await _exchangeRequestRepository.GetByIdAsync(request.exchangeRequestId1);
            var exchangeRequest2 = await _exchangeRequestRepository.GetByIdAsync(request.exchangeRequestId2);

            if (exchangeRequest1 is null || exchangeRequest2 is null)
                throw new NotFoundException(nameof(ExchangeRequest), $"Id1: {request.exchangeRequestId1}, Id2: {request.exchangeRequestId2}");


            if (exchangeRequest1.Approved != null || exchangeRequest2.Approved != null)
                throw new BadRequestException("Exchange already approved");

            // if request is not already approved, get and update the cutomer's orders

            await UpdateCurstomerOrders(exchangeRequest1, exchangeRequest2);
            await UpdateCurstomerOrders(exchangeRequest2, exchangeRequest1);

            return Unit.Value;
        }

        private async Task UpdateCurstomerOrders(Domain.ExchangeRequest exchangeRequest, Domain.ExchangeRequest exchangeRequest2)
        {
            double userSellAmount = exchangeRequest.CurrencyToExchangeAmount;
            double userBuyAmount = exchangeRequest.CurrencyForExchangeAmount;

            var userSellCurrencyOrder = await _ordersRepository
                    .GetUserOrder(exchangeRequest.RequestedCustomerId, exchangeRequest.CurrencyToExchangeId);
            var userBuyCurrencyOrder = await _ordersRepository
                    .GetUserOrder(exchangeRequest.RequestedCustomerId, exchangeRequest.CurrencyForExchangeId);

            userSellCurrencyOrder.Amount -= userSellAmount;
            userBuyCurrencyOrder.Amount += userBuyAmount;

            var receivedCustomer = await _userService.GetCustomer(exchangeRequest2.RequestedCustomerId);
            exchangeRequest.ReceivedCustomerId = receivedCustomer.Id;

            await _ordersRepository.UpdateAsync(userSellCurrencyOrder);
            await _ordersRepository.UpdateAsync(userBuyCurrencyOrder);

            exchangeRequest.Approved = true;
            await _exchangeRequestRepository.UpdateAsync(exchangeRequest);
        }
    }
}
