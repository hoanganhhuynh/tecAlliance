using Domain.Entities;

namespace Application.Employee.Queries.Responses
{
    public record GetEmployeeResponse(
        string Name, string Position, DateTime HiringDate, decimal Salary);
}

