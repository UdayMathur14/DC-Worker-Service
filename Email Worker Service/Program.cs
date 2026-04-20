using BusinessLogic.Extensions;
using DataAccess.Extensions;
using DC_Worker_Service;

var builder = Host.CreateDefaultBuilder(args)
                  .UseWindowsService()
                  .ConfigureServices((context, services) =>
                  {
                      services.AddHostedService<ProcessBackgroundJob>();
                      services.AddApplicationServices();
                      services.AddBusinessLayerServices();
                  });

var host = builder.Build();
host.Run();
