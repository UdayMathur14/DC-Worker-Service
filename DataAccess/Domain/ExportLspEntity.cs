using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("LGEIL_NERP_GATEOUT_EXP_TXN_R_IF")]
    public class ExportLspEntity
    {
        [Key]
        [Column("INTERFACE_ID")]
        public decimal InterfaceId { get; set; }

        [Column("ZTXN_TYPE")]
        public string? ZtxnType { get; set; }

        [Column("ZDOMAIN")]
        public string? Zdomain { get; set; }

        [Column("WERKS")]
        public string? Werks { get; set; }

        [Column("ZDOCUMENT_NO")]
        public string? ZdocumentNo { get; set; }

        [Column("ZDOCUMENT_DATE")]
        public DateTime? ZdocumentDate { get; set; }

        [Column("TRANS_ID")]
        public string? TransId { get; set; }

        [Column("TRANS_NAME")]
        public string? TransName { get; set; }

        [Column("ZTRANS_MODE")]
        public string? ZtransMode { get; set; }

        [Column("VEHICLE")]
        public string? Vehicle { get; set; }

        [Column("VEH_SIZE")]
        public string? VehSize { get; set; }

        [Column("ATTRIBUTE1")]
        public string? Attribute1 { get; set; }

        [Column("ATTRIBUTE4")]
        public string? Attribute4 { get; set; }

    }
}
