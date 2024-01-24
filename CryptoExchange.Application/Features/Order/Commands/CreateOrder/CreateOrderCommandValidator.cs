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

        public CreateOrderCommandValidator(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;

            RuleFor(p => p.CurrencyId)
                .GreaterThan(0)
                .MustAsync(CurrencyMustExist)
                .WithMessage("{PropertyName} does not exist.");
        }

        private async Task<bool> CurrencyMustExist(int id, CancellationToken arg2)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);
            return currency != null;
        }
    }
}
