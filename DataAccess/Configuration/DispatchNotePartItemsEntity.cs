using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration
{
    public class DispatchNotePartIemsConfiguration : IEntityTypeConfiguration<DispatchNotePartItemsEntity>
    {
        public void Configure(EntityTypeBuilder<DispatchNotePartItemsEntity> builder)
        {
          

        }
    }
}
