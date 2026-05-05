using DataAccess.Domain.Masters.LookUp;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("MFG_DISPATCH_NOTE_CREATION_HEADER_TXN_TB")]
    public class DispatchNoteEntity : EntityBase
    {
        [Column("LOCATION_ID")]
        public decimal LocationId { get; set; }

        [Column("DISPATCH_NUMBER")]
        public string? DispatchNumber { get; set; }

        [Column("DISPATCH_DATE")]
        public DateTime DispatchDate { get; set; }

        [Column("SUPPLIER_ID")]
        public decimal SupplierId { get; set; }

        [Column("VEHICLE_ID")]
        public decimal VehicleId { get; set; }

        [Column("FRLR_NUMBER")]
        public string? FrlrNumber { get; set; }
        [Column("TRANSPORTER_ID")]
        public decimal? TransporterId { get; set; }
        [Column("FRLR_DATE")]
        public DateTime? FrlrDate { get; set; }
        [Column("OPEN_FLAG")]
        public string? OpenFlag { get; set; }
        [Column("TRANSPORTER_MODE")]
        public string? TransporterMode { get; set; }

        [Column("ATTRIBUTE4")]
        public string? Attribute4 { get; set; }

        public ICollection<DispatchNotePartItemsEntity> DispatchNotePartEntities { get; set; }
        public LookUpEntity Locations { get; set; }
        public VendorEntity Suppliers { get; set; }
        public VehicleEntity Vehicles { get; set; }
        public TransporterEntity Transporter { get; set; }
    }
}
