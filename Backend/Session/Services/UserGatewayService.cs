using System.Collections.Generic;

namespace Session.Services
{
    public class UserGatewayService
    {
        // TODO Move this to a database
        private readonly IDictionary<string, string> _dictionary;
        
        public UserGatewayService()
        {
            _dictionary = new Dictionary<string, string>();
        }

        public void AddUser(string username, string gatewayId)
        {
            _dictionary[username] = gatewayId;
        }

        public string GetGateway(string username)
        {
            return _dictionary[username];
        }
    }
}