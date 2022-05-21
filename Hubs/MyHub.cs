using Microsoft.AspNetCore.SignalR;
using Web_API.Models;


namespace WebApi.Hubs
{
    public class MyHub : Hub
    {

        public async Task SendMessage(string to)
        {
            await Clients.All.SendAsync("MessageUpdate",to);
        }
        public async Task Invite(string to)
        {
            await Clients.All.SendAsync("InviteUpdate", to);
        }
        
    }
}
