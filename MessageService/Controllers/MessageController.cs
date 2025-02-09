using Microsoft.AspNetCore.Mvc;
using MessageService.Models;
using MessageService.Repository;
using Microsoft.AspNetCore.SignalR;
using MessageService.Hubs;

namespace MessageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _repository;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger, IMessageRepository messageRepository,
                                IHubContext<MessageHub> hubContext)
        {
            _logger = logger;
            _repository = messageRepository;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            await _repository.AddMessage(message);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.Text);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory([FromQuery] DateTime time)
        {
            var messages = await _repository.GetMessages(time);
            return Ok(messages);
        }
    }
}
