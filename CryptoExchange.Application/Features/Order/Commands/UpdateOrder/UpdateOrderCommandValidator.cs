using CryptoExchange.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IOrdersRepository _ordersRepository;

        public UpdateOrderCommandValidator(ICurrencyRepository currencyRepository, IOrdersRepository ordersRepository)
        {
            _currencyRepository = currencyRepository;
            _ordersRepository = ordersRepository;

            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

            RuleFor(p => p.CurrencyId)
                .GreaterThan(0)
                .MustAsync(CurrencyMustExist)
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(OrderMustExist)
                .WithMessage("{PropertyName} must be present");
        }

        private async Task<bool> OrderMustExist(int id, CancellationToken arg2)
        {
            var leaveAllocation = await _ordersRepository.GetByIdAsync(id);
            return leaveAllocation != null;
        }

        private async Task<bool> CurrencyMustExist(int id, CancellationToken arg2)
        {
            var leaveType = await _currencyRepository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
