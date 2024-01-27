using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserService _userService;

        public CreateOrderCommandValidator(ICurrencyRepository currencyRepository,IUserService userService)
        {
            _currencyRepository = currencyRepository;
            _userService = userService;

            RuleFor(p => p.CurrencyId)
                .GreaterThan(0)
                .MustAsync(CurrencyMustExist)
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.CustomerId)
                .NotEmpty()
                .MustAsync(CustomerMustExist)
                .WithMessage("{PropertyName} does not exist.");
        }

        private async Task<bool> CurrencyMustExist(int id, CancellationToken arg2)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);
            return currency != null;
        }

        private async Task<bool> CustomerMustExist(string id, CancellationToken arg2)
        {
            var customer = await _userService.GetCustomer(id);
            return customer != null;
        }
    }
}
