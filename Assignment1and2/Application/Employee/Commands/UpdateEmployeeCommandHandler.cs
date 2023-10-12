using System;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Employee.Commands
{
    public record UpdateEmployeeCommand(
        Guid Id,
        string Name,
        string Position,
        DateTime HiringDate,
        decimal Salary) : IRequest<Unit>;

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
        public UpdateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            ILogger<UpdateEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateEmployeeCommandHandler::Handle(...) => START {@Request}", request);
            var employee = _mapper.Map<EmployeeEntity>(request);
            await Task.Run(() => _employeeRepository.Update(employee));
            return Unit.Value;
        }
    }
}

