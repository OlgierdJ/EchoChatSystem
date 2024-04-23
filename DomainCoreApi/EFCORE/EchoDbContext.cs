using DomainCoreApi.EFCORE.Configurations.AccountCore;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE
{
    public class EchoDbContext : DbContext
    {
        public EchoDbContext(DbContextOptions<EchoDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountConfiguration).Assembly);
        }
    }
}
