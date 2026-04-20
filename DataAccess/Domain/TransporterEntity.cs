using DataAccess.Domain.Masters.LookUp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("MFG_TRANSPORTER_MST_TB")]
    public class TransporterEntity : EntityBase
    {
        [Column("LOCATION_ID")]
        public decimal LocationId { get; set; }

        [Column("TRANSPORTER_CODE")]
        public string? TransporterCode { get; set; }

        [Column("TRANSPORTER_NAME")]
        public string? TransporterName { get; set; }

        [Column("TRANSPORTER_ADDRESS1")]
        public string? TransporterAddress1 { get; set; }

        [Column("TRANSPORTER_ADDRESS2")]
        public string? TransporterAddress2 { get; set; }

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

        [Column("TRANSPORTER_PAYMENT_TERMS_NAME")]
        public string? TransporterPaymentTermsName { get; set; }

        [Column("TRANSPORTER_PAYMENT_METHOD_CODE")]
        public string? TransporterPaytermMethodCode { get; set; }

        [Column("TRANSPORTER_PAYMENT_GROUP")]
        public string? TransporterPaymentGroup { get; set; }

        [Column("TRANSPORTER_PAYTERM_STATUS")]
        public string? TransporterPaytermStatus { get; set; }

        [Column("TRANSPORTER_PAYTERM_TERMS_DAYS")]
        public decimal TransporterPaytermDays { get; set; }

        [Column("TRANSPORTER_CONTACT_NO")]
        public string? TransporterContactNo { get; set; }

        [Column("TRANSPORTER_MAIL_ID")]
        public string? TransporterMailId { get; set; }

        [Column("OWNER_NAME")]
        public string? OwnerName { get; set; }

        [Column("CONTACT_PERSON")]
        public string? ContactPerson { get; set; }

        [Column("REGD_DETAILS")]
        public string? RegdDetails { get; set; }

        [Column("AUTO_BILTI_REQUIRED_FLAG")]
        public string? AutoBiltiRequiredFlag { get; set; }

        [Column("AUTO_BILTI_STARTING_CHARACTER")]
        public string? AutoBiltiStartingCharacter { get; set; }

        [Column("CONSIGNOR_NAME")]
        public string? ConsignorName { get; set; }

        [Column("CONSIGNOR_CONTACT_INFORMATION")]
        public string? ConsignorContactInformation { get; set; }

        [Column("BILTI_HEADER_COMMENTS")]
        public string? BiltiHeaderComments { get; set; }

        [Column("NOTE")]
        public string? Note { get; set; }

        [Column("FOOTER")]
        public string? Footer { get; set; }

        [Column("CREATION_FLAG")]
        public string? CreationFlag { get; set; }
        public ICollection<VehicleEntity> VehicleEntities { get; set; }
        public LookUpEntity Locations { get; set; }
        public ICollection<DispatchNoteEntity> TransporterDetail { get; set; }
      

    }
}
