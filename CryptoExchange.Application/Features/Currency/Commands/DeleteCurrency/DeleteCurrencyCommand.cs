using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
