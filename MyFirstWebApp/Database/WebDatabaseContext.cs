using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Database.Entities;

namespace MyFirstWebApp.Database
{
    public class WebDatabaseContext : DbContext
    {
        private IConfiguration _configuration;

        public WebDatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public DbSet<Processor> Processors { get; set; }
        public DbSet<ApiProcessor> ApiProcessors { get; set; }
        public DbSet<ApiSeller> apiSellers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration is not null)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("WebConnection"));
            }
        }       
    }
}
