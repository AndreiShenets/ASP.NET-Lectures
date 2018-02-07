using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using AspNetCoreSignalRApp.Hubs.Models;
using System;

namespace AspNetCoreSignalRApp.Hubs
{
    [Authorize]
    public class Chat : Hub
    {
        protected IUserTracker userTracker;

        public Chat(IUserTracker userTracker)
        {
            this.userTracker = userTracker ?? throw new ArgumentNullException(nameof(userTracker));
        }

        public override async Task OnConnectedAsync()
        {
            var currentUser = new UserDetails(Context.ConnectionId, Context.User.Identity.Name);
            userTracker.AddUser(currentUser);
            await OnUsersJoined(currentUser);

            await Clients.Client(Context.ConnectionId).InvokeAsync("SetUsersOnline", userTracker.GetUsers());

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var currentUser = userTracker.GetUser(Context.ConnectionId);
            userTracker.RemoveUser(currentUser);

            await OnUsersLeft(currentUser);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task OnUsersJoined(params UserDetails[] users)
        {
            await Clients.All.InvokeAsync("UsersJoined", new[] { users });
        }

        public async Task OnUsersLeft(params UserDetails[] users)
        {
            await Clients.All.InvokeAsync("UsersLeft", new[] { users });
        }

        public async Task Send(string message)
        {
            await Clients.All.InvokeAsync("Send", Context.User.Identity.Name, message);
        }
    }
}
