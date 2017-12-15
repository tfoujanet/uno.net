using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Uno.Api.Repository;

namespace Uno.Api.Hubs
{
    public class UnoHub : Hub
    {
        private readonly IUserRepository repository;

        public UnoHub(IUserRepository repository)
        {
            this.repository = repository;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).InvokeAsync("connected", Context.ConnectionId);
        }
    }
}