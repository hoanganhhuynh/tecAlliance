using System;
using Application.Employee.Commands;
using Application.Employee.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("employee")]
    public class EmployeeController : MediatorController
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(
            ILogger<EmployeeController> logger
            )
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll(GetEmployeesQuery request)
        {
            _logger.LogInformation("EmployeeController::Handle(...) => START {@Request}", request);
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await Mediator.Send(new GetEmployeeQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateEmployeeCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await Mediator.Send(new DeleteEmployeeCommand(id));
            return Ok(result);
        }
    }
}

