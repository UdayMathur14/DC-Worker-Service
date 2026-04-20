using DataAccess.Domain;

namespace DataAccess.Interfaces
{
    public interface IDispatchNoteRepository
    {
        Task<IReadOnlyList<DispatchNoteEntity>> GetPendingDispatchNotesAsync(string pendingStatus, int batchSize, CancellationToken cancellationToken);

        Task UpdateDispatchStatusAsync(decimal dispatchNoteId, string status, string updatedBy, CancellationToken cancellationToken);
    }
}
