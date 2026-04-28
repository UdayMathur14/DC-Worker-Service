using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Domain;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class DispatchNoteService(
        IDispatchNoteRepository dispatchNoteRepository,
        ILogger<DispatchNoteService> logger) : IDispatchNoteService
    {
        private const string DispatchTxnTypeCode = "DISPATCH_NOTE";
        private const string GateOutTxnTypeCode = "GATE_OUT";
        private const string DispatchDomainFallback = "DISPATCH";
        private const string DispatchDocumentType = "DISPATCH_NOTE";

        public async Task<DispatchNoteProcessingResult> ProcessPendingDispatchNotesAsync(CancellationToken cancellationToken)
        {
            var dispatchResult = await ProcessDispatchInboundAsync(cancellationToken);
            var gateOutResult = dispatchNoteRepository.IsGateOutConfigured()
                ? await ProcessGateOutInboundAsync(cancellationToken)
                : LogAndReturnSkippedGateOutResult();

            return new DispatchNoteProcessingResult
            {
                TotalFetched = dispatchResult.TotalFetched + gateOutResult.TotalFetched,
                ProcessedCount = dispatchResult.ProcessedCount + gateOutResult.ProcessedCount,
                FailedCount = dispatchResult.FailedCount + gateOutResult.FailedCount
            };
        }

        private DispatchNoteProcessingResult LogAndReturnSkippedGateOutResult()
        {
            logger.LogWarning("Gate-out inbound processing skipped because Oracle_INTF connection is not configured.");
            return new DispatchNoteProcessingResult();
        }

        private async Task<DispatchNoteProcessingResult> ProcessDispatchInboundAsync(CancellationToken cancellationToken)
        {
            var dispatchNotes = await dispatchNoteRepository.GetDispatchInboundAsync(cancellationToken);
            var insertedCount = 0;
            var failedCount = 0;

            foreach (var dispatchNote in dispatchNotes)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var commonInboundEntity = MapDispatchInboundToCommonInbound(dispatchNote);
                    await dispatchNoteRepository.InsertCommonInboundAsync(commonInboundEntity, cancellationToken);
                    insertedCount++;
                }
                catch (Exception ex)
                {
                    failedCount++;
                    logger.LogError(ex, "Dispatch inbound insert failed for DispatchNoteId={DispatchNoteId}", dispatchNote.Id);
                }
            }

            return new DispatchNoteProcessingResult
            {
                TotalFetched = dispatchNotes.Count,
                ProcessedCount = insertedCount,
                FailedCount = failedCount
            };
        }

        private async Task<DispatchNoteProcessingResult> ProcessGateOutInboundAsync(CancellationToken cancellationToken)
        {
            var gateOutRecords = await dispatchNoteRepository.GetGateOutInboundAsync(cancellationToken);
            var insertedCount = 0;
            var failedCount = 0;

            foreach (var gateOutRecord in gateOutRecords)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var commonInboundEntity = MapGateOutInboundToCommonInbound(gateOutRecord);
                    await dispatchNoteRepository.InsertCommonInboundAsync(commonInboundEntity, cancellationToken);
                    insertedCount++;
                }
                catch (Exception ex)
                {
                    failedCount++;
                    logger.LogError(ex, "Gate-out inbound insert failed for InterfaceId={InterfaceId}", gateOutRecord.InterfaceId);
                }
            }

            return new DispatchNoteProcessingResult
            {
                TotalFetched = gateOutRecords.Count,
                ProcessedCount = insertedCount,
                FailedCount = failedCount
            };
        }

        private static CommonInboundEntity MapDispatchInboundToCommonInbound(DispatchNoteEntity dispatchNote)
        {
            var invoiceAmount = dispatchNote.DispatchNotePartEntities?
                .Sum(item => item.PartQty * (item.PartEntity?.PartPrice ?? 0m)) ?? 0m;

            return new CommonInboundEntity
            {
                InterfaceId = dispatchNote.Id,
                TxnTypeCode = DispatchTxnTypeCode,
                Domain = dispatchNote.Locations?.Code ?? DispatchDomainFallback,
                DocumentNo = dispatchNote.DispatchNumber,
                DocumentCreationDate = dispatchNote.DispatchDate,
                DocumentType = DispatchDocumentType,
                InvoiceAmount = invoiceAmount,
                CgstUtRate = 0,
                SgstUtRate = 0,
                IgstRate = 0,
                TaxAmountCgst = 0,
                TaxAmountSgstUtgst = 0,
                TaxAmountIgst = 0,
                InvoiceTotAmountWithtax = invoiceAmount,
                FromPlantCode = dispatchNote.Locations?.Code,
                FromStorageLocation = dispatchNote.Locations?.Value,
                FromCustomerCode = null,
                ToPlantCode = null,
                ToStorageLocation = null,
                ToVendorCode = dispatchNote.Suppliers?.VendorCode,
                ToCustomerCode = null,
                TransporterCode = dispatchNote.Transporter?.TransporterCode,
                TransportationMode = dispatchNote.TransporterMode,
                VehicleNo = dispatchNote.Vehicles?.VehicleNumber,
                VehicleSize = dispatchNote.Vehicles?.VehicleSize?.Code ?? dispatchNote.Vehicles?.VehicleSize?.Value,
                FrlrNo = dispatchNote.FrlrNumber,
                FrlrDate = dispatchNote.FrlrDate,
                TravellingDistance = 0
            };
        }

        private static CommonInboundEntity MapGateOutInboundToCommonInbound(GateOutInboundEntity gateOutRecord)
        {
            return new CommonInboundEntity
            {
                InterfaceId = gateOutRecord.InterfaceId,
                TxnTypeCode = string.IsNullOrWhiteSpace(gateOutRecord.ZtxnType) ? GateOutTxnTypeCode : gateOutRecord.ZtxnType.Trim().ToUpperInvariant(),
                Domain = gateOutRecord.Zdomain,
                DocumentNo = gateOutRecord.ZdocumentNo,
                DocumentCreationDate = gateOutRecord.ZdocumentDate ?? DateTime.UtcNow.Date,
                DocumentType = string.IsNullOrWhiteSpace(gateOutRecord.ZtxnType) ? GateOutTxnTypeCode : gateOutRecord.ZtxnType.Trim().ToUpperInvariant(),
                InvoiceAmount = 0,
                CgstUtRate = 0,
                SgstUtRate = 0,
                IgstRate = 0,
                TaxAmountCgst = 0,
                TaxAmountSgstUtgst = 0,
                TaxAmountIgst = 0,
                InvoiceTotAmountWithtax = 0,
                FromPlantCode = gateOutRecord.Werks,
                FromStorageLocation = null,
                FromCustomerCode = null,
                ToPlantCode = null,
                ToStorageLocation = null,
                ToVendorCode = null,
                ToCustomerCode = null,
                TransporterCode = gateOutRecord.TransId,
                TransportationMode = gateOutRecord.ZtransMode,
                VehicleNo = gateOutRecord.Vehicle,
                VehicleSize = gateOutRecord.VehSize,
                FrlrNo = null,
                FrlrDate = null,
                TravellingDistance = 0
            };
        }
    }
}
