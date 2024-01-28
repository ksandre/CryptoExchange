using AutoMapper;
using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestList
{
    public class GetExchangeRequestListQueryHandler : IRequestHandler<GetExchangeRequestListQuery, List<ExchangeRequestListDto>>
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetExchangeRequestListQueryHandler(IExchangeRequestRepository exchangeRequestRepository, IMapper mapper, IUserService userService)
        {
            _exchangeRequestRepository = exchangeRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<List<ExchangeRequestListDto>> Handle(GetExchangeRequestListQuery request, CancellationToken cancellationToken)
        {
            var exchangeRequests = new List<Domain.ExchangeRequest>();
            var requests = new List<ExchangeRequestListDto>();

            exchangeRequests = await _exchangeRequestRepository.GetExchangeRequestsWithDetails();
            requests = _mapper.Map<List<ExchangeRequestListDto>>(exchangeRequests);
            foreach (var req in requests)
            {
                req.RequestedCustomer = await _userService.GetCustomer(req.RequestedCustomerId);
                req.ReceivedCustomer = await _userService.GetCustomer(req.ReceivedCustomerId);
            }

            return requests;
        }
    }
}
