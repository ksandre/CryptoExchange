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

namespace CryptoExchange.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserService _userService;

        public CreateOrderCommandHandler(
            IMapper mapper,
            IOrdersRepository ordersRepository,
            ICurrencyRepository currencyRepository,
            IUserService userService
            )
        {
            _mapper = mapper;
            _ordersRepository = ordersRepository;
            _currencyRepository = currencyRepository;
            _userService = userService;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderCommandValidator(_currencyRepository,_userService);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Order Request", validationResult);

            var order = new Domain.Order
            {
                Amount = request.Amount,
                CurrencyId = request.CurrencyId,
                CustomerId = request.CustomerId,
            };

            await _ordersRepository.AddOrder(order);

            return Unit.Value;
        }
    }
}
