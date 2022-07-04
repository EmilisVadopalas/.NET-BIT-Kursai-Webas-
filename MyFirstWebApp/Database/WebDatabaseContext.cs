using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Database.Entities;

namespace MyFirstWebApp.Database
{
    public class WebDatabaseContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<Processor> Processors { get; set; }

        public WebDatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration is not null)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("WebConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Processor>(b => b.HasKey(k => k.Id));
        }
    }
}
