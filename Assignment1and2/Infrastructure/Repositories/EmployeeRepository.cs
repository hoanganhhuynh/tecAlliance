using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Options;
using Domain.Repositories;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
    {
        public static List<EmployeeEntity> _allEmployees = new List<EmployeeEntity>();
        private readonly MockDataOptions _mockDataOptions;
        public EmployeeRepository(IOptions<MockDataOptions> mockDataOptions)
		{
            _mockDataOptions = mockDataOptions.Value;
            if (_allEmployees.Count == 0)
            {
                using (FileStream fileStream = File.OpenRead(_mockDataOptions.Path))
                {
                    _allEmployees = JsonSerializer.Deserialize<List<EmployeeEntity>>(fileStream) ?? new List<EmployeeEntity>();
                }
            }
        }

        public void Delete(EmployeeEntity employee)
        {
            _allEmployees.Remove(employee);
        }

        public List<EmployeeEntity> GetAll()
        {
            return _allEmployees;
        }

        public EmployeeEntity? GetById(Guid id)
        {
            return _allEmployees.FirstOrDefault(employee => employee.Id == id);
        }

        public void Insert(EmployeeEntity employee)
        {
            _allEmployees.Add(employee);
        }

        public void Update(EmployeeEntity employee)
        {
            var updateEmployeeIndex = _allEmployees.FindIndex(emp => emp.Id == employee.Id);
            _allEmployees[updateEmployeeIndex] = employee;
        }
    }
}

