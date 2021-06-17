using System.Threading.Tasks;
using Gateway.Services;
using KafkaLibrary.Dto;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Gateway.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SessionService _sessionService;
        private readonly MessageService _messageService;
        private readonly ILogger<ChatHub> _logger;
        private readonly UserConnectionService _userConnectionService;

        public ChatHub(
            ILogger<ChatHub> logger,
            SessionService sessionService, 
            MessageService messageService,
            UserConnectionService userConnectionService)
        {
            _userConnectionService = userConnectionService;
            _logger = logger;
            _sessionService = sessionService;
            _messageService = messageService;
        }

        public void Connect(string username)
        {
            var connectRequestDto= new SessionCreate
            {
                Name = username
            };
            _sessionService.SendConnection(connectRequestDto);
        }

        public async Task SendMessage(ChatMessage message)
        {
            _logger.LogDebug($"Received message from \"{Context.ConnectionId}\": {message.Message}");
            
            _userConnectionService.AddUser(message.FromUser, Context.ConnectionId);
            await _messageService.SendMessage(message);
        }
    }
}