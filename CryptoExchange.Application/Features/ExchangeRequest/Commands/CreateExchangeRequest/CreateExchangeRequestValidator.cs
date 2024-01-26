using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Features.ExchangeRequest.Shared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.CreateExchangeRequest
{
    public class CreateExchangeRequestValidator : AbstractValidator<CreateExchangeRequestCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CreateExchangeRequestValidator(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
            Include(new BaseExchangeRequestValidator(_currencyRepository));
        }
    }
}
