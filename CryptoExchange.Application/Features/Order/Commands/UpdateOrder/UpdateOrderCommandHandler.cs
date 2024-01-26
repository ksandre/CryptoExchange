using AutoMapper;
using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public UpdateOrderCommandHandler(IMapper mapper, IOrdersRepository ordersRepository, ICurrencyRepository currencyRepository)
        {
            _mapper = mapper;
            _ordersRepository = ordersRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOrderCommandValidator(_currencyRepository, _ordersRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Order", validationResult);

            var order = await _ordersRepository.GetByIdAsync(request.Id);

            if (order is null)
                throw new NotFoundException(nameof(Order), request.Id);

            _mapper.Map(request, order);

            await _ordersRepository.UpdateAsync(order);
            return Unit.Value;
        }
    }
}
