using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("DC_COMMON_INBOUND_TB", Schema = "ILFRM")]
    public class CommonInboundEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public decimal Id { get; set; }

        [Column("FRM_SAP_TXN_TRANSACTION_ID")]
        public decimal? FrmSapTxnTransactionId { get; set; }

        [Column("TXN_TYPE_CODE")]
        [StringLength(255)]
        public string? TxnTypeCode { get; set; }

        [Column("DOCUMENT_NUMBER")]
        [StringLength(255)]
        public string? DocumentNumber { get; set; }

        [Column("DOCUMENT_CREATION_DATE")]
        public DateTime? DocumentCreationDate { get; set; }

        [Column("DOCUMENT_TYPE")]
        [StringLength(100)]
        public string? DocumentType { get; set; }

        [Column("INVOICE_AMOUNT")]
        public decimal? InvoiceAmount { get; set; }

        [Column("CGST_RATE")]
        public decimal? CgstRate { get; set; }

        [Column("SGST_RATE")]
        public decimal? SgstRate { get; set; }

        [Column("IGST_RATE")]
        public decimal? IgstRate { get; set; }

        [Column("TAX_AMOUNT_CGST")]
        public decimal? TaxAmountCgst { get; set; }

        [Column("TAX_AMOUNT_SGST")]
        public decimal? TaxAmountSgst { get; set; }

        [Column("TAX_AMOUNT_IGST")]
        public decimal? TaxAmountIgst { get; set; }

        [Column("INV_TOTAL_AMOUNT")]
        public decimal? InvTotalAmount { get; set; }

        [Column("FROM_DESTINATION")]
        [StringLength(255)]
        public string? FromDestination { get; set; }

        [Column("TO_DESTINATION")]
        [StringLength(255)]
        public string? ToDestination { get; set; }

        [Column("TRANSPORTER_ID")]
        public long? TransporterId { get; set; }

        [Column("TRANSPORTATION_MODE_ID")]
        public long? TransportationModeId { get; set; }

        [Column("VEHICLE_NUMBER")]
        [StringLength(50)]
        public string? VehicleNumber { get; set; }

        [Column("VEHICLE_SIZE_ID")]
        public long? VehicleSizeId { get; set; }

        [Column("FRLR_NUMBER")]
        [StringLength(50)]
        public string? FrlrNumber { get; set; }

        [Column("FRLR_DATE")]
        public DateTime? FrlrDate { get; set; }

        [Column("TRAVELLING_DISTANCE")]
        public decimal? TravellingDistance { get; set; }

        [Column("OPEN_FLAG")]
        [StringLength(20)]
        public string? OpenFlag { get; set; }

        [Column("STATUS")]
        [StringLength(20)]
        public string Status { get; set; } = "Active";

        [Column("INACTIVE_DATE")]
        public DateTime? InactiveDate { get; set; }

        [Column("CREATED_BY")]
        [StringLength(36)]
        public string? CreatedBy { get; set; }

        [Column("CREATION_DATE")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Column("LAST_UPDATED_BY")]
        [StringLength(36)]
        public string? LastUpdatedBy { get; set; }

        [Column("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
    }
}
