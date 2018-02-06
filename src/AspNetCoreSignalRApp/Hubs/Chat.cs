using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreSignalRApp.Hubs
{
    [Authorize]
    public class Chat : Hub
    {
        public Chat()
        {

        }
        
        public async Task Send(string message)
        {
            await Clients.All.InvokeAsync("Send", Context.User.Identity.Name, message);
        }
    }
}
