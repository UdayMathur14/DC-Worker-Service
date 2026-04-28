using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("COMMON_INBOUND_TB")]
    public class CommonInboundEntity
    {
        [Column("INTERFACE_ID")]
        public decimal InterfaceId { get; set; }

        [Column("TXN_TYPE_CODE")]
        public string? TxnTypeCode { get; set; }

        [Column("DOMAIN")]
        public string? Domain { get; set; }

        [Column("DOCUMENT_NO")]
        public string? DocumentNo { get; set; }

        [Column("DOCUMENT_CREATION_DATE")]
        public DateTime DocumentCreationDate { get; set; }

        [Column("DOCUMENT_TYPE")]
        public string? DocumentType { get; set; }

        [Column("INVOICE_AMOUNT")]
        public decimal InvoiceAmount { get; set; }

        [Column("CGST_UT_RATE")]
        public decimal? CgstUtRate { get; set; }

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
        public decimal? InvoiceTotAmountWithtax { get; set; }

        [Column("FROM_PLANT_CODE")]
        public string? FromPlantCode { get; set; }

        [Column("FROM_STORAGE_LOCATION")]
        public string? FromStorageLocation { get; set; }

        [Column("FROM_CUSTOMER_CODE")]
        public string? FromCustomerCode { get; set; }

        [Column("TO_PLANT_CODE")]
        public string? ToPlantCode { get; set; }

        [Column("TO_STORAGE_LOCATION")]
        public string? ToStorageLocation { get; set; }

        [Column("TO_VENDOR_CODE")]
        public string? ToVendorCode { get; set; }

        [Column("TO_CUSTOMER_CODE")]
        public string? ToCustomerCode { get; set; }

        [Column("TRANSPORTER_CODE")]
        public string? TransporterCode { get; set; }

        [Column("TRANSPORTATION_MODE")]
        public string? TransportationMode { get; set; }

        [Column("VEHICLE_NO")]
        public string? VehicleNo { get; set; }

        [Column("VEHICLE_SIZE")]
        public string? VehicleSize { get; set; }

        [Column("FRLR_NO")]
        public string? FrlrNo { get; set; }

        [Column("FRLR_DATE")]
        public DateTime? FrlrDate { get; set; }

        [Column("TRAVELLING_DISTANCE")]
        public decimal? TravellingDistance { get; set; }
    }
}
