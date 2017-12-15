using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Uno.Api.Repository;
using Uno.Api.Streams;
using Uno.ValueObjects;

namespace Uno.Api.Hubs
{
    public class UnoHub : Hub
    {
        private readonly IUserRepository repository;
        private readonly TalonStream streamTalon;

        public UnoHub(IUserRepository repository, TalonStream streamTalon)
        {
            this.streamTalon = streamTalon;
            this.repository = repository;
        }

        public async Task Connect(string nom)
        {
            repository.AjouterUtilisateur(Context.ConnectionId, nom);
            await Clients.Client(Context.ConnectionId).InvokeAsync("connected", Context.ConnectionId);
        }

        public IObservable<Carte> StreamTalon()
        {
            return streamTalon.StreamTalon();
        }
    }
}