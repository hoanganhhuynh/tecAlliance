using System;
namespace ReportService.Domain.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime HiringDate { get; set; }
        public decimal Salary { get; set; }
    }
}

