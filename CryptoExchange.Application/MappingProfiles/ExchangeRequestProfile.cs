using AutoMapper;
using CryptoExchange.Application.Features.ExchangeRequest.Commands.CreateExchangeRequest;
using CryptoExchange.Application.Features.ExchangeRequest.Commands.UpdateExchangeRequest;
using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestDetail;
using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestList;
using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetRelatedExchangeRequestList;
using CryptoExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.MappingProfiles
{
    public class ExchangeRequestProfile : Profile
    {
        public ExchangeRequestProfile()
        {
            CreateMap<ExchangeRequestListDto, ExchangeRequest>().ReverseMap();
            CreateMap<ExchangeRequestDetailsDto, ExchangeRequest>().ReverseMap();
            CreateMap<RelatedExchangeRequestsListDto, ExchangeRequest>().ReverseMap();
            CreateMap<CreateExchangeRequestCommand, ExchangeRequest>();
            CreateMap<UpdateExchangeRequestCommand, ExchangeRequest>();
        }
    }
}
