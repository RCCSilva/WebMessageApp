using Gateway.Hubs;
using KafkaLibrary.Dto;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Gateway.Services
{
    public class ReceiveMessageService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserConnectionService _userConnectionService;
        private readonly ILogger<ReceiveMessageService> _logger;

        public ReceiveMessageService(
            IHubContext<ChatHub> hubContext, 
            UserConnectionService userConnectionService,
            ILogger<ReceiveMessageService> logger)
        {
            _hubContext = hubContext;
            _userConnectionService = userConnectionService;
            _logger = logger;
        }

        public async void ReceiveMessage(ChatMessage chatMessage)
        {
            var connectionId = _userConnectionService.GetConnectionId(chatMessage.ToUser);
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", chatMessage);
            _logger.LogDebug($"Message sent to {connectionId}");
        }
    }
}