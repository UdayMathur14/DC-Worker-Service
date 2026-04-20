using DataAccess.Domain.Masters.LookUpType;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain.Masters.LookUp
{
    [Table("MFG_LOOKUP_MST_TB")]
    public class LookUpEntity : EntityBase
    {
        [Column("TYPE_ID")]
        public decimal? TypeId { get; set; }

        [Column("CODE")]
        public string? Code { get; set; }

        [Column("VALUE")]
        public string? Value { get; set; }

        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        [Column("ATTRIBUTE11")]
        public string? Attribute11 { get; set; }

        [Column("ATTRIBUTE12")]
        public string? Attribute12 { get; set; }

        [Column("ATTRIBUTE13")]
        public decimal? Attribute13 { get; set; }

        [Column("ATTRIBUTE14")]
        public decimal? Attribute14 { get; set; }
        public LookUpTypeEntity LookUpType { get; set; }

        public ICollection<VehicleEntity> LocationId2 { get; set; }

        public ICollection<TransporterEntity> Locations2 { get; set; }

        public ICollection<VehicleEntity> VehicleSizeId2 { get; set; }
        public ICollection<DispatchNoteEntity> DispatchNoteLocationEntity { get; set; }
        public ICollection<VendorEntity> VendorTaxationType { get; set; }
        public ICollection<VendorEntity> VendorTaxCodes { get; set; }


    }
}
