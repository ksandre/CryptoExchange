using AutoMapper;
using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using CryptoExchange.Application.Features.Order.Commands.CreateOrder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.CreateExchangeRequest
{
    public class CreateExchangeRequestHandler : IRequestHandler<CreateExchangeRequestCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IExchangeRequestRepository _exchangeRequestRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public CreateExchangeRequestHandler(
            IMapper mapper,
            ICurrencyRepository currencyRepository,
            IExchangeRequestRepository exchangeRequestRepository,
            IOrdersRepository ordersRepository,
            IUserService userService,
            IMediator mediator
            )
        {
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _exchangeRequestRepository = exchangeRequestRepository;
            _ordersRepository = ordersRepository;
            _userService = userService;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateExchangeRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExchangeRequestValidator(_currencyRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Exchange Request", validationResult);

            // Get requesting customer's id
            var customerId = _userService.UserId;

            // Check customer's orders
            var order = await _ordersRepository.GetUserOrders(customerId, request.CurrencyToExchangeId);

            var orderForExchange = await _ordersRepository.GetUserOrders(customerId, request.CurrencyForExchangeId);
            if(orderForExchange == null)
            {
                await _mediator.Send(new CreateOrderCommand { 
                    Amount = 0,
                    CurrencyId = request.CurrencyForExchangeId,
                    CustomerId = customerId,
                });
            }

            // if orders aren't enough, return validation error with message
            if (order is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.CurrencyToExchangeId),
                    "You do not have any allocations for this currency type."));
                throw new BadRequestException("Invalid Exchange Request", validationResult);
            }

            double amountRequested = request.CurrencyToExchangeAmount;
            if (amountRequested > order.Amount)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                    nameof(request.CurrencyToExchangeAmount), "You do not have enough amount of currency for this request"));
                throw new BadRequestException("Invalid Exchange Request", validationResult);
            }

            // Create Exchange request
            var exchangeRequest = _mapper.Map<Domain.ExchangeRequest>(request);
            exchangeRequest.RequestedCustomerId = customerId;
            exchangeRequest.DateRequested = DateTime.Now.ToUniversalTime();
            await _exchangeRequestRepository.CreateAsync(exchangeRequest);

            return Unit.Value;
        }
    }
}
