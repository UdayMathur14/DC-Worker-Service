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
        private readonly DispatchNoteWorkerOptions _options = options.Value;

        public async Task<DispatchNoteProcessingResult> ProcessPendingDispatchNotesAsync(CancellationToken cancellationToken)
        {
            var pendingDispatchNotes = await dispatchNoteRepository.GetPendingDispatchNotesAsync(
                _options.PendingStatus,
                _options.BatchSize,
                cancellationToken);

            if (pendingDispatchNotes.Count == 0)
            {
                return new DispatchNoteProcessingResult();
            }

            var processedCount = 0;
            var failedCount = 0;

            foreach (var dispatchNote in pendingDispatchNotes)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    if (!IsDispatchNoteValid(dispatchNote, out var validationMessage))
                    {
                        failedCount++;
                        logger.LogWarning(
                            "Dispatch note {DispatchNoteId} skipped because {Reason}.",
                            dispatchNote.Id,
                            validationMessage);

                        await dispatchNoteRepository.UpdateDispatchStatusAsync(
                            dispatchNote.Id,
                            _options.FailedStatus,
                            _options.UpdatedBy,
                            cancellationToken);

                        continue;
                    }

                    logger.LogInformation(
                        "Processing dispatch note {DispatchNoteId} ({DispatchNumber}) with {PartCount} part rows.",
                        dispatchNote.Id,
                        dispatchNote.DispatchNumber,
                        dispatchNote.DispatchNotePartEntities?.Count ?? 0);

                    await dispatchNoteRepository.UpdateDispatchStatusAsync(
                        dispatchNote.Id,
                        _options.ProcessedStatus,
                        _options.UpdatedBy,
                        cancellationToken);

                    processedCount++;
                }
                catch (Exception ex)
                {
                    failedCount++;
                    logger.LogError(ex, "Dispatch note {DispatchNoteId} processing failed.", dispatchNote.Id);

                    await dispatchNoteRepository.UpdateDispatchStatusAsync(
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

        private static bool IsDispatchNoteValid(DispatchNoteEntity dispatchNote, out string validationMessage)
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
    }
}
