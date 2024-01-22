using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.Currency.Queries.GetCurrencyDetails
{
    public record GetCurrencyDetailsQuery(int Id) : IRequest<CurrencyDetailsDto>;
}
