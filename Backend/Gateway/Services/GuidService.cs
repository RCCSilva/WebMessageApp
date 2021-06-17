using System;

namespace Gateway.Services
{
    public class GuidService
    {
        public string GatewayId { get; }

        public GuidService()
        {
            GatewayId = Guid.NewGuid().ToString();
        }
    }
}