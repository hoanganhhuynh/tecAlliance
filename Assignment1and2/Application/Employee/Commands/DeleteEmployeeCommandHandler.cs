using System;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Employee.Commands
{
    public record DeleteEmployeeCommand(Guid Id) : IRequest<Unit>;

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
	{
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;
        public DeleteEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            ILogger<DeleteEmployeeCommandHandler> logger)
		{
            _employeeRepository = employeeRepository;
            _logger = logger;
		}

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DeleteEmployeeCommandHandler::Handle(...) => START {@Request}", request);
            var employee = _employeeRepository.GetById(request.Id);
            if (employee == null)
            {
                throw new Domain.Exceptions.NotFoundException("Employee does not exist.");
            }
            
            await Task.Run(() => _employeeRepository.Delete(employee));
            return Unit.Value;
        }
    }
}

