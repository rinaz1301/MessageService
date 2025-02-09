using Microsoft.AspNetCore.SignalR;

namespace MessageService.ChatHub
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string text, int sequenceNumber, DateTime timestamp)
        {
            await Clients.All.SendAsync("ReceiveMessage", text, sequenceNumber, timestamp);
        }
    }
}
