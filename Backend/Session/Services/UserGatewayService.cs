#nullable enable
using System.Collections.Generic;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Session.Services
{
    public class UserGatewayService
    {
        // TODO Move this to a database
        private readonly IDictionary<string, string> _dictionary;
        private readonly ILogger<UserGatewayService> _logger;

        public UserGatewayService(ILogger<UserGatewayService> logger)
        {
            _logger = logger;
            _dictionary = new Dictionary<string, string>();
        }

        public void AddUser(string username, string gatewayId)
        {
            _dictionary[username] = gatewayId;
            _logger.LogDebug($"Saved '{username}' with gatewayId {gatewayId}");
        }

        public string? GetGateway(string username)
        {
            try
            {
                return _dictionary[username];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}