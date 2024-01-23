using CryptoExchange.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Commands.CreateCurrency
{
    public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CreateCurrencyCommandValidator(ICurrencyRepository currencyRepository)
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

            RuleFor(q => q)
                .MustAsync(CurrencyNameUnique)
                .WithMessage("Currency already exists");

            _currencyRepository = currencyRepository;
        }

        private Task<bool> CurrencyNameUnique(CreateCurrencyCommand command, CancellationToken token)
        {
            return _currencyRepository.IsCurrencyNameUnique(command.Name);
        }
    }
}
