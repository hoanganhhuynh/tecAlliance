using System;
using Application.Employee.Commands;
using Application.Employee.Queries.Responses;
using AutoMapper;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Employee.Queries
{
	public record GetEmployeeQuery(Guid Id) : IRequest<GetEmployeeResponse>;

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeQuery, GetEmployeeResponse>
	{
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmployeeByIdQueryHandler> _logger;
        public GetEmployeeByIdQueryHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            ILogger<GetEmployeeByIdQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetEmployeeResponse> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetEmployeeByIdQueryHandler::Handle(...) => START {@Request}", request);
            var employee = _employeeRepository.GetById(request.Id);

            if (employee == null)
            {
                throw new NotFoundException("Employee does not exist.");
            }
            
            return await Task.FromResult<GetEmployeeResponse>(_mapper.Map<GetEmployeeResponse>(employee));
        }
    }
}

