using Application.Services.Repositories;
using Application.Services.UsersService;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    // .NET IOC burada da kullanılabilir halde dolayısıyla klasik bir controller gibi (canlı olması dışında) kullanılabilir.
    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

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