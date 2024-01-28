using Microsoft.AspNetCore.SignalR;

namespace CryptoExchange.Api.Hubs
{
    public sealed class RelatedExchangeRequestsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} said: {message}");
        }
    }
}

// {"protocol":"json","version":1}

//{
//    "arguments" : ["Test Message"],
//    "invocationId" : "0",
//    "target" : "SendMessage",
//    "type" : 1
//}