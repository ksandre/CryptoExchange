using AutoMapper;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Queries.GetCurrencyDetails
{
    public class GetCurrencyDetailsQueryHandler : IRequestHandler<GetCurrencyDetailsQuery, CurrencyDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;

        public GetCurrencyDetailsQueryHandler(IMapper mapper, ICurrencyRepository currencyRepository)
        {
            _mapper = mapper;
            _currencyRepository = currencyRepository;
        }

        public async Task<CurrencyDetailsDto> Handle(GetCurrencyDetailsQuery request, CancellationToken cancellationToken)
        {
            // Query the database
            var currency = await _currencyRepository.GetByIdAsync(request.Id);

            // verify that record exists
            if (currency == null)
            {
                throw new NotFoundException(nameof(Currency), request.Id);
            }

            // convert data object to DTO object
            var data = _mapper.Map<CurrencyDetailsDto>(currency);

            // return DTO object
            return data;
        }
    }
}
