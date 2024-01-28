using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.ApproveExchangeRequest
{
    public class ApproveRelatedExchangeRequestsValidator : AbstractValidator<ApproveRelatedExchangeRequestsCommand>
    {
        public ApproveRelatedExchangeRequestsValidator()
        {
            RuleFor(p => p.exchangeRequestId1)
            .NotNull()
            .WithMessage("Id1 cannot be null");

            RuleFor(p => p.exchangeRequestId2)
            .NotNull()
            .WithMessage("Id2 cannot be null");
        }
    }
}
