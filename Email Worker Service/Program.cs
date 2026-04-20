using BusinessLogic.Extensions;
using BusinessLogic.Options;
using DataAccess.Extensions;
using DC_Worker_Service;

var builder = Host.CreateDefaultBuilder(args)
                  .UseWindowsService()
                  .ConfigureServices((context, services) =>
                  {
                      services.Configure<DispatchNoteWorkerOptions>(
                          context.Configuration.GetSection(DispatchNoteWorkerOptions.SectionName));

                      services.AddHostedService<ProcessBackgroundJob>();
                      services.AddApplicationServices();
                      services.AddBusinessLayerServices();
                  });

var host = builder.Build();
host.Run();
