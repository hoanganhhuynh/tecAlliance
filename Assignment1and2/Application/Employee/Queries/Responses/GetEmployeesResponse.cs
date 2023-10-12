using System;
using Domain.Entities;

namespace Application.Employee.Queries.Responses
{
	public record GetEmployeesResponse(
		int Total,
		List<EmployeeEntity> Data);
}

