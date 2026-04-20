using DataAccess.Domain.Masters.LookUp;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain.Masters.LookUpType
{
    [Table("MFG_LOOKUP_TYPE_MST_TB")]
    public class LookUpTypeEntity : EntityBase
    {
        [Column("TYPE")]
        public string? Type { get; set; }

        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        public ICollection<LookUpEntity> LookUp { get; set; }
    }
}
