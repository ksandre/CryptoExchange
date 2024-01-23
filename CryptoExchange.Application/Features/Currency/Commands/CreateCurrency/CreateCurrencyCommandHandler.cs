using AutoMapper;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Commands.CreateCurrency
{
    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;

        public CreateCurrencyCommandHandler(IMapper mapper, ICurrencyRepository currencyRepository)
        {
            _mapper = mapper;
            _currencyRepository = currencyRepository;
        }

        public async Task<int> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new CreateCurrencyCommandValidator(_currencyRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave type", validationResult);

            // convert to domain entity object
            var currencyToCreate = _mapper.Map<Domain.Currency>(request);

            // add to database
            await _currencyRepository.CreateAsync(currencyToCreate);

            // retun record id
            return currencyToCreate.Id;
        }
    }
}
