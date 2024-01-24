using AutoMapper;
using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
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
            var validator = new CreateOrderCommandValidator(_currencyRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Order Request", validationResult);

            // Get Currency for Orders
            var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);

            // Get Employees
            var employees = await _userService.GetEmployees();

            //Get Period
            var period = DateTime.Now.Year;

            //Assign Orders if an order doesn't already exist for period and currency
            var orders = new List<Domain.Order>();
            foreach (var emp in employees)
            {
                var oderExists = await _ordersRepository.OrderExists(emp.Id, request.CurrencyId);

                if (oderExists == false)
                {
                    orders.Add(new Domain.Order
                    {
                        Amount = request.Amount,
                        EmployeeId = emp.Id,
                        CurrencyId = currency.Id
                    });
                }
            }

            if (orders.Any())
            {
                await _ordersRepository.AddOrders(orders);
            }

            return Unit.Value;
        }
    }
}
