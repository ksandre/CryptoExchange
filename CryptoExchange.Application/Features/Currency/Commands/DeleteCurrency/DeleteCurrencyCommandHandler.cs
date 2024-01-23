using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, Unit>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public DeleteCurrencyCommandHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<Unit> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            // retrieve domain entity object
            var currencyToDelete = await _currencyRepository.GetByIdAsync(request.Id);

            // verify that record exists
            if (currencyToDelete == null)
                throw new NotFoundException(nameof(Currency), request.Id);

            // remove from database
            await _currencyRepository.DeleteAsync(currencyToDelete);

            // retun record id
            return Unit.Value;
        }
    }
}
