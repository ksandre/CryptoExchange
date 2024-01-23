using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Commands.CreateCurrency
{
    public class CreateCurrencyCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
