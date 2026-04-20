using DataAccess.Configuration;
using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Configuration = configuration;
            string connectionString = Environment.GetEnvironmentVariable(ConnectionString.IlfrmSchema); 
        }



        #region Transactions
        public virtual DbSet<DispatchNoteEntity> DispatchNoteEntity { get; set; }
        public virtual DbSet<DispatchNotePartItemsEntity> DispatchNotePartItemsEntity { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DispatchNoteEntity>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

                entity.HasOne(b => b.Suppliers)
                .WithMany(a => a.Suppliers)
                .HasForeignKey(b => b.SupplierId);

                entity.HasOne(b => b.Vehicles)
               .WithMany(a => a.Vehicles)
               .HasForeignKey(b => b.VehicleId);

                entity.HasOne(b => b.Transporter)
               .WithMany(a => a.TransporterDetail)
               .HasForeignKey(b => b.TransporterId);
            });

            modelBuilder.Entity<DispatchNotePartItemsEntity>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

                entity.HasOne(b => b.DispatchNoteEntity)
                .WithMany(a => a.DispatchNotePartEntities)
                .HasForeignKey(b => b.DispatchNoteid);

                entity.HasOne(b => b.PartEntity)
                .WithMany(a => a.DispatchNotePartEntity)
                .HasForeignKey(b => b.PartId);

                entity.HasOne(b => b.CreatedByDetails)
             .WithMany(a => a.CreatedByDispatchNotePartItemsEntity)
             .HasForeignKey(b => b.CreatedBy);

                entity.HasOne(b => b.LastUpdatedByDetails)
                      .WithMany(a => a.ModifiedByDispatchNotePartItemsEntity)
                      .HasForeignKey(b => b.LastUpdatedBy);

            });

            modelBuilder.Entity<TransporterEntity>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

            });

            base.OnModelCreating(modelBuilder);

        }

    }
}
