using Microsoft.AspNetCore.Mvc;
using MessageService.Model;
using MessageService.Repository;

namespace MessageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _repository;
        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger, IMessageRepository messageRepository)
        {
            _logger = logger;
            _repository = messageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            await _repository.AddMessage(message);
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
