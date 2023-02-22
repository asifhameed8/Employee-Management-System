using AutoMapper;
using EMS.Models.Command;
using EMS.Models.ViewModels;
using EMS.Service;
using EMS.Service.EmplyeeSrv;
using EMS.Service.Uow;
using EMS.UnitTest.Mock;
using EMS.Util.logManager;
using EMS.Web.ApiController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data.Entity.Core.Objects;
using Xunit;

namespace EMS.UnitTest
{
    public class EmployeeControllerTest
    {
        private EmployeeController controller;
        private Mock<IEmployeeService> employeeServiceMock;
        private Mock<ILogger<EmployeeController>> logger;
        public IMapper GetMapper()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }

        //[Fact]
        //public void WhenGettingAllOwners_ThenAllOwnersReturn()
        //{
        //    var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        //    var mapper = GetMapper();
        //    employeeServiceMock = new Mock<IEmployeeService>();
        //    logger = new Mock<ILogger<EmployeeController>>();

        //    controller = new EmployeeController(repositoryWrapperMock.Object, logger.Object);

        //    var parm = new Mock<JqueryDatatableParam>();
        //    controller.GetAllEmployee(parm.Object);
        //    //var logger = new LoggerManager();
        //    //var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    //var employeeService = new EmployeeService(repositoryWrapperMock.Object, mapper);

        //    var command = new Mock<EmployeeCommand>();
        //    //EmployeeCommand command = new EmployeeCommand();
        //    //command.PagingData.CurrentPage = 0;
        //    //command.PagingData.Take = 10;

        //    var result = employeeServiceMock.Object.GetAllEmployee(command.Object).Item1;
        //    Assert.NotNull(result);
        //    Assert.Equal(StatusCodes.Status200OK, 200);//result.StatusCode
        //    Assert.IsAssignableFrom<IEnumerable<EmployeeVM>>(result);
        //    Assert.NotEmpty(result as IEnumerable<EmployeeVM>);
        //}


        //[Fact]
        //public void WhenGettingAllOwners_ThenAllOwnersReturn()
        //{
        //    var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        //    var mapper = GetMapper();
        //    var logger = new Mock<ILogger<EmployeeController>>();
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var employeeServiceMock = new Mock<IEmployeeService>();
        //    //var employeeService = new EmployeeService(repositoryWrapperMock, mapper);
        //    var controller = new EmployeeController(employeeServiceMock.Object, logger.Object);

        //    var command = new Mock<JqueryDatatableParam>();

        //    var result = controller.GetAllEmployee(command.Object) as ObjectResult;
        //    Assert.NotNull(result);
        //    Assert.Equal(StatusCodes.Status200OK, 200);//result.StatusCode
        //    Assert.IsAssignableFrom<IEnumerable<EmployeeVM>>(result);
        //    Assert.NotEmpty(result as IEnumerable<EmployeeVM>);
        //}
    }
}