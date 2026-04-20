using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("DC_USER_SUPPORT_MST_TB")]
    public class UserEntity
    {
        [Column("ID")]
        public decimal Id { get; set; }

        [Key]
        [Column("USER_ID")]
        public string UserId { get; set; }

        [Column("EMPLOYEE_CODE")]
        public string EmpCode { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("EMAIL_ID")]
        public string EmailId { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }

        [Column("CREATED_BY")]
        public string? CreatedBy { get; set; }

        [Column("CREATION_DATE")]
        public DateTime CreationDate { get; set; }

        [Column("LAST_UPDATED_BY")]
        public string? LastUpdatedBy { get; set; }

        [Column("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate { get; set; }

        public ICollection<DispatchNotePartItemsEntity>? CreatedByDispatchNotePartItemsEntity { get; set; }
        public ICollection<DispatchNotePartItemsEntity>? ModifiedByDispatchNotePartItemsEntity { get; set; }
     

    }
}
