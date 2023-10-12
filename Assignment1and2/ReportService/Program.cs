using ReportService;
using ReportService.Domain.Contracts;
using ReportService.Domain.Options;
using ReportService.Infrastructure.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddHttpClient<IEmployeeReportService>();
        services.AddSingleton<IEmployeeReportService, EmployeeReportService>();
        services.Configure<EmployeeServiceInfo>(hostContext.Configuration.GetSection("EmployeeServiceInfo"));
    })
    .Build();

host.Run();

