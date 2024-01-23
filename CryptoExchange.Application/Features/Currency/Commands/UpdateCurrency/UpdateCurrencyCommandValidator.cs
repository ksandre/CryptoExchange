using CryptoExchange.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Commands.UpdateCurrency
{
    public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public UpdateCurrencyCommandValidator(ICurrencyRepository currencyRepository)
        {
            RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(CurrencyMustExist);

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

            _currencyRepository = currencyRepository;
        }

        private async Task<bool> CurrencyMustExist(int id, CancellationToken arg2)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);
            return currency != null;
        }
    }
}
