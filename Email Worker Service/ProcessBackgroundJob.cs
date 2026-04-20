namespace DC_Worker_Service
{
    public class ProcessBackgroundJob(IServiceScopeFactory serviceScopeFactory, ILogger<ProcessBackgroundJob> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

        }
        private async Task RunTaskAsync(CancellationToken cancellationToken)
        {
         
        }
    }
}
