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
                .GreaterThan(p => DateTime.Now).WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(p => p.CurrencyId)
                .GreaterThan(0)
                .MustAsync(CurrencyMustExist)
                .WithMessage("{PropertyName} does not exist.");
        }

        private async Task<bool> CurrencyMustExist(int id, CancellationToken arg2)
        {
            var leaveType = await _currencyRepository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
