using Application.Employee.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTest.Commands
{
    [TestFixture]
    public class EmployeeCommandTest
	{
        Mock<IEmployeeRepository> _employeeRepositoryMock;
        Mock<IMapper> _mapperMock;
        Mock<ILogger<CreateEmployeeCommandHandler>> _createLogger;
        Mock<ILogger<UpdateEmployeeCommandHandler>> _updateLogger;
        Mock<ILogger<DeleteEmployeeCommandHandler>> _deleteLogger;
        [SetUp]
        public void Setup()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _createLogger = new Mock<ILogger<CreateEmployeeCommandHandler>>();
            _updateLogger = new Mock<ILogger<UpdateEmployeeCommandHandler>>();
            _deleteLogger = new Mock<ILogger<DeleteEmployeeCommandHandler>>();
        }

        [Test]
        public async Task Create_Handler_Successfully()
        {
            var handler = new CreateEmployeeCommandHandler(_employeeRepositoryMock.Object, _createLogger.Object);
            var command = new CreateEmployeeCommand("New name", "New Pos", DateTime.UtcNow, 1400);
            await handler.Handle(command, CancellationToken.None);

            _employeeRepositoryMock.Verify(x => x.Insert(It.IsAny<EmployeeEntity>()), Times.Once);
        }

        [Test]
        public async Task Update_Handler_Successfully()
        {
            var handler = new UpdateEmployeeCommandHandler(_employeeRepositoryMock.Object, _mapperMock.Object, _updateLogger.Object);
            var command = new UpdateEmployeeCommand(new Guid("18709645-c5a4-43ff-b47e-f1da905acc60"),"Update name", "Update Pos", DateTime.UtcNow, 1500);
            await handler.Handle(command, CancellationToken.None);

            _employeeRepositoryMock.Verify(x => x.Update(It.IsAny<EmployeeEntity>()), Times.Once);
        }

        [Test]
        public async Task Delete_Handler_Successfully()
        {
            var mockEmployee = new EmployeeEntity(new Guid("18709645-c5a4-43ff-b47e-f1da905acc60"), "Update name", "Update Pos", DateTime.UtcNow, 1500);
            _employeeRepositoryMock.Setup(em => em.GetById(It.IsAny<Guid>())).Returns(mockEmployee);

            var handler = new DeleteEmployeeCommandHandler(_employeeRepositoryMock.Object, _deleteLogger.Object);
            var command = new DeleteEmployeeCommand(new Guid("18709645-c5a4-43ff-b47e-f1da905acc60"));
            await handler.Handle(command, CancellationToken.None);

            _employeeRepositoryMock.Verify(x => x.Delete(It.IsAny<EmployeeEntity>()), Times.Once);
        }

        [Test]
        public void Delete_Handler_Exception()
        {
            var handler = new DeleteEmployeeCommandHandler(_employeeRepositoryMock.Object, _deleteLogger.Object);
            var command = new DeleteEmployeeCommand(new Guid("18709645-c5a4-43ff-b47e-f1da905acc60"));
            Assert.That(async () => await handler.Handle(command, CancellationToken.None), Throws.Exception.TypeOf<NotFoundException>());
        }
    }
}

