using System;
using ReportService.Domain.Models;

namespace ReportService.Response
{
	public class PagedEmployeeResponse
	{
		public int Total { get; set; }
		public List<Employee> Data { get; set; } = new List<Employee>();
	}
}

