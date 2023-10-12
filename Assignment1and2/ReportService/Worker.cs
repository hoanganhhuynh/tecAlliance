using ReportService.Domain.Contracts;

namespace ReportService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IEmployeeReportService _employeeReportService;

    public Worker(
        ILogger<Worker> logger,
        IEmployeeReportService employeeReportService)
    {
        _logger = logger;
        _employeeReportService = employeeReportService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await _employeeReportService.ExportEmployees();

            await Task.Delay(600000, stoppingToken);
        }
    }
}

