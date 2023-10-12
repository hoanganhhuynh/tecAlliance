using System;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
	public class EmployeeEntity : BaseEntity
	{
		public string Name { get; private set; } = string.Empty;
        public string Position { get; private set; } = string.Empty;
        public DateTime HiringDate { get; private set; }
		public decimal Salary { get; private set; }

        public EmployeeEntity(
            string name,
            string position,
            DateTime hiringDate,
            decimal salary) : base (Guid.NewGuid())
        {
			Name = name;
			Position = position;
			HiringDate = hiringDate;
			Salary = salary;
		}

        [JsonConstructor]
        public EmployeeEntity(
            Guid id,
            string name,
            string position,
            DateTime hiringDate,
            decimal salary) : base(id)
        {
            Name = name;
            Position = position;
            HiringDate = hiringDate;
            Salary = salary;
        }
    }
}

