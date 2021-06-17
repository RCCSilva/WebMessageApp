using Gateway.Dto;
using Gateway.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Gateway.Services
{
    public class ReceiveMessageService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserDictService _userDictService;
        private readonly ILogger<ReceiveMessageService> _logger;

        public ReceiveMessageService(
            IHubContext<ChatHub> hubContext, 
            UserDictService userDictService,
            ILogger<ReceiveMessageService> logger)
        {
            _hubContext = hubContext;
            _userDictService = userDictService;
            _logger = logger;
        }

        public async void ReceiveMessage(ChatMessage chatMessage)
        {
            var connectionId = _userDictService.GetConnectionId(chatMessage.ToUser);
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", chatMessage);
            _logger.LogDebug($"Message sent to {connectionId}");
        }
    }
}