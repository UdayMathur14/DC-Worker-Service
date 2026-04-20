using DataAccess.Domain;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataAccess.Implementations
{
    internal class DispatchNoteRepository(
        ApplicationDbContext applicationDbContext,
        IServiceProvider serviceProvider,
        ILogger<DispatchNoteRepository> logger) : IDispatchNoteRepository
    {
        private IntfDbContext? IntfDbContext => serviceProvider.GetService<IntfDbContext>();

        public bool IsShpcfmConfigured()
        {
            return IntfDbContext is not null;
        }

        public async Task<IReadOnlyList<DispatchNoteEntity>> GetPendingDispatchInboundAsync(string pendingStatus, int batchSize, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pendingStatus))
                {
                    throw new ArgumentException("Pending status is required.", nameof(pendingStatus));
                }

                return await applicationDbContext.DispatchNoteEntity
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
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. pendingStatus={PendingStatus}, batchSize={BatchSize}",
                    nameof(GetPendingDispatchInboundAsync),
                    pendingStatus,
                    batchSize);
                throw;
            }
        }

        public async Task<IReadOnlyList<ShpcfmEntity>> GetPendingShpcfmInboundAsync(string pendingStatus, int batchSize, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pendingStatus))
                {
                    throw new ArgumentException("Pending status is required.", nameof(pendingStatus));
                }

                if (IntfDbContext is null)
                {
                    logger.LogWarning(
                        "Repository warning in {MethodName}. INTF DbContext is not configured.",
                        nameof(GetPendingShpcfmInboundAsync));
                    return Array.Empty<ShpcfmEntity>();
                }

                return await IntfDbContext.ShpcfmEntity
                    .Where(note => note.InactiveDate == null && note.Status == pendingStatus)
                    .OrderBy(note => note.CreationDate)
                    .Take(batchSize)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. pendingStatus={PendingStatus}, batchSize={BatchSize}",
                    nameof(GetPendingShpcfmInboundAsync),
                    pendingStatus,
                    batchSize);
                throw;
            }
        }

        public Task<bool> CommonInboundExistsAsync(decimal sourceTransactionId, string txnTypeCode, CancellationToken cancellationToken)
        {
            try
            {
                return applicationDbContext.CommonInboundEntities.AnyAsync(
                    item =>
                        item.FrmSapTxnTransactionId == sourceTransactionId &&
                        item.TxnTypeCode == txnTypeCode &&
                        item.InactiveDate == null,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. sourceTransactionId={SourceTransactionId}, txnTypeCode={TxnTypeCode}",
                    nameof(CommonInboundExistsAsync),
                    sourceTransactionId,
                    txnTypeCode);
                throw;
            }
        }

        public async Task InsertCommonInboundAsync(CommonInboundEntity commonInboundEntity, CancellationToken cancellationToken)
        {
            try
            {
                await applicationDbContext.CommonInboundEntities.AddAsync(commonInboundEntity, cancellationToken);
                await applicationDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. frmSapTxnTransactionId={SourceTransactionId}, txnTypeCode={TxnTypeCode}, documentNumber={DocumentNumber}",
                    nameof(InsertCommonInboundAsync),
                    commonInboundEntity.FrmSapTxnTransactionId,
                    commonInboundEntity.TxnTypeCode,
                    commonInboundEntity.DocumentNumber);
                throw;
            }
        }

        public async Task UpdateDispatchInboundStatusAsync(decimal dispatchNoteId, string status, string updatedBy, CancellationToken cancellationToken)
        {
            try
            {
                var dispatchNote = await applicationDbContext.DispatchNoteEntity
                    .FirstOrDefaultAsync(note => note.Id == dispatchNoteId, cancellationToken);

                if (dispatchNote is null)
                {
                    logger.LogWarning(
                        "Repository warning in {MethodName}. Dispatch note not found for id={DispatchNoteId}",
                        nameof(UpdateDispatchInboundStatusAsync),
                        dispatchNoteId);
                    return;
                }

                dispatchNote.Status = status;
                dispatchNote.LastUpdatedBy = updatedBy;
                dispatchNote.LastUpdateDate = DateTime.UtcNow;

                await applicationDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. dispatchNoteId={DispatchNoteId}, status={Status}, updatedBy={UpdatedBy}",
                    nameof(UpdateDispatchInboundStatusAsync),
                    dispatchNoteId,
                    status,
                    updatedBy);
                throw;
            }
        }

        public async Task UpdateShpcfmInboundStatusAsync(decimal shpcfmId, string status, string updatedBy, CancellationToken cancellationToken)
        {
            try
            {
                if (IntfDbContext is null)
                {
                    logger.LogWarning(
                        "Repository warning in {MethodName}. INTF DbContext is not configured for shpcfmId={ShpcfmId}",
                        nameof(UpdateShpcfmInboundStatusAsync),
                        shpcfmId);
                    return;
                }

                var shpcfm = await IntfDbContext.ShpcfmEntity
                    .FirstOrDefaultAsync(note => note.Id == shpcfmId, cancellationToken);

                if (shpcfm is null)
                {
                    logger.LogWarning(
                        "Repository warning in {MethodName}. SHPCFM record not found for id={ShpcfmId}",
                        nameof(UpdateShpcfmInboundStatusAsync),
                        shpcfmId);
                    return;
                }

                shpcfm.Status = status;
                shpcfm.LastUpdatedBy = updatedBy;
                shpcfm.LastUpdateDate = DateTime.UtcNow;

                await IntfDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. shpcfmId={ShpcfmId}, status={Status}, updatedBy={UpdatedBy}",
                    nameof(UpdateShpcfmInboundStatusAsync),
                    shpcfmId,
                    status,
                    updatedBy);
                throw;
            }
        }
    }
}
