using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain.OutboundTransaction.INTF
{
    [Table("LGEIL_NERP_FRM_TXN_R_IF")]

    public class NerpFrmTxnEntity
    {
        [Key]
        [Column("INTERFACE_ID")]
        public decimal InterfaceId { get; set; }

        [Required]
        [Column("TXN_TYPE_CODE")]
        public string TxnTypeCode { get; set; }

        [Required]
        [Column("DOMAIN")]
        public string Domain { get; set; }

        [Required]
        [Column("DOCUMENT_NO")]
        public string DocumentNo { get; set; }

        [Required]
        [Column("DOCUMENT_CREATION_DATE")]
        public DateTime DocumentCreationDate { get; set; }

        [Required]
        [Column("DOCUMENT_TYPE")]
        public string DocumentType { get; set; }

        [Required]
        [Column("INVOICE_AMOUNT")]
        public decimal InvoiceAmount { get; set; }

        [Column("CGST_RATE")]
        public decimal? CgstRate { get; set; }

        [Column("SGST_UT_RATE")]
        public decimal? SgstUtRate { get; set; }

        [Column("IGST_RATE")]
        public decimal? IgstRate { get; set; }

        [Column("TAX_AMOUNT_CGST")]
        public decimal? TaxAmountCgst { get; set; }

        [Column("TAX_AMOUNT_SGST_UTGST")]
        public decimal? TaxAmountSgstUtgst { get; set; }

        [Column("TAX_AMOUNT_IGST")]
        public decimal? TaxAmountIgst { get; set; }

        [Column("INVOICE_TOT_AMOUNT_WITHTAX")]
        public decimal? InvoiceTotAmountWithTax { get; set; }

        [Column("FROM_PLANT_CODE")]
        public string FromPlantCode { get; set; }

        [Column("FROM_STORAGE_LOCATION")]
        public string FromStorageLocation { get; set; }

        [Column("FROM_CUSTOMER_CODE")]
        public string FromCustomerCode { get; set; }

        [Column("TO_PLANT_CODE")]
        public string ToPlantCode { get; set; }

        [Column("TO_STORAGE_LOCATION")]
        public string ToStorageLocation { get; set; }

        [Column("TO_VENDOR_CODE")]
        public string ToVendorCode { get; set; }

        [Column("TO_CUSTOMER_CODE")]
        public string ToCustomerCode { get; set; }

        [Column("TRANSPORTER_CODE")]
        public string TransporterCode { get; set; }

        [Column("TRANSPORTATION_MODE")]
        public string TransportationMode { get; set; }

        [Column("VEHICLE_NO")]
        public string VehicleNo { get; set; }

        [Column("VEHICLE_SIZE")]
        public string VehicleSize { get; set; }

        [Column("FRLR_NO")]
        public string FrlrNo { get; set; }

        [Column("FRLR_DATE")]
        public DateTime? FrlrDate { get; set; }

        [Column("TRAVELLING_DISTANCE")]
        public decimal? TravellingDistance { get; set; }

        [Column("TRANSFER_FLAG")]
        public string TransferFlag { get; set; }

        [Column("TRANSFER_DATE")]
        public DateTime? TransferDate { get; set; }

        [Column("GLOBAL_UNIQUE_ID")]
        public string GlobalUniqueId { get; set; }

        [Column("BAM_SEQUENCE_ID")]
        public string BamSequenceId { get; set; }

        [Column("OLD_GLOBAL_UNIQUE_ID")]
        public string OldGlobalUniqueId { get; set; }
    }
}
