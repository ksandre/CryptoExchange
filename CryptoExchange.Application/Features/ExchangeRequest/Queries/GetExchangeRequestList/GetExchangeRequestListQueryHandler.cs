﻿using AutoMapper;
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
    internal class GetExchangeRequestListQueryHandler : IRequestHandler<GetExchangeRequestListQuery, List<ExchangeRequestListDto>>
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

            // Check if it is logged in employee
            if (request.IsLoggedInUser)
            {
                var userId = _userService.UserId;
                exchangeRequests = await _exchangeRequestRepository.GetExchangeRequestsWithDetails(userId);

                var employee = await _userService.GetEmployee(userId);
                requests = _mapper.Map<List<ExchangeRequestListDto>>(exchangeRequests);
                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                exchangeRequests = await _exchangeRequestRepository.GetExchangeRequestsWithDetails();
                requests = _mapper.Map<List<ExchangeRequestListDto>>(exchangeRequests);
                foreach (var req in requests)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }
            }

            return requests;
        }
    }
}