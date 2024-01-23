using AutoMapper;
using CryptoExchange.Application.Contracts.Logging;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Commands.UpdateCurrency
{
    public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IAppLogger<UpdateCurrencyCommandHandler> _logger;

        public UpdateCurrencyCommandHandler(IMapper mapper, ICurrencyRepository currencyRepository, IAppLogger<UpdateCurrencyCommandHandler> logger)
        {
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new UpdateCurrencyCommandValidator(_currencyRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(Currency), request.Id);
                throw new BadRequestException("Invalid currency", validationResult);
            }

            // convert to domain entity object
            var currencyToUpdate = _mapper.Map<Domain.Currency>(request);

            // add to database
            await _currencyRepository.UpdateAsync(currencyToUpdate);

            // return Unit value (It means Empty)
            return Unit.Value;
        }
    }
}
