using Application.Employee.Queries.Responses;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Employee.Queries
{
	public record GetEmployeesQuery(
        int RecordsPerPage,
        int PageNumber) : IRequest<GetEmployeesResponse>;

    public class GetEmployeeQueryHandler: IRequestHandler<GetEmployeesQuery, GetEmployeesResponse>
	{
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<GetEmployeeQueryHandler> _logger;
        public GetEmployeeQueryHandler(
            IEmployeeRepository employeeRepository,
            ILogger<GetEmployeeQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<GetEmployeesResponse> Handle(
            GetEmployeesQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetEmployeeQueryHandler::Handle(...) => START {@Request}", request);
            var allEmployee = _employeeRepository.GetAll();
            var employees = allEmployee
                .Skip((request.PageNumber - 1) * request.RecordsPerPage)
                .Take(request.RecordsPerPage).ToList();

            var result = new GetEmployeesResponse(allEmployee.Count, employees);
            return await Task.FromResult<GetEmployeesResponse>(result);
        }
    }
}