using AutoMapper;
using CryptoExchange.Application.Contracts.Logging;
using CryptoExchange.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Queries.GetAllCurrencies
{
    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, List<CurrencyDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IAppLogger<GetCurrenciesQueryHandler> _logger;

        public GetCurrenciesQueryHandler(IMapper mapper,ICurrencyRepository currencyRepository,IAppLogger<GetCurrenciesQueryHandler> logger)
        {
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _logger = logger;
        }

        public async Task<List<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var currencies = await _currencyRepository.GetAsync();

            var data = _mapper.Map<List<CurrencyDto>>(currencies);

            // return list of DTO object
            _logger.LogInformation("Currencies types were retrieved successfully");
            return data;
        }
    }
}
