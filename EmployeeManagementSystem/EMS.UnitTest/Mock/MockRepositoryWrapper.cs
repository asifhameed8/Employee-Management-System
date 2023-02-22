using EMS.Models.Command;
using EMS.Models.Entities;
using EMS.Models.ViewModels;
using EMS.Service.EmplyeeSrv;
using EMS.Service.Uow;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.UnitTest.Mock
{
    internal class MockRepositoryWrapper
    {
        public static Mock<IEmployeeService> GetMock()
        {
            var mock = new Mock<IEmployeeService>();
            var command = new Mock<EmployeeCommand>();
            
            // Setup the mock
            var employeeMock = new Mock<EmployeeVM>();
            var items = new List<EmployeeVM>()
            {
                employeeMock.Object
            };
            var employeeList = new Mock<Tuple<IEnumerable<EmployeeVM>, long>>(items, 1);
            mock.Setup(m => m.GetAllEmployee(command.Object)).Returns(() => employeeList.Object);
            mock.Setup(m => m.SaveChanges()).Callback(() => { return; });

            return mock;
        }
    }
}
