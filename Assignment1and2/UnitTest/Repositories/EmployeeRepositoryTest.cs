using System;
using Domain.Entities;
using Domain.Options;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;

namespace UnitTest.Repositories
{
    [TestFixture]
    public class EmployeeRepositoryTest
	{
        IEmployeeRepository _employeeRepository;
        
        [SetUp]
        public void Setup()
        {
            var option = Options.Create(new MockDataOptions() { Path = "./Data/employees.json" });
            _employeeRepository = new EmployeeRepository(option);
        }


        [Test]
        public void GetAll()
        {
            var employees = _employeeRepository.GetAll();
            Assert.That(10, Is.EqualTo(employees.Count));
        }

        [Test]
        public void GetById_No_Record()
        {
            var id = Guid.NewGuid();
            var employee = _employeeRepository.GetById(id);
            Assert.IsNull(employee);
        }

        [Test]
        public void GetById_Has_Record()
        {
            var id = new Guid("2aff582d-b4af-4ba8-9dfa-6a8186009898");
            var employee = _employeeRepository.GetById(id);
            Assert.IsNotNull(employee);
        }

        [Test]
        public void Delete()
        {
            var id = new Guid("18709645-c5a4-43ff-b47e-f1da905acc60");
            var employee = _employeeRepository.GetById(id);
            _employeeRepository.Delete(employee!);
            employee = _employeeRepository.GetById(id);
            Assert.IsNull(employee);
        }

        [Test]
        public void Update_Successfully()
        {
            var id = new Guid("2aff582d-b4af-4ba8-9dfa-6a8186009898");

            var employee = new EmployeeEntity(id,"Update name", "Test", new DateTime(), 2304);
            _employeeRepository.Update(employee!);
            var updatedEmployee = _employeeRepository.GetById(id);
            employee = _employeeRepository.GetById(id);
            Assert.That(updatedEmployee!.Name, Is.EqualTo("Update name"));
        }

        [Test]
        public void Create_Successfully()
        {
            var employee = new EmployeeEntity(Guid.NewGuid(), "Create", "Test", new DateTime(), 2204);
            _employeeRepository.Insert(employee);
            var insertedEmployee = _employeeRepository.GetById(employee.Id);
            Assert.IsNotNull(insertedEmployee);
            Assert.That(insertedEmployee!.Name, Is.EqualTo("Create"));
        }
    }
}

