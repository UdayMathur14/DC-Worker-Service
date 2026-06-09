using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("INTF_SHPCFM_FOR_IOD_MQ_R_IF")]
    public class ShpcfmEntity 
    {
        [Column("SOURCE_TYPE_CODE")]
        public string SourceTypeCode { get; set; } // No Null

        [Key]
        [Column("INTERFACE_ID")]
        public string? InterfaceId { get; set; }

        [Column("ATTRIBUTE2")]
        public string? Attribute2 { get; set; }

        [Column("ATTRIBUTE4")]
        public string? Attribute4 { get; set; }

        [Column("BUKRS")]
        public string Bukrs { get; set; } // No Null

        [Column("SOURCE_HEAD_NO")]
        public string SourceHeadNo { get; set; } // No Null

        [Column("SOURCE_ITEM_NO")]
        public string SourceItemNo { get; set; } // No Null

        [Column("VBELN_VL")]
        public string VbelnVl { get; set; } // No Null

        [Column("POSNR_VL")]
        public long PosnrVl { get; set; } // No Null

        [Column("SHIPPING_SEQ_NO")]
        public string? ShippingSeqNo { get; set; }

        [Column("ZDLVNO")]
        public string? Zdlvno { get; set; }

        [Column("DELIVERY_NO_GERP")]
        public string? DeliveryNoGerp { get; set; }

        [Column("WERKS")]
        public string Werks { get; set; } // No Null

        [Column("MATNR")]
        public string Matnr { get; set; } // No Null

        [Column("TRANSFER_ITEM_CODE")]
        public string? TransferItemCode { get; set; }

        [Column("DELIVERY_QTY")]
        public decimal? DeliveryQty { get; set; }

        [Column("ZVOLUM_N")]
        public decimal? ZvolumN { get; set; }

        [Column("AUART_TXT")]
        public string? AuartTxt { get; set; }

        [Column("ORDER_DATE")]
        public string? OrderDate { get; set; }

        [Column("ORDER_TIME")]
        public string? OrderTime { get; set; }

        [Column("TRANSACTION_DATE")]
        public string? TransactionDate { get; set; }

        [Column("TRANSACTION_TIME")]
        public string? TransactionTime { get; set; }

        [Column("APPOINTMENT_FROM_DATE")]
        public string? AppointmentFromDate { get; set; }

        [Column("APPOINTMENT_FROM_TIME")]
        public string? AppointmentFromTime { get; set; }

        [Column("BILL_TO_NAME")]
        public string? BillToName { get; set; }

        [Column("KUNRE")]
        public string? Kunre { get; set; }

        [Column("KUNWE")]
        public string? Kunwe { get; set; }

        [Column("SHIP_TO_SHORT_NAME")]
        public string? ShipToShortName { get; set; }

        [Column("SHIP_TO_ADDRESS_LINE1_INFO")]
        public string? ShipToAddressLine1Info { get; set; }

        [Column("SHIP_TO_ADDRESS_LINE2_INFO")]
        public string? ShipToAddressLine2Info { get; set; }

        [Column("SHIP_TO_ADDRESS_LINE3_INFO")]
        public string? ShipToAddressLine3Info { get; set; }

        [Column("SHIP_TO_ADDRESS_LINE4_INFO")]
        public string? ShipToAddressLine4Info { get; set; }

        [Column("SHIP_TO_CITY_NAME")]
        public string? ShipToCityName { get; set; }

        [Column("SHIP_TO_POSTAL_CODE")]
        public string? ShipToPostalCode { get; set; }

        [Column("SHIP_TO_PHONE_NO")]
        public string? ShipToPhoneNo { get; set; }

        [Column("SHIP_TO_PHONE2_NO")]
        public string? ShipToPhone2No { get; set; }

        [Column("SHIP_TO_COUNTRY_CODE")]
        public string? ShipToCountryCode { get; set; }

        [Column("BUKRS_TXT")]
        public string BukrsTxt { get; set; } // No Null

        [Column("CANCEL_FLAG")]
        public string? CancelFlag { get; set; }

        [Column("AR_FLAG")]
        public string? ArFlag { get; set; }

        [Column("SOURCE_SYSTEM_NAME")]
        public string? SourceSystemName { get; set; }

        [Column("CARRIER_CODE")]
        public string? CarrierCode { get; set; }

        [Column("ATTRIBUTE12")]
        public string? Attribute12 { get; set; }

        [Column("ATTRIBUTE19")]
        public string? Attribute19 { get; set; }

        [Column("TRANSFER_DATE")]
        public string? TransferDate { get; set; }

        [Column("TRANSFER_TIME")]
        public string? TransferTime { get; set; }

        [Column("ZHQAU")]
        public string? Zhqau { get; set; }

        [Column("PRODH")]
        public string? Prodh { get; set; }

        [Column("ACCEPTANCE_CODE")]
        public string? AcceptanceCode { get; set; }

        [Column("SIMPLE_METHOD")]
        public string? SimpleMethod { get; set; }

        [Column("ACTUAL_ARRIVAL_DATE")]
        public string? ActualArrivalDate { get; set; }

        [Column("VTWEG")]
        public string? Vtweg { get; set; }

        [Column("VTWEG_TXT")]
        public string? VtwegTxt { get; set; }

        [Column("VSART")]
        public string? Vsart { get; set; }

        [Column("KWMENG")]
        public decimal? Kwmeng { get; set; }

        [Column("LFIMG")]
        public decimal? Lfimg { get; set; }

        [Column("BOOKED_DATE")]
        public string? BookedDate { get; set; }

        [Column("SALESREP_NAME")]
        public string? SalesrepName { get; set; }

        [Column("LGORT")]
        public string? Lgort { get; set; }

        [Column("ACTUAL_ARRIVAL_TIME")]
        public string? ActualArrivalTime { get; set; }

        [Column("SDABW")]
        public string? Sdabw { get; set; }

        [Column("BOOKED_TIME")]
        public string? BookedTime { get; set; }

        [Column("RECORD_CREATION_DATE")]
        public DateTime? RecordCreationDate { get; set; } // DATE type

        [Column("SHIP_TO_ADDRESS_LINE5_INFO")]
        public string? ShipToAddressLine5Info { get; set; }

        [Column("SHIP_TO_DISTRICT")]
        public string? ShipToDistrict { get; set; }
    }
}
