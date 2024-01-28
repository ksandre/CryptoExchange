using CryptoExchange.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Shared
{
    public class BaseExchangeRequestValidator : AbstractValidator<BaseExchangeRequest>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public BaseExchangeRequestValidator(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;

            RuleFor(p => p.DateRequested)
                .LessThanOrEqualTo(p => DateTime.Now).WithMessage("{PropertyName} must be after or same as {ComparisonValue}");

            RuleFor(p => p.CurrencyToExchangeId)
                .GreaterThan(0)
                .MustAsync(CurrencyMustExist)
                .WithMessage("{PropertyName} does not exist.");
            RuleFor(p => p.CurrencyToExchangeAmount)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(p => p.CurrencyForExchangeId)
                .GreaterThan(0)
                .MustAsync(CurrencyMustExist)
                .WithMessage("{PropertyName} does not exist.");
            RuleFor(p => p.CurrencyForExchangeAmount)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }

        private async Task<bool> CurrencyMustExist(int id, CancellationToken arg2)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);
            return currency != null;
        }
    }
}
