using DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    public abstract class EntityBase : IEntity
    {
        [Key]
        [Column("ID")]
        public decimal Id { get; set; }

        [Column("STATUS")]
        public string? Status { get; set; }

        [Column("INACTIVE_DATE")]
        public DateTime? InactiveDate { get; set; }

        [Column("CREATED_BY")]
        public string? CreatedBy { get; set; }

        [Column("CREATION_DATE")]
        public DateTime CreationDate { get; set; }

        [Column("LAST_UPDATED_BY")]
        public string? LastUpdatedBy { get; set; }

        [Column("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate { get; set; }
        //public UserEntity CreatedByDetails { get; set; }
        //public UserEntity LastUpdatedByDetails { get; set; }
    }
}
