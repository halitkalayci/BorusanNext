using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs;

public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        var x = Context.ConnectionId;
       // Clients.Group("MüşteriHizmetleri").SendAsync("NewConnection");
        return base.OnConnectedAsync();
    }
    public async Task SendMessage(string message)
    {
        // Client.X
        await Clients.Others.SendAsync("NewMessage", message);
    }
}
// Iyzico, ElasticSearch