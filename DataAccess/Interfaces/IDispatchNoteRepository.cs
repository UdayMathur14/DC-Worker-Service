using DataAccess.Domain;

namespace DataAccess.Interfaces
{
    public interface IDispatchNoteRepository
    {
        bool IsGateOutConfigured();

        Task<IReadOnlyList<DispatchNoteEntity>> GetDispatchInboundAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<GateOutInboundEntity>> GetGateOutInboundAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<ShpcfmEntity>> GetShpcfmInboundAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<ExportLspEntity>> GetExportLspInboundAsync(CancellationToken cancellationToken);

        Task InsertCommonInboundAsync(CommonInboundEntity commonInboundEntity, CancellationToken cancellationToken);

        Task MarkDispatchInboundProcessedAsync(decimal dispatchNoteId, CancellationToken cancellationToken);

        Task MarkGateOutInboundProcessedAsync(decimal interfaceId, CancellationToken cancellationToken);
        Task MarkExportLspInboundProcessedAsync(decimal interfaceId, CancellationToken cancellationToken);

        Task MarkShpcfmInboundProcessedAsync(string? interfaceId, CancellationToken cancellationToken);
    }
}
