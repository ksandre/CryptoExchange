using AutoMapper;
using CryptoExchange.Application.Contracts.Logging;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.UpdateExchangeRequest
{
    public class UpdateExchangeRequestHandler : IRequestHandler<UpdateExchangeRequestCommand, Unit>
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        public UpdateExchangeRequestHandler(
            IExchangeRequestRepository exchangeRequestRepository,
            ICurrencyRepository currencyRepository,
            IMapper mapper
            )
        {
            _exchangeRequestRepository = exchangeRequestRepository;
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateExchangeRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _exchangeRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
                throw new NotFoundException(nameof(ExchangeRequest), request.Id);


            var validator = new UpdateExchangeRequestValidator(_currencyRepository, _exchangeRequestRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Exchange Request", validationResult);

            _mapper.Map(request, leaveRequest);

            await _exchangeRequestRepository.UpdateAsync(leaveRequest);

            return Unit.Value;
        }
    }
}
