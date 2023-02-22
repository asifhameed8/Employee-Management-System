using EMS.Models.Command;
using EMS.Models.Entities;
using EMS.Service.EmplyeeSrv;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EMS.UnitTest.Mock
{
    internal class MockIOwnerRepository
    {
        public static Mock<IEmployeeService> GetMock()
        {
            var mock = new Mock<IEmployeeService>();
            var employee = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Name = "John",
                    DateOfBirth = DateTime.Now.AddYears(-20),
                    DepartmentName = "Accounts",
                    Email = "john@gmail.com"
                }
            };
            // Set up
            EmployeeCommand command = new EmployeeCommand();
            command.PagingData.CurrentPage = 0;
            command.PagingData.Take = 10;
            
            command.Name = null;
            command.Email = null;
            command.DepartmentName = null;
            
            mock.Setup(m => m.GetAllEmployee(command)).Returns(() => employee);

            mock.Setup(m => m.GetEmployeeById(It.IsAny<long>())).Returns((long id) => employee.FirstOrDefault(o => o.Id == id));

            mock.Setup(m => m.SaveEmployee(It.IsAny<Employee>()))
    .Callback(() => { return; });
            mock.Setup(m => m.SaveEmployee(It.IsAny<Employee>()))
               .Callback(() => { return; });
            mock.Setup(m => m.DeleteEmployee(It.IsAny<long>()))
               .Callback(() => { return; });

            return mock;
        }

    }
}
