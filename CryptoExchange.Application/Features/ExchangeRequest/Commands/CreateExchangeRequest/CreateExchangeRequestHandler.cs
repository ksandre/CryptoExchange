using AutoMapper;
using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
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

        public CreateExchangeRequestHandler(
            IMapper mapper,
            ICurrencyRepository currencyRepository,
            IExchangeRequestRepository exchangeRequestRepository,
            IOrdersRepository ordersRepository,
            IUserService userService
            )
        {
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _exchangeRequestRepository = exchangeRequestRepository;
            _ordersRepository = ordersRepository;
            _userService = userService;
        }

        public async Task<Unit> Handle(CreateExchangeRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExchangeRequestValidator(_currencyRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Exchange Request", validationResult);

            // Get requesting employee's id
            var employeeId = _userService.UserId;

            // Check on employee's allocation
            var order = await _ordersRepository.GetUserOrders(employeeId, request.CurrencyId);

            // if orders aren't enough, return validation error with message
            if (order is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.CurrencyId),
                    "You do not have any allocations for this currency type."));
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            double amountRequested = request.Amount;
            if (amountRequested > order.Amount)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                    nameof(request.Amount), "You do not have enough amount of currency for this request"));
                throw new BadRequestException("Invalid Exchange Request", validationResult);
            }

            // Create leave request
            var exchangeRequest = _mapper.Map<Domain.ExchangeRequest>(request);
            exchangeRequest.RequestingEmployeeId = employeeId;
            exchangeRequest.DateRequested = DateTime.Now;
            await _exchangeRequestRepository.CreateAsync(exchangeRequest);

            return Unit.Value;
        }
    }
}
