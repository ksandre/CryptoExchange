using AutoMapper;
using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestDetail
{
    public class GetExchangeRequestDetailQueryHandler : IRequestHandler<GetExchangeRequestDetailQuery, ExchangeRequestDetailsDto>
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetExchangeRequestDetailQueryHandler(IExchangeRequestRepository exchangeRequestRepository, IMapper mapper, IUserService userService)
        {
            _exchangeRequestRepository = exchangeRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ExchangeRequestDetailsDto> Handle(GetExchangeRequestDetailQuery request, CancellationToken cancellationToken)
        {
            var exchangeRequest = _mapper.Map<ExchangeRequestDetailsDto>(await _exchangeRequestRepository.GetExchangeRequestWithDetails(request.Id));

            if (exchangeRequest == null)
                throw new NotFoundException(nameof(ExchangeRequest), request.Id);

            // Add Customer details as needed
            exchangeRequest.Customer = await _userService.GetCustomer(exchangeRequest.RequestingCustomerId);

            return exchangeRequest;
        }
    }
}
