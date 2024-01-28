using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Queries.GetRelatedExchangeRequestList
{
    public class GetRelatedExchangeRequestsListQuery : IRequest<List<RelatedExchangeRequestsListDto>>
    {
        public double CurrencyToExchangeAmount { get; set; }
        public double CurrencyForExchangeAmount { get; set; }
        public int CurrencyToExchangeId { get; set; }
        public int CurrencyForExchangeId { get; set; }
    }
}
