using BusinessLogic.Interfaces;
using BusinessLogic.Options;
using Microsoft.Extensions.Options;

namespace DC_Worker_Service
{
    public class ProcessBackgroundJob(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<DispatchNoteWorkerOptions> options,
        ILogger<ProcessBackgroundJob> logger) : BackgroundService
    {
        private readonly DispatchNoteWorkerOptions _options = options.Value;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(
                "Dispatch note background worker started with poll interval {PollIntervalSeconds} seconds.",
                _options.PollIntervalSeconds);

            while (!stoppingToken.IsCancellationRequested)
            {
                await RunTaskAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(_options.PollIntervalSeconds), stoppingToken);
            }
        }

        private async Task RunTaskAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dispatchNoteService = scope.ServiceProvider.GetRequiredService<IDispatchNoteService>();

            try
            {
                var result = await dispatchNoteService.ProcessPendingDispatchNotesAsync(cancellationToken);

                logger.LogInformation(
                    "Background cycle completed. fetched={Fetched}, processed={Processed}, failed={Failed}",
                    result.TotalFetched,
                    result.ProcessedCount,
                    result.FailedCount);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("Dispatch note background worker is stopping.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled error while executing dispatch note background cycle.");
            }
        }
    }
}
