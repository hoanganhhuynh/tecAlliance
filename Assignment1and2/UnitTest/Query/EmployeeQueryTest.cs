using System;
using System.Text.Json;
using Application.Employee.Commands;
using Application.Employee.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Options;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTest.Query
{
    [TestFixture]
    public class EmployeeQueryTest
	{
        Mock<IEmployeeRepository> _employeeRepositoryMock;
        Mock<IMapper> _mapperMock;
        Mock<ILogger<GetEmployeeByIdQueryHandler>> _employeeByIdLogger;
        Mock<ILogger<GetEmployeeQueryHandler>> _employeesLogger;
        [SetUp]
        public void Setup()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _employeeByIdLogger = new Mock<ILogger<GetEmployeeByIdQueryHandler>>();
            _employeesLogger = new Mock<ILogger<GetEmployeeQueryHandler>>();
        }

        private List<EmployeeEntity> GetEmployees()
        {
            using (FileStream fileStream = File.OpenRead("./Data/employees.json"))
            {
                return JsonSerializer.Deserialize<List<EmployeeEntity>>(fileStream) ?? new List<EmployeeEntity>();
            }
        }

        [Test]
        public async Task Get_By_Id_Handler_Successfully()
        {
            var mockEmployee = new EmployeeEntity(new Guid("18709645-c5a4-43ff-b47e-f1da905acc60"), "Update name", "Update Pos", DateTime.UtcNow, 1500);
            _employeeRepositoryMock.Setup(em => em.GetById(It.IsAny<Guid>())).Returns(mockEmployee);

            var handler = new GetEmployeeByIdQueryHandler(_employeeRepositoryMock.Object, _mapperMock.Object, _employeeByIdLogger.Object);
            var command = new GetEmployeeQuery(new Guid("18709645-c5a4-43ff-b47e-f1da905acc60"));
            await handler.Handle(command, CancellationToken.None);

            _employeeRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void Get_By_Id_Handler_Exception()
        {
            var handler = new GetEmployeeByIdQueryHandler(_employeeRepositoryMock.Object, _mapperMock.Object, _employeeByIdLogger.Object);
            var command = new GetEmployeeQuery(new Guid("18709645-c5a4-43ff-b47e-f1da905acc60"));
            
            Assert.That(async () => await handler.Handle(command, CancellationToken.None), Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public async Task Get_All_Handler_Successfully()
        {
            var itemPerPage = 5;
            var mockEmployees = GetEmployees();
            _employeeRepositoryMock.Setup(em => em.GetAll()).Returns(mockEmployees);

            var handler = new GetEmployeeQueryHandler(_employeeRepositoryMock.Object, _employeesLogger.Object);
            var command = new GetEmployeesQuery(5, 0);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(itemPerPage, Is.EqualTo(result.Data.Count));
        }
    }
}

