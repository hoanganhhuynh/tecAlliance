using System;
namespace ReportService.Request
{
	public class PagedDataRequest
	{
		public int RecordsPerPage { get; set; }
        public int PageNumber { get; set; }
	}
}

