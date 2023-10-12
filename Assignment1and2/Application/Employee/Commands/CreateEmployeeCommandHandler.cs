using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Employee.Commands
{
    public record CreateEmployeeCommand(
        string Name,
        string Position,
        DateTime HiringDate,
        decimal Salary) : IRequest<Unit>;

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Unit>
	{
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;
        public CreateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            ILogger<CreateEmployeeCommandHandler> logger)
		{
            _employeeRepository = employeeRepository;
            _logger = logger;
		}

        public async Task<Unit> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateEmployeeCommandHandler::Handle(...) => START {@Request}", request);
            var employee = new EmployeeEntity(
                request.Name,
                request.Position,
                request.HiringDate,
                request.Salary);
            await Task.Run(() => _employeeRepository.Insert(employee));
            return Unit.Value;
        }
    }
}

