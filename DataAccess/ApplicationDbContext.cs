using DataAccess.Configuration;
using DataAccess.Domain;
using DataAccess.Domain.Masters.LookUp;
using DataAccess.Domain.Masters.LookUpType;
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
        }



        #region Transactions
        public virtual DbSet<DispatchNoteEntity> DispatchNoteEntity { get; set; }
        public virtual DbSet<DispatchNotePartItemsEntity> DispatchNotePartItemsEntity { get; set; }
        public virtual DbSet<VehicleEntity> VehicleEntities { get; set; }
        public virtual DbSet<VendorEntity> VendorEntities { get; set; }
        public virtual DbSet<TransporterEntity> TransporterEntities { get; set; }
        public virtual DbSet<PartEntity> PartEntities { get; set; }
        public virtual DbSet<CommonInboundEntity> CommonInboundEntities { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LookUpEntity>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

                entity.HasOne(b => b.LookUpType)
                .WithMany(a => a.LookUp)
                .HasForeignKey(b => b.TypeId);

            });


            modelBuilder.Entity<LookUpTypeEntity>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DispatchNoteEntity>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

                entity.HasOne(b => b.Locations)
                .WithMany(a => a.DispatchNoteLocationEntity)
                .HasForeignKey(b => b.LocationId);

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

                entity.Navigation(p => p.PartEntity)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Navigation(p => p.DispatchNoteEntity)
               .UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            modelBuilder.Entity<TransporterEntity>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

                entity.HasOne(b => b.Locations)
                .WithMany(a => a.Locations2)
                .HasForeignKey(b => b.LocationId);

            });

            modelBuilder.Entity<VendorEntity>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

                entity.HasOne(b => b.TaxationType)
               .WithMany(a => a.VendorTaxationType)
               .HasForeignKey(b => b.TaxationTypeId);

                entity.HasOne(b => b.TaxCodes)
               .WithMany(a => a.VendorTaxCodes)
               .HasForeignKey(b => b.TaxCodeId);
            });

            modelBuilder.Entity<VehicleEntity>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

                entity.HasOne(b => b.TransporterEntity)
                .WithMany(a => a.VehicleEntities)
                .HasForeignKey(b => b.TransporterId);

                entity.HasOne(b => b.Locations)
               .WithMany(a => a.LocationId2)
               .HasForeignKey(b => b.LocationId);

                entity.HasOne(b => b.VehicleSize)
               .WithMany(a => a.VehicleSizeId2)
               .HasForeignKey(b => b.VehicleSizeId);
            });

            modelBuilder.Entity<CommonInboundEntity>(entity =>
            {
                entity.HasKey(e => new { e.InterfaceId, e.TxnTypeCode });
            });

            base.OnModelCreating(modelBuilder);

        }

    }
}
