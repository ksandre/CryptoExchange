using AutoMapper;
using CryptoExchange.Application.Features.Currency.Commands.CreateCurrency;
using CryptoExchange.Application.Features.Currency.Queries.GetAllCurrencies;
using CryptoExchange.Application.Features.Currency.Queries.GetCurrencyDetails;
using CryptoExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.MappingProfiles
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<CurrencyDto, Currency>().ReverseMap();
            CreateMap<Currency, CurrencyDetailsDto>();
            CreateMap<CreateCurrencyCommand, Currency>();
        }
    }
}
