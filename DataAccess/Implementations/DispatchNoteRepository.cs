using DataAccess.Domain;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    internal class DispatchNoteRepository(ApplicationDbContext context) : IDispatchNoteRepository
    {
        public async Task<IReadOnlyList<DispatchNoteEntity>> GetPendingDispatchNotesAsync(string pendingStatus, int batchSize, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(pendingStatus))
            {
                throw new ArgumentException("Pending status is required.", nameof(pendingStatus));
            }

            return await context.DispatchNoteEntity
                .Include(note => note.Suppliers)
                .Include(note => note.Vehicles)
                .Include(note => note.Transporter)
                .Include(note => note.DispatchNotePartEntities)
                    .ThenInclude(item => item.PartEntity)
                .Where(note => note.InactiveDate == null && note.Status == pendingStatus)
                .OrderBy(note => note.CreationDate)
                .Take(batchSize)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateDispatchStatusAsync(decimal dispatchNoteId, string status, string updatedBy, CancellationToken cancellationToken)
        {
            var dispatchNote = await context.DispatchNoteEntity
                .FirstOrDefaultAsync(note => note.Id == dispatchNoteId, cancellationToken);

            if (dispatchNote is null)
            {
                return;
            }

            dispatchNote.Status = status;
            dispatchNote.LastUpdatedBy = updatedBy;
            dispatchNote.LastUpdateDate = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
