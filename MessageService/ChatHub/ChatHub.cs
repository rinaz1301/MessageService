using Microsoft.AspNetCore.SignalR;

namespace MessageService.ChatHub
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string text, int sequenceNumber)
        {
            await Clients.All.SendAsync("ReceiveMessage", text, sequenceNumber, DateTime.UtcNow);
        }
    }
}
