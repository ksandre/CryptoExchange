using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Features.ExchangeRequest.Shared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.UpdateExchangeRequest
{
    public class UpdateExchangeRequestValidator : AbstractValidator<UpdateExchangeRequestCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IExchangeRequestRepository _exchangeRequestRepository;

        public UpdateExchangeRequestValidator(ICurrencyRepository currencyRepository, IExchangeRequestRepository exchangeRequestRepository)
        {
            _currencyRepository = currencyRepository;
            _exchangeRequestRepository = exchangeRequestRepository;

            Include(new BaseExchangeRequestValidator(_currencyRepository));

            RuleFor(p => p.Id)
                    .NotNull()
                    .MustAsync(ExchangeRequestMustExist)
                    .WithMessage("{PropertyName} must be present");
        }

        private async Task<bool> ExchangeRequestMustExist(int id, CancellationToken arg2)
        {
            var leaveAllocation = await _exchangeRequestRepository.GetByIdAsync(id);
            return leaveAllocation != null;
        }
    }
}
