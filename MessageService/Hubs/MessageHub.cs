using Microsoft.AspNetCore.SignalR;
using MessageService.Models;

namespace MessageService.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{message.Timestamp} : {message.Text}");
        }
    }
}
