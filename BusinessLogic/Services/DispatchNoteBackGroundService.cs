using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Options;
using DataAccess.Domain;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BusinessLogic.Services
{
    public class DispatchNoteService(
        IDispatchNoteRepository dispatchNoteRepository,
        IOptions<DispatchNoteWorkerOptions> options,
        ILogger<DispatchNoteService> logger) : IDispatchNoteService
    {
        private const string DispatchTxnTypeCode = "DISPATCH_NOTE_INBOUND";
        private const string ShpcfmTxnTypeCode = "SHPCFM_INBOUND";

        private readonly DispatchNoteWorkerOptions _options = options.Value;

        public async Task<DispatchNoteProcessingResult> ProcessPendingDispatchNotesAsync(CancellationToken cancellationToken)
        {
            var dispatchResult = await ProcessDispatchInboundAsync(cancellationToken);
            var shpcfmResult = dispatchNoteRepository.IsShpcfmConfigured()
                ? await ProcessShpcfmInboundAsync(cancellationToken)
                : LogAndReturnSkippedShpcfmResult();

            return new DispatchNoteProcessingResult
            {
                TotalFetched = dispatchResult.TotalFetched + shpcfmResult.TotalFetched,
                ProcessedCount = dispatchResult.ProcessedCount + shpcfmResult.ProcessedCount,
                FailedCount = dispatchResult.FailedCount + shpcfmResult.FailedCount
            };
        }

        private DispatchNoteProcessingResult LogAndReturnSkippedShpcfmResult()
        {
            logger.LogWarning("SHPCFM inbound processing skipped because INTF database connection is not configured.");
            return new DispatchNoteProcessingResult();
        }

        private async Task<DispatchNoteProcessingResult> ProcessDispatchInboundAsync(CancellationToken cancellationToken)
        {
            
            var pendingDispatchNotes = await dispatchNoteRepository.GetPendingDispatchInboundAsync(
                _options.PendingStatus,
                _options.BatchSize,
                cancellationToken);

            var processedCount = 0;
            var failedCount = 0;

            foreach (var dispatchNote in pendingDispatchNotes)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    if (!IsDispatchInboundValid(dispatchNote, out var validationMessage))
                    {
                        failedCount++;
                        logger.LogWarning(
                            "Dispatch note inbound {DispatchNoteId} skipped because {Reason}.",
                            dispatchNote.Id,
                            validationMessage);

                        await dispatchNoteRepository.UpdateDispatchInboundStatusAsync(
                            dispatchNote.Id,
                            _options.FailedStatus,
                            _options.UpdatedBy,
                            cancellationToken);

                        continue;
                    }

                    var alreadyExists = await dispatchNoteRepository.CommonInboundExistsAsync(
                        dispatchNote.Id,
                        DispatchTxnTypeCode,
                        cancellationToken);

                    if (!alreadyExists)
                    {
                        var commonInboundEntity = MapDispatchInboundToCommonInbound(dispatchNote);
                        await dispatchNoteRepository.InsertCommonInboundAsync(commonInboundEntity, cancellationToken);
                    }

                    await dispatchNoteRepository.UpdateDispatchInboundStatusAsync(
                        dispatchNote.Id,
                        _options.ProcessedStatus,
                        _options.UpdatedBy,
                        cancellationToken);

                    processedCount++;
                }
                catch (Exception ex)
                {
                    failedCount++;
                    logger.LogError(ex, "Dispatch note inbound {DispatchNoteId} processing failed.", dispatchNote.Id);

                    await dispatchNoteRepository.UpdateDispatchInboundStatusAsync(
                        dispatchNote.Id,
                        _options.FailedStatus,
                        _options.UpdatedBy,
                        cancellationToken);
                }
            }

            return new DispatchNoteProcessingResult
            {
                TotalFetched = pendingDispatchNotes.Count,
                ProcessedCount = processedCount,
                FailedCount = failedCount
            };
        }

        private async Task<DispatchNoteProcessingResult> ProcessShpcfmInboundAsync(CancellationToken cancellationToken)
        {
            var pendingShpcfmRecords = await dispatchNoteRepository.GetPendingShpcfmInboundAsync(
                _options.PendingStatus,
                _options.BatchSize,
                cancellationToken);

            var processedCount = 0;
            var failedCount = 0;

            foreach (var shpcfm in pendingShpcfmRecords)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    if (!IsShpcfmInboundValid(shpcfm, out var validationMessage))
                    {
                        failedCount++;
                        logger.LogWarning(
                            "SHPCFM inbound {ShpcfmId} skipped because {Reason}.",
                            shpcfm.Id,
                            validationMessage);

                        await dispatchNoteRepository.UpdateShpcfmInboundStatusAsync(
                            shpcfm.Id,
                            _options.FailedStatus,
                            _options.UpdatedBy,
                            cancellationToken);

                        continue;
                    }

                    var alreadyExists = await dispatchNoteRepository.CommonInboundExistsAsync(
                        shpcfm.Id,
                        ShpcfmTxnTypeCode,
                        cancellationToken);

                    if (!alreadyExists)
                    {
                        var commonInboundEntity = MapShpcfmInboundToCommonInbound(shpcfm);
                        await dispatchNoteRepository.InsertCommonInboundAsync(commonInboundEntity, cancellationToken);
                    }

                    await dispatchNoteRepository.UpdateShpcfmInboundStatusAsync(
                        shpcfm.Id,
                        _options.ProcessedStatus,
                        _options.UpdatedBy,
                        cancellationToken);

                    processedCount++;
                }
                catch (Exception ex)
                {
                    failedCount++;
                    logger.LogError(ex, "SHPCFM inbound {ShpcfmId} processing failed.", shpcfm.Id);

                    await dispatchNoteRepository.UpdateShpcfmInboundStatusAsync(
                        shpcfm.Id,
                        _options.FailedStatus,
                        _options.UpdatedBy,
                        cancellationToken);
                }
            }

            return new DispatchNoteProcessingResult
            {
                TotalFetched = pendingShpcfmRecords.Count,
                ProcessedCount = processedCount,
                FailedCount = failedCount
            };
        }

        private static bool IsDispatchInboundValid(DispatchNoteEntity dispatchNote, out string validationMessage)
        {
            if (string.IsNullOrWhiteSpace(dispatchNote.DispatchNumber))
            {
                validationMessage = "dispatch number is missing";
                return false;
            }

            if (dispatchNote.DispatchNotePartEntities is null || dispatchNote.DispatchNotePartEntities.Count == 0)
            {
                validationMessage = "part items are missing";
                return false;
            }

            validationMessage = string.Empty;
            return true;
        }

        private static bool IsShpcfmInboundValid(ShpcfmEntity shpcfm, out string validationMessage)
        {
            if (string.IsNullOrWhiteSpace(shpcfm.SourceHeadNo))
            {
                validationMessage = "source head number is missing";
                return false;
            }

            if (string.IsNullOrWhiteSpace(shpcfm.SourceTypeCode))
            {
                validationMessage = "source type code is missing";
                return false;
            }

            validationMessage = string.Empty;
            return true;
        }

        private CommonInboundEntity MapDispatchInboundToCommonInbound(DispatchNoteEntity dispatchNote)
        {
            var now = DateTime.UtcNow;
            var invoiceAmount = dispatchNote.DispatchNotePartEntities?
                .Sum(item => item.PartQty * (item.PartEntity?.PartPrice ?? 0m));

            return new CommonInboundEntity
            {
                FrmSapTxnTransactionId = dispatchNote.Id,
                TxnTypeCode = DispatchTxnTypeCode,
                DocumentNumber = dispatchNote.DispatchNumber,
                DocumentCreationDate = dispatchNote.DispatchDate,
                DocumentType = "DISPATCH_NOTE",
                InvoiceAmount = invoiceAmount,
                InvTotalAmount = invoiceAmount,
                FromDestination = dispatchNote.Suppliers?.VendorName,
                ToDestination = dispatchNote.Transporter?.TransporterName,
                TransporterId = dispatchNote.TransporterId.HasValue ? Convert.ToInt64(dispatchNote.TransporterId.Value) : null,
                VehicleNumber = dispatchNote.Vehicles?.VehicleNumber,
                VehicleSizeId = Convert.ToInt64(dispatchNote.Vehicles?.VehicleSizeId ?? 0),
                FrlrNumber = dispatchNote.FrlrNumber,
                FrlrDate = dispatchNote.FrlrDate,
                OpenFlag = dispatchNote.OpenFlag,
                Status = "Active",
                CreatedBy = _options.UpdatedBy,
                CreationDate = now,
                LastUpdatedBy = _options.UpdatedBy,
                LastUpdateDate = now
            };
        }

        private CommonInboundEntity MapShpcfmInboundToCommonInbound(ShpcfmEntity shpcfm)
        {
            var now = DateTime.UtcNow;

            return new CommonInboundEntity
            {
                FrmSapTxnTransactionId = shpcfm.Id,
                TxnTypeCode = ShpcfmTxnTypeCode,
                DocumentNumber = shpcfm.SourceHeadNo,
                DocumentCreationDate = shpcfm.RecordCreationDate ?? now,
                DocumentType = shpcfm.AuartTxt,
                InvoiceAmount = shpcfm.Kwmeng,
                InvTotalAmount = shpcfm.Lfimg,
                FromDestination = shpcfm.BukrsTxt ?? shpcfm.Werks,
                ToDestination = shpcfm.ShipToCityName,
                FrlrNumber = shpcfm.Zdlvno ?? shpcfm.DeliveryNoGerp,
                FrlrDate = shpcfm.RecordCreationDate,
                OpenFlag = shpcfm.CancelFlag,
                Status = "Active",
                CreatedBy = _options.UpdatedBy,
                CreationDate = now,
                LastUpdatedBy = _options.UpdatedBy,
                LastUpdateDate = now
            };
        }
    }
}
