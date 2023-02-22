using AutoMapper;
using Castle.Core.Logging;
using EMS.Core.Repositories;
using EMS.Models.Command;
using EMS.Models.Entities;
using EMS.Models.ViewModels;
using EMS.Service;
using EMS.Service.EmplyeeSrv;
using EMS.Service.Uow;
using EMS.Web.ApiController;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace EMS.UnitTest
{
    public class EmployeeControllerNUnitTest
    {
        private EmployeeController _controller;
        private Mock<IEmployeeService> _employeeServiceMock;
        private Mock<IUnitOfWork> mockReadItemService;
        private Mock<ILogger<EmployeeController>> _logger;
        private Mock<Employee> _employee;
        private Mock<EmployeeCommand> _employeeCommand;
        public IMapper GetMapper()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }

        [SetUp]
        public void Setup()
        {

            _employeeServiceMock = new Mock<IEmployeeService>();
            mockReadItemService = new Mock<IUnitOfWork>();
            _employee = new Mock<Employee>();
            _employeeCommand = new Mock<EmployeeCommand>();
            _logger = new Mock<ILogger<EmployeeController>>();
            // arrange

            // 
            var employeeMock = new Mock<EmployeeVM>();
            //employeeMock.Setup(item => item.Name).Returns("john");

            var items = new List<EmployeeVM>()
            {
                employeeMock.Object
            };

            Tuple<IEnumerable<EmployeeVM>, long> employeeList = new Tuple<IEnumerable<EmployeeVM>, long>(items, 1);

            _employeeServiceMock.Setup(c => c.GetAllEmployee(_employeeCommand.Object)).Returns(employeeList);
            _employeeServiceMock.Setup(c => c.GetRepository()).Returns(new Mock<IGenericRepository<Employee>>().Object);
            mockReadItemService.Setup(c => c.GetRepository<Employee>()).Returns(new Mock<IGenericRepository<Employee>>().Object);

            _controller = new EmployeeController(_employeeServiceMock.Object, _logger.Object);
        }

        [Test]
        public void EmployeeList()
        {
            var employeeMock = new Mock<EmployeeVM>();

            var items = new List<EmployeeVM>()
            {
                employeeMock.Object
            };

            Tuple<IEnumerable<EmployeeVM>, long> employeeList = new Tuple<IEnumerable<EmployeeVM>, long>(items, 1);

            _employeeServiceMock.Setup(c => c.GetAllEmployee(_employeeCommand.Object)).Returns(employeeList);

            _employeeCommand.Object.Name = "asif";
            // act
            //var result = _employeeServiceMock.Setup(c => c.GetAllEmployee(_employeeCommand.Object)).Returns(employeeList);

            var param = new Mock<JqueryDatatableParam>();
            param.Object.iDisplayStart = 0;
            param.Object.iDisplayLength= 10;
            var result = _controller.GetAllEmployee(param.Object);

            // assert
            _employeeServiceMock.Verify(s => s.GetAllEmployee(_employeeCommand.Object), Times.Never());

            NUnit.Framework.Assert.AreEqual(null, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value);
        }

        [Test]
        public void Test_GetAllEmployees_ReturnsListOfEmployeesItReceivesFromReadAllMethodOfReadItemCommand_WhenCalled()
        {
            //Arrange
            var mapper = GetMapper();

            var employeeService = new EmployeeService(mockReadItemService.Object, mapper);

            var employeeList = new List<EmployeeVM>(){
                new EmployeeVM {
                    Name = "john",
                    Email = "jogn@gmail.com",
                    DepartmentName = "Accounts",
                    DateOfBirth = Convert.ToDateTime("2/2/2022")
                },
                new EmployeeVM {
                    Name = "welliam",
                    Email = "welliam@gmail.com",
                    DepartmentName = "Dev",
                    DateOfBirth = Convert.ToDateTime("1/1/2000") 
                    //populate other properties  
                }
            };

            var expected = new Tuple<IEnumerable<EmployeeVM>, long>(employeeList, 2);
            

            _employeeServiceMock
                .Setup(_ => _.GetAllEmployee(_employeeCommand.Object))
                .Returns(expected);

            //Act
            var actual = employeeService.GetAllEmployee(_employeeCommand.Object);

            //Assert
            NUnit.Framework.Assert.AreNotSame(expected, actual);
        }

        [Test]
        public void AddEmployeeIssue()
        {
            var employee = new Mock<Employee>().Object;
            employee.DepartmentName = "Account";
            employee.DateOfBirth = Convert.ToDateTime("2/2/2000");
            employee.Email = "taha@gmail.com";

            long employeeId = 0;
            _employeeServiceMock.Setup(c => c.SaveEmployee(employee).Result).Returns(employeeId);

            // act
            //var result = _employeeServiceMock.Setup(c => c.GetAllEmployee(_employeeCommand.Object)).Returns(employeeList);

            var result = _controller.SaveEmployee(employee);

            // assert
            _employeeServiceMock.Verify(s => s.SaveEmployee(employee), Times.Once());

            NUnit.Framework.Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
        }

        [Test]
        public void AddEmployee()
        {
            var employee = new Mock<Employee>().Object;
            employee.Name= "Test";
            employee.DepartmentName = "Account";
            employee.DateOfBirth = Convert.ToDateTime("2/2/2000");
            employee.Email = "asif@gmail.com";

            long employeeId = 1;
            _employeeServiceMock.Setup(c => c.SaveEmployee(employee).Result).Returns(employeeId);

            // act
            //var result = _employeeServiceMock.Setup(c => c.GetAllEmployee(_employeeCommand.Object)).Returns(employeeList);

            var result = _controller.SaveEmployee(employee);

            // assert
            _employeeServiceMock.Verify(s => s.SaveEmployee(employee), Times.Once());

            NUnit.Framework.Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
        }
    }
}
