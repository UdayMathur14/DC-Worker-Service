using DataAccess.Domain;

namespace DataAccess.Interfaces
{
    public interface IDispatchNoteRepository
    {
        bool IsShpcfmConfigured();

        Task<IReadOnlyList<DispatchNoteEntity>> GetPendingDispatchInboundAsync(string pendingStatus, int batchSize, CancellationToken cancellationToken);

        Task<IReadOnlyList<ShpcfmEntity>> GetPendingShpcfmInboundAsync(string pendingStatus, int batchSize, CancellationToken cancellationToken);

        Task<bool> CommonInboundExistsAsync(decimal sourceTransactionId, string txnTypeCode, CancellationToken cancellationToken);

        Task InsertCommonInboundAsync(CommonInboundEntity commonInboundEntity, CancellationToken cancellationToken);

        Task UpdateDispatchInboundStatusAsync(decimal dispatchNoteId, string status, string updatedBy, CancellationToken cancellationToken);

        Task UpdateShpcfmInboundStatusAsync(decimal shpcfmId, string status, string updatedBy, CancellationToken cancellationToken);
    }
}
