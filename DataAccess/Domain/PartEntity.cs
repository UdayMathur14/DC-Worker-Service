using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("MFG_PART_MST_TB")]
    public class PartEntity : EntityBase
    {
        [Column("PART_NUMBER")]
        public string? PartNumber { get; set; }

        [Column("PART_NAME")]
        public string? PartName { get; set; }

        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        [Column("PART_SIZE")]
        public string? PartSize { get; set; }

        [Column("REMARKS")]
        public string? Remarks { get; set; }

        [Column("PART_PRICE")]
        public decimal? PartPrice { get; set; }

        public ICollection<DispatchNotePartItemsEntity> DispatchNotePartEntity { get; set; }
    }
}
