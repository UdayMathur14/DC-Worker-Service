using DataAccess.Domain;
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
        public string? SourceTypeCode { get; set; } // No Null

        [Column("INTERFACE_ID")]
        public string? InterfaceId { get; set; }

        [Column("ATTRIBUTE2")]
        public string? Attribute2 { get; set; }

        [Column("ATTRIBUTE4")]
        public string? Attribute4 { get; set; }

        [Column("BUKRS")]
        public string? Bukrs { get; set; } // No Null
    
        [Column("WERKS")]
        public string? Werks { get; set; } // No Null

        [Column("CARRIER_CODE")]
        public string? CarrierCode { get; set; }

        [Column("ATTRIBUTE12")]
        public string? Attribute12 { get; set; }

        [Column("ATTRIBUTE19")]
        public string? Attribute19 { get; set; }

        [Column("RECORD_CREATION_DATE")]
        public DateTime? RecordCreationDate { get; set; } // DATE type

    }
}