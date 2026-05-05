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

        public bool IsGateOutConfigured()
        {
            return IntfDbContext is not null;
        }

        public async Task<IReadOnlyList<DispatchNoteEntity>> GetDispatchInboundAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await applicationDbContext.DispatchNoteEntity
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
                return await IntfDbContext.GateOutInboundEntities
                    .OrderBy(item => item.InterfaceId)
                    .ToListAsync(cancellationToken);
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
    }
}
