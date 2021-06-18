using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Session.Models;

namespace Session
{
    public class SessionDbContext : DbContext
    {
        public SessionDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StoredMessage> StoredMessages { get; set; }
    }
}