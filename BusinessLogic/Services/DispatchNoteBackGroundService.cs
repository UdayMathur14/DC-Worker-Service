using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Domain;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace BusinessLogic.Services
{
    public class DispatchNoteService(
        IDispatchNoteRepository dispatchNoteRepository,
        ILogger<DispatchNoteService> logger) : IDispatchNoteService
    {
        private const string DispatchTxnTypeCode = "RB";
        private const string GateOutTxnTypeCode = "GATE_OUT";
        private const string ShpcfmTxnTypeCode = "SAL";
        private const string DispatchDocumentType = "RB";

        public async Task<DispatchNoteProcessingResult> ProcessPendingDispatchNotesAsync(CancellationToken cancellationToken)
        {
            var dispatchResult = await ProcessDispatchInboundAsync(cancellationToken);

            var gateOutResult =  await ProcessGateOutInboundAsync(cancellationToken);

            var shpcfmResult = await ProcessShpcfmInboundAsync(cancellationToken);

            return new DispatchNoteProcessingResult
            {
                TotalFetched = dispatchResult.TotalFetched + gateOutResult.TotalFetched + shpcfmResult.TotalFetched,
                ProcessedCount = dispatchResult.ProcessedCount + gateOutResult.ProcessedCount + shpcfmResult.ProcessedCount,
                FailedCount = dispatchResult.FailedCount + gateOutResult.FailedCount + shpcfmResult.FailedCount
            };
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
                    await dispatchNoteRepository.MarkDispatchInboundProcessedAsync(dispatchNote.Id, cancellationToken);
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

        private async Task<DispatchNoteProcessingResult> ProcessShpcfmInboundAsync(CancellationToken cancellationToken)
        {
            var shpcfmRecords = await dispatchNoteRepository.GetShpcfmInboundAsync(cancellationToken);
            var insertedCount = 0;
            var failedCount = 0;

            foreach (var shpcfmRecord in shpcfmRecords)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var commonInboundEntity = MapShpcfmInboundToCommonInbound(shpcfmRecord);
                    await dispatchNoteRepository.InsertCommonInboundAsync(commonInboundEntity, cancellationToken);
                    await dispatchNoteRepository.MarkShpcfmInboundProcessedAsync(shpcfmRecord.InterfaceId, cancellationToken);
                    insertedCount++;
                }
                catch (Exception ex)
                {
                    failedCount++;
                    logger.LogError(
                        ex,
                        "SHPCFM inbound insert failed for InterfaceId={InterfaceId}",
                        shpcfmRecord.InterfaceId);
                }
            }

            return new DispatchNoteProcessingResult
            {
                TotalFetched = shpcfmRecords.Count,
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
                    await dispatchNoteRepository.MarkGateOutInboundProcessedAsync(gateOutRecord.InterfaceId, cancellationToken);
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
            var invoiceAmount = 0;

            return new CommonInboundEntity
            {
                InterfaceId = dispatchNote.Id,
                TxnTypeCode = DispatchTxnTypeCode,
                Domain = "MFG",
                DocumentNo = dispatchNote.DispatchNumber,
                DocumentCreationDate = dispatchNote.DispatchDate,
                DocumentType = DispatchDocumentType,
                InvoiceAmount = invoiceAmount,

                CgstRate = 0,
                SgstUtRate = 0,
                IgstRate = 0,

                TaxAmountCgst = 0,
                TaxAmountSgstUtgst = 0,
                TaxAmountIgst = 0,
                InvoiceTotAmountWithtax = invoiceAmount,

                FromPlantCode = string.Equals(dispatchNote.Locations?.Code?.Trim(), "HA", StringComparison.OrdinalIgnoreCase)? "AO2": dispatchNote.Locations?.Code,
                FromStorageLocation = dispatchNote.Locations?.Value,
                FromCustomerCode = null,

                ToPlantCode = null,
                ToStorageLocation = null,
                ToVendorCode = dispatchNote.Suppliers?.VendorCode,
                ToCustomerCode = null,

                TransporterCode = dispatchNote.Transporter?.TransporterCode,
                TransporterName = dispatchNote.Transporter?.TransporterName,
                TransportationMode = dispatchNote.TransporterMode,
                VehicleNo = dispatchNote.Vehicles?.VehicleNumber,
                VehicleSize = dispatchNote.Vehicles?.VehicleSize?.Code ?? dispatchNote.Vehicles?.VehicleSize?.Value,

                FrlrNo = dispatchNote.FrlrNumber,
                FrlrDate = dispatchNote.FrlrDate,
                TravellingDistance = 0,

                TransferFlag = null,
                TransferDate = null,
                GlobalUniqueId = null,
                BamSequenceId = null,
                OldGlobalUniqueId = null
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
                CgstRate = 0,
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
                TransporterName = gateOutRecord.TransName,
                TransportationMode = gateOutRecord.ZtransMode,
                VehicleNo = gateOutRecord.Vehicle,
                VehicleSize = gateOutRecord.VehSize,
                FrlrNo = gateOutRecord.FrlrNo,
                FrlrDate = gateOutRecord.FrlrDate,
                TravellingDistance = 0
            };
        }

        private static CommonInboundEntity MapShpcfmInboundToCommonInbound(ShpcfmEntity shpcfmRecord)
        {
            return new CommonInboundEntity
            {
                InterfaceId = ResolveShpcfmInterfaceId(shpcfmRecord.InterfaceId),
                TxnTypeCode = ShpcfmTxnTypeCode,
                Domain = ShpcfmTxnTypeCode,
                DocumentNo = shpcfmRecord.Attribute19,
                DocumentCreationDate = shpcfmRecord.RecordCreationDate,
                DocumentType = ShpcfmTxnTypeCode,
                InvoiceAmount = 0,
                CgstRate = 0,
                SgstUtRate = 0,
                IgstRate = 0,
                TaxAmountCgst = 0,
                TaxAmountSgstUtgst = 0,
                TaxAmountIgst = 0,
                InvoiceTotAmountWithtax = 0,
                FromPlantCode = shpcfmRecord.Werks,
                VehicleNo = shpcfmRecord.Attribute12,
                TransporterCode = shpcfmRecord.CarrierCode,
                TravellingDistance = 0,
                FrlrNo = null, 
                FrlrDate = null,
            };
        }

        private static decimal ResolveShpcfmInterfaceId(string? interfaceId)
        {
            if (decimal.TryParse(interfaceId, NumberStyles.Number, CultureInfo.InvariantCulture, out var parsedInterfaceId))
            {
                return parsedInterfaceId;
            }

            return 0;
        }
    }
}
