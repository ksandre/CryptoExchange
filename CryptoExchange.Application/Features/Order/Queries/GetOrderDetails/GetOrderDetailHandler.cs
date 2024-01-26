using AutoMapper;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Queries.GetOrderDetails
{
    public class GetOrderDetailHandler : IRequestHandler<GetOrderDetailQuery, OrderDetailsDto>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public GetOrderDetailHandler(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public async Task<OrderDetailsDto> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetOrderWithDetails(request.Id);
            if (order == null)
                throw new NotFoundException(nameof(Order), request.Id);

            return _mapper.Map<OrderDetailsDto>(order);
        }
    }
}
