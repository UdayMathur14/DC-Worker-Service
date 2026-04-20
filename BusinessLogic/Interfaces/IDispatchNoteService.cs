using BusinessLogic.Models;

namespace BusinessLogic.Interfaces
{
    public interface IDispatchNoteService
    {
        Task<DispatchNoteProcessingResult> ProcessPendingDispatchNotesAsync(CancellationToken cancellationToken);
    }
}
