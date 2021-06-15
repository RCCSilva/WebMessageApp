using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Gateway.Producer;
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
        private readonly KafkaDependentProducer<Null, string> _kafkaDependentProducer;

        public SessionController(KafkaDependentProducer<Null, string> kafkaDependentProducer,
            ILogger<SessionController> logger)
        {
            _kafkaDependentProducer = kafkaDependentProducer;
            _logger = logger;
        }

        [HttpPost("/connect")]
        public async Task Connect()
        {
            _logger.LogDebug("Received request to connect");
            var message = new Message<Null, string> {Value = "I want to connect!!!"};
            await _kafkaDependentProducer.ProduceAsync("test", message);
            _logger.LogDebug("Message to connect sent");
        }
    }
}