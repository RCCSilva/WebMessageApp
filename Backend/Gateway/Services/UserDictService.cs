using System.Collections.Generic;

namespace Gateway.Services
{
    public class UserDictService
    {
        private readonly IDictionary<string, string> _dictionary;
        
        public UserDictService()
        {
            _dictionary = new Dictionary<string, string>();
        }

        public void AddUser(string username, string connectionId)
        {
            _dictionary[username] = connectionId;
        }

        public string GetConnectionId(string username)
        {
            return _dictionary[username];
        }
    }
}