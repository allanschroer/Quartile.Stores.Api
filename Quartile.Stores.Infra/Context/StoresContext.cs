using Microsoft.EntityFrameworkCore;

namespace Quartile.Stores.Infra.Configuration
{
    public class StoresContext : DbContext
    {
        public StoresContext(DbContextOptions<StoresContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoresContext).Assembly);
        }
    }
}
