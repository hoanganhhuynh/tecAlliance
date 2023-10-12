using System.Globalization;
using System.Net.Mime;
using System.Text;
using CsvHelper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ReportService.Domain.Contracts;
using ReportService.Domain.Models;
using ReportService.Domain.Options;
using ReportService.Extensions;
using ReportService.Request;
using ReportService.Response;

namespace ReportService.Infrastructure.Service
{
	public class EmployeeReportService : IEmployeeReportService
    {
        private const string Token = "token";
        private readonly HttpClient _client;
        private readonly EmployeeServiceInfo _employeeServiceInfo;
        public EmployeeReportService(
            IHttpClientFactory httpClientFactory,
            IOptions<EmployeeServiceInfo> employeeServiceInfo
            )
		{
            _employeeServiceInfo = employeeServiceInfo.Value;
            _client = httpClientFactory.CreateClient(nameof(EmployeeReportService));
            _client.DefaultRequestHeaders.Add(
            Token, _employeeServiceInfo.Token);

        }

        public async Task ExportEmployees()
        {
            var url = $"{_employeeServiceInfo.Url}employee/all";
            var result = new List<Employee>();
            var index = 0;
            var folder = "Report";
            
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var path = Path.Combine(folder, GetFileName());
            using (var stream = File.Open(path, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                do
                {
                    result = await GetListEmployee(url, index);
                    csv.WriteRecords(result);
                    index++;
                }
                while (result.Count > 0);
            }   
        }


        private async Task<List<Employee>> GetListEmployee(string url, int index)
        {
            var request = new PagedDataRequest() { RecordsPerPage = 5, PageNumber = index };
            var content = new StringContent(request.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await _client.PostAsync(url, content);
            var response = await httpResponse?.Content.ReadAsStringAsync()!;

            var responseObject = JsonConvert.DeserializeObject<PagedEmployeeResponse>(response)!;
            return responseObject.Data;
        }

        private string GetFileName()
        {
            var currentDate = DateTime.Now;
            return $"EmployeeReport{currentDate.Day}{currentDate.Month}{currentDate.Year}{currentDate.Hour}{currentDate.Minute}{currentDate.Second}.csv";
        }
    }
}

