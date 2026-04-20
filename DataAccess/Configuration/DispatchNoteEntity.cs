using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration
{
    public class DispatchNoteHederConfiguration : IEntityTypeConfiguration<DispatchNoteEntity>
    {
        public void Configure(EntityTypeBuilder<DispatchNoteEntity> builder)
        {
            

        }
    }
}
