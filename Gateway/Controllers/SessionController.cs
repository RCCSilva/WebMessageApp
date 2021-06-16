using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Gateway.Dtos;
using Gateway.Producer;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
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