using System;
using ReportService.Domain.Models;

namespace ReportService.Domain.Contracts
{
	public interface IEmployeeReportService
	{
		Task ExportEmployees();
	}
}

