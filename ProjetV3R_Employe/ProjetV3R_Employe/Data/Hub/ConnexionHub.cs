using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ProjetV3R_Employe.SignalR
{
    public class ConnectionHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> UserConnections = new();

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connecté : {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (user != null)
            {
                UserConnections.TryRemove(user, out _);
            }
            Console.WriteLine($"Client déconnecté : {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }

        public Task RegisterUser(string email)
        {
            UserConnections[email] = Context.ConnectionId;
            Console.WriteLine($"Utilisateur enregistré : {email} -> {Context.ConnectionId}");
            return Task.CompletedTask;
        }

        public string? GetConnectionId(string email)
        {
            UserConnections.TryGetValue(email, out var connectionId);
            return connectionId;
        }
    }
}
