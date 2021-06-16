using System.Threading.Tasks;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;
        private readonly SessionService _sessionService;

        public SessionController(ILogger<SessionController> logger, SessionService sessionService)
        {
            _sessionService = sessionService;
            _logger = logger;
        }

        [HttpPost("/connect")]
        public async Task Connect()
        {
            await _sessionService.SendConnection("Rafael");
        }
    }
}