using Microsoft.AspNetCore.SignalR;
using Web_API.Models;


namespace WebApi.Hubs
{
    public class MyHub : Hub
    {

        public async Task SendMessage(string from,string to)
        {
            await Clients.All.SendAsync("MessageUpdate",from,to);
        }
        public async Task Invite(string to)
        {
            Console.WriteLine(to);
            await Clients.All.SendAsync("InviteUpdate", to);
        }
         
        
    }
}
