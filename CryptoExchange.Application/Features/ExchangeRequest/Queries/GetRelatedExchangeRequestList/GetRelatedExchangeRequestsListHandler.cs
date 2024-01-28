using AutoMapper;
using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Queries.GetRelatedExchangeRequestList
{
    public class GetRelatedExchangeRequestsListHandler : IRequestHandler<GetRelatedExchangeRequestsListQuery, List<RelatedExchangeRequestsListDto>>
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetRelatedExchangeRequestsListHandler(IExchangeRequestRepository exchangeRequestRepository, IMapper mapper, IUserService userService)
        {
            _exchangeRequestRepository = exchangeRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<List<RelatedExchangeRequestsListDto>> Handle(GetRelatedExchangeRequestsListQuery request, CancellationToken cancellationToken)
        {
            var exchangeRequests = new List<Domain.ExchangeRequest>();
            var requests = new List<RelatedExchangeRequestsListDto>();

            exchangeRequests = await _exchangeRequestRepository.GetRelatedExchangeRequests(
                request.CurrencyToExchangeAmount, request.CurrencyForExchangeAmount, request.CurrencyToExchangeId, request.CurrencyForExchangeId);

            requests = _mapper.Map<List<RelatedExchangeRequestsListDto>>(exchangeRequests);

            foreach (var req in requests)
            {
                req.RequestedCustomer = await _userService.GetCustomer(req.RequestedCustomerId);
                req.ReceivedCustomer = await _userService.GetCustomer(req.ReceivedCustomerId);
            }

            return requests;
        }
    }
}
