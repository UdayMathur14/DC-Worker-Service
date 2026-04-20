using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("MFG_VENDOR_MST_TB")]
    public class VendorEntity : EntityBase
    {
        [Column("VENDOR_CODE")]
        public string? VendorCode { get; set; }

        [Column("VENDOR_NAME")]
        public string? VendorName { get; set; }

        [Column("VENDOR_ADDRESS1")]
        public string? VendorAddress1 { get; set; }

        [Column("VENDOR_ADDRESS2")]
        public string? VendorAddress2 { get; set; }

        [Column("CITY")]
        public string? City { get; set; }

        [Column("STATE")]
        public string? State { get; set; }

        [Column("COUNTRY")]
        public string? Country { get; set; }

        [Column("POSTAL_CODE")]
        public string? PostalCode { get; set; }

        [Column("PAN_NO")]
        public string? PanNo { get; set; }

        [Column("GSTIN_NO")]
        public string? GstInNo { get; set; }

        [Column("CONTACT_NUMBER")]
        public string? ContactNumber { get; set; }

        [Column("EMAIL")]
        public string? Email { get; set; }

        [Column("VENDOR_PAYMENT_TERMS_NAME")]
        public string? VendorPaymentTermsName { get; set; }

        [Column("VENDOR_PAYMENT_METHOD_CODE")]
        public string? VendorPaytermMethodCode { get; set; }

        [Column("VENDOR_PAYMENT_GROUP")]
        public string? VendorPaymentGroup { get; set; }

        [Column("VENDOR_PAYTERM_STATUS")]
        public string? VendorPaytermStatus { get; set; }

        [Column("VENDOR_PAYTERM_TERMS_DAYS")]
        public decimal? VendorPaytermDays { get; set; }

        [Column("TAXATION_TYPE_ID")]
        public decimal? TaxationTypeId { get; set; }

        [Column("TAX_CODES_ID")]
        public decimal? TaxCodeId { get; set; }
        public ICollection<DispatchNoteEntity> Suppliers { get; set; }
    }
}
