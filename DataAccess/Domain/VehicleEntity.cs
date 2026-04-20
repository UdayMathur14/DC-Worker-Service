using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    public class VehicleEntity :EntityBase
    {
        [Column("LOCATION_ID")]
        public decimal LocationId { get; set; }

        [Column("VEHICLE_NUMBER")]
        public string? VehicleNumber { get; set; }

        [Column("VEHICLE_SIZE_ID")]
        public decimal VehicleSizeId { get; set; }

        [Column("TRANSPORTER_ID")]
        public decimal TransporterId { get; set; }

        [Column("VEHICLE_CONDITION")]
        public string? VehicleCondition { get; set; }

        [Column("REMARKS")]
        public string? Remarks { get; set; }

        [Column("INACTIVE_DATE")]
        public DateTime? InactiveDate { get; set; }
        public TransporterEntity TransporterEntity { get; set; }
        public ICollection<DispatchNoteEntity> Vehicles { get; set; }

    }
}
