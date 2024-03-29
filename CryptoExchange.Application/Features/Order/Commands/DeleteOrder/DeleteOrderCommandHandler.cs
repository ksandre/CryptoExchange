﻿using AutoMapper;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public DeleteOrderCommandHandler(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetByIdAsync(request.Id);

            if (order == null)
                throw new NotFoundException(nameof(Order), request.Id);

            await _ordersRepository.DeleteAsync(order);

            return Unit.Value;
        }
    }
}
