using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("COMMON_INBOUND_TB", Schema = "ILFRM")]
    public class CommonInboundEntity
    {
        [Column("INTERFACE_ID")]
        public decimal InterfaceId { get; set; }

        [Column("TXN_TYPE_CODE")]
        public string TxnTypeCode { get; set; } = string.Empty;

        [Column("DOMAIN")]
        public string Domain { get; set; } = string.Empty;

        [Column("DOCUMENT_NO")]
        public string DocumentNo { get; set; } = string.Empty;

        [Column("DOCUMENT_CREATION_DATE")]
        public DateTime DocumentCreationDate { get; set; }

        [Column("DOCUMENT_TYPE")]
        public string DocumentType { get; set; } = string.Empty;

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

        [Column("TRANSPORTER_NAME")]
        public string? TransporterName { get; set; }

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

        [Column("TRANSFER_FLAG")]
        public string? TransferFlag { get; set; }

        [Column("TRANSFER_DATE")]
        public DateTime? TransferDate { get; set; }

        [Column("GLOBAL_UNIQUE_ID")]
        public string? GlobalUniqueId { get; set; }

        [Column("BAM_SEQUENCE_ID")]
        public string? BamSequenceId { get; set; }

        [Column("OLD_GLOBAL_UNIQUE_ID")]
        public string? OldGlobalUniqueId { get; set; }

        [Column("CONTROL_OUTGOING_BY")]
        public string? ControlOutgoingBy { get; set; }

        [Column("CONTROL_OUTGOING_DATE")]
        public DateTime? ControlOutgoingDate { get; set; }

        [Column("CONTROL_OUTGOING_LAST_UPDATED_BY")]
        public string? ControlOutgoingLastUpdatedBy { get; set; }

        [Column("CONTROL_OUTGOING_LAST_UPDATE_DATE")]
        public DateTime? ControlOutgoingLastUpdateDate { get; set; }

        [Column("CONTROL_OUTGOING_REMARKS")]
        public string? ControlOutgoingRemarks { get; set; }

        [Column("GATE_OUT_BY")]
        public string? GateOutBy { get; set; }

        [Column("GATE_OUT_DATE")]
        public DateTime? GateOutDate { get; set; }

        [Column("STATUS")]
        public string? Status { get; set; }
    }
}