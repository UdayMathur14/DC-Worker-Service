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
        private const string ProcessedFlag = "Y";
        private IntfDbContext? IntfDbContext => serviceProvider.GetService<IntfDbContext>();

        public bool IsGateOutConfigured()
        {
            return IntfDbContext is not null;
        }

        public async Task<IReadOnlyList<DispatchNoteEntity>> GetDispatchInboundAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await applicationDbContext.DispatchNoteEntity
                    .Where(note => note.Attribute4 == null || note.Attribute4.Trim().ToUpper() != ProcessedFlag)
                    .Include(note => note.Locations)
                    .Include(note => note.Suppliers)
                    .Include(note => note.Vehicles)
                        .ThenInclude(vehicle => vehicle.VehicleSize)
                    .Include(note => note.Transporter)
                    .Include(note => note.DispatchNotePartEntities)
                        .ThenInclude(item => item.PartEntity)
                    .OrderBy(note => note.Id)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Repository error in {MethodName}.", nameof(GetDispatchInboundAsync));
                throw;
            }
        }

        public async Task<IReadOnlyList<GateOutInboundEntity>> GetGateOutInboundAsync(CancellationToken cancellationToken)
        {
            try
            {
                var intfDbContext = IntfDbContext
                    ?? throw new InvalidOperationException("Gate-out interface database context is not configured.");

                return await (
                    from gateOut in intfDbContext.GateOutInboundEntities

                    where gateOut.Attribute4 == null
                       || gateOut.Attribute4.Trim().ToUpper() != ProcessedFlag

                    orderby gateOut.InterfaceId

                    select new GateOutInboundEntity
                    {
                        InterfaceId = gateOut.InterfaceId,
                        ZtxnType = gateOut.ZtxnType,
                        Zdomain = gateOut.Zdomain,
                        Werks = gateOut.Werks,
                        ZdocumentNo = gateOut.ZdocumentNo,
                        ZdocumentDate = gateOut.ZdocumentDate,
                        TransId = gateOut.TransId,
                        TransName = gateOut.TransName,
                        ZtransMode = gateOut.ZtransMode,
                        Vehicle = gateOut.Vehicle,
                        VehSize = gateOut.VehSize,
                        Attribute4 = gateOut.Attribute4,
                        FrlrNo = null,
                        FrlrDate = null
                    }
                ).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Repository error in {MethodName}.", nameof(GetGateOutInboundAsync));
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
                applicationDbContext.Entry(commonInboundEntity).State = EntityState.Detached;

                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. interfaceId={InterfaceId}, txnTypeCode={TxnTypeCode}, documentNo={DocumentNo}",
                    nameof(InsertCommonInboundAsync),
                    commonInboundEntity.InterfaceId,
                    commonInboundEntity.TxnTypeCode,
                    commonInboundEntity.DocumentNo);

                throw;
            }
        }

        public async Task<IReadOnlyList<ShpcfmEntity>> GetShpcfmInboundAsync(CancellationToken cancellationToken)
        {
            try
            {
                var intfDbContext = IntfDbContext
                    ?? throw new InvalidOperationException("SHPCFM interface database context is not configured.");

                return await intfDbContext.ShpcfmEntity
                    .Where(item => item.Attribute2 == null || item.Attribute2.Trim().ToUpper() != ProcessedFlag)
                    .OrderBy(item => item.InterfaceId)
                    .ToListAsync(cancellationToken);        
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Repository error in {MethodName}.", nameof(GetShpcfmInboundAsync));
                throw;
            }
        }

        public async Task MarkDispatchInboundProcessedAsync(decimal dispatchNoteId, CancellationToken cancellationToken)
        {
            try
            {
                await applicationDbContext.DispatchNoteEntity
                    .Where(note => note.Id == dispatchNoteId)
                    .ExecuteUpdateAsync(
                        updates => updates.SetProperty(note => note.Attribute4, ProcessedFlag),
                        cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. dispatchNoteId={DispatchNoteId}",
                    nameof(MarkDispatchInboundProcessedAsync),
                    dispatchNoteId);

                throw;
            }
        }

        public async Task MarkGateOutInboundProcessedAsync(decimal interfaceId, CancellationToken cancellationToken)
        {
            try
            {
                var intfDbContext = IntfDbContext ?? throw new InvalidOperationException("Gate-out interface database context is not configured.");

                await intfDbContext.GateOutInboundEntities
                    .Where(item => item.InterfaceId == interfaceId)
                    .ExecuteUpdateAsync(
                        updates => updates.SetProperty(item => item.Attribute4, ProcessedFlag),
                        cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. interfaceId={InterfaceId}",
                    nameof(MarkGateOutInboundProcessedAsync),
                    interfaceId);

                throw;
            }
        }

        public async Task MarkShpcfmInboundProcessedAsync(string? interfaceId, CancellationToken cancellationToken)
        {
            try
            {
                var intfDbContext = IntfDbContext ?? throw new InvalidOperationException("SHPCFM interface database context is not configured.");

                await intfDbContext.ShpcfmEntity
                    .Where(item => item.InterfaceId == interfaceId)
                    .ExecuteUpdateAsync(
                        updates => updates.SetProperty(item => item.Attribute2, ProcessedFlag),
                        cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Repository error in {MethodName}. interfaceId={InterfaceId}",
                    nameof(MarkShpcfmInboundProcessedAsync),
                    interfaceId);

                throw;
            }
        }
    }
}
