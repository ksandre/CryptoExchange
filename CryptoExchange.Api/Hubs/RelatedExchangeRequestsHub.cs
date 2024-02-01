using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetRelatedExchangeRequestList;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CryptoExchange.Api.Hubs
{
    public sealed class RelatedExchangeRequestsHub : Hub
    {
        private readonly IMediator _mediator;

        public RelatedExchangeRequestsHub(IMediator mediator)
        {
           _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task<Task> SendMessageToCaller(
            double currencyToExchangeAmount, 
            double currencyForExchangeAmount, 
            int currencyToExchangeId,
            int currencyForExchangeId
            )
        {
            var exchangeRequests = await _mediator.Send(new GetRelatedExchangeRequestsListQuery
            {
                CurrencyToExchangeAmount = currencyToExchangeAmount,
                CurrencyForExchangeAmount = currencyForExchangeAmount,
                CurrencyToExchangeId = currencyToExchangeId,
                CurrencyForExchangeId = currencyForExchangeId
            });

            return Clients.Caller.SendAsync("ReceiveMessage", exchangeRequests);
        }
    }
}

public interface INotificationClient
{
    Task ReceiveNotification(string message);
    Task SendMessageToCaller(string message);
}

// {"protocol":"json","version":1}

//{
//    "arguments" : [7,2,1,2],
//    "invocationId" : "0",
//    "target" : "SendMessageToCaller",
//    "type" : 1
//}