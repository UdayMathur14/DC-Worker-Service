using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class IntfDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public IntfDbContext(IConfiguration configuration, DbContextOptions<IntfDbContext> options) : base(options)
        {
            Configuration = configuration;
            string connectionString = Environment.GetEnvironmentVariable(ConnectionString.IntfSchema);
        }

        public virtual DbSet<ShpcfmEntity> ShpcfmEntity { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

       

    }
}
