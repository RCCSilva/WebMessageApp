using System.Threading.Tasks;
using Gateway.Dto;
using Gateway.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Gateway.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SessionService _sessionService;
        private readonly MessageService _messageService;
        private readonly ILogger<ChatHub> _logger;
        private readonly UserDictService _userDictService;

        public ChatHub(
            ILogger<ChatHub> logger,
            SessionService sessionService, 
            MessageService messageService,
            UserDictService userDictService)
        {
            _userDictService = userDictService;
            _logger = logger;
            _sessionService = sessionService;
            _messageService = messageService;
        }

        public void Connect(string username)
        {
            var connectRequestDto= new ConnectionRequest
            {
                Name = username
            };
            _sessionService.SendConnection(connectRequestDto);
        }

        public async Task SendMessage(ChatMessage message)
        {
            _logger.LogDebug($"Received message from \"{Context.ConnectionId}\": {message.Message}");
            
            _userDictService.AddUser(message.FromUser, Context.ConnectionId);
            await _messageService.SendMessage(message);
        }
    }
}