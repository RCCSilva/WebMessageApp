using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GatewayManager
{
    namespace EFGetStarted
    {
        public class GatewayManagerContext : DbContext
        {
            public GatewayManagerContext(DbContextOptions options): base(options)
            {
            }

            public DbSet<Connections> Connections { get; set; }
        }

        public class Connections
        {
            public int Id { get; set; }
            public string Url { get; set; }
        }
    }
}