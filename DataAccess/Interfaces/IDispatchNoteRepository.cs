using DataAccess.Domain;

namespace DataAccess.Interfaces
{
    public interface IDispatchNoteRepository
    {
        bool IsGateOutConfigured();

        Task<IReadOnlyList<DispatchNoteEntity>> GetDispatchInboundAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<GateOutInboundEntity>> GetGateOutInboundAsync(CancellationToken cancellationToken);

        Task InsertCommonInboundAsync(CommonInboundEntity commonInboundEntity, CancellationToken cancellationToken);

        Task MarkDispatchInboundProcessedAsync(decimal dispatchNoteId, CancellationToken cancellationToken);

        Task MarkGateOutInboundProcessedAsync(decimal interfaceId, CancellationToken cancellationToken);
    }
}
