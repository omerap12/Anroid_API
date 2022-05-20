using Microsoft.AspNetCore.SignalR;
using Web_API.Models;


namespace WebApi.Hubs
{
    public class MyHub : Hub
    {
        public async Task SendMessage(string from, string to, string content)
        {


            await Clients.All.SendAsync("MessageUpdate","Send: "+from+" to "+to+" -> "+content);
        }

    }
}
