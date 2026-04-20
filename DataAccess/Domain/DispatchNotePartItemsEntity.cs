using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("MFG_DISPATCH_NOTE_PART_ITEMS_TXN_TB")]
    public class DispatchNotePartItemsEntity : EntityBase
    {
        [Column("DISPATCH_NOTE_ID")]
        public decimal DispatchNoteid { get; set; }

        [Column("PART_ID")]
        public decimal PartId { get; set; }

        [Column("PART_QTY")]
        public decimal PartQty { get; set; }
        public DispatchNoteEntity DispatchNoteEntity { get; set; }
        public PartEntity PartEntity { get; set; }
    }
}
