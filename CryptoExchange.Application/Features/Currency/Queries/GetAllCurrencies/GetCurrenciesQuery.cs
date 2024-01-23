using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Queries.GetAllCurrencies
{
    public record GetCurrenciesQuery : IRequest<List<CurrencyDto>>;
}
