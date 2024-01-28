using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Features.ExchangeRequest.Commands.CreateExchangeRequest;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.Features.ExchangeRequest.Commands.CancelExchangeRequest
{
    public class CancelExchangeRequestValidator : AbstractValidator<CancelExchangeRequestCommand>
    {
        //private readonly IUserService _userService;

        //public CancelExchangeRequestValidator(IUserService userService)
        //{
        //    _userService = userService;
        //}
        //private async Task<bool> CustomerMustExist(string id, CancellationToken arg2)
        //{
        //    var customer = await _userService.GetCustomer(id);
        //    return customer != null;
        //}
    }
}
