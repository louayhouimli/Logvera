using Logvera.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Logvera.API.Infrastructure
{
    public class LogveraDbContext : DbContext
    {
        public LogveraDbContext(DbContextOptions<LogveraDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LogEntry>()
                .HasIndex(l => new { l.ApiId, l.Timestamp });

            builder.Entity<LogEntry>()
                .HasIndex(l => new { l.ApiId, l.StatusCode });

            builder.Entity<Api>()
                .HasIndex(a => a.ApiKey)
                .IsUnique();

            builder.Entity<AlertRule>()
.HasIndex(a => a.ApiId);
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Api> Apis { get; set; } = null!;
        public DbSet<LogEntry> LogEntries { get; set; } = null!;
        public DbSet<Alert> Alerts { get; set; } = null!;
        public DbSet<AlertRule> AlertRules { get; set; } = null!;

    }
}