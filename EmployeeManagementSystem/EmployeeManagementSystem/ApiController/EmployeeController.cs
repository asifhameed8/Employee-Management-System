using EMS.Models.Command;
using EMS.Models.Entities;
using EMS.Service.EmplyeeSrv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog.Fluent;
using Newtonsoft.Json;

namespace EMS.Web.ApiController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEmployeeById(long id)
        {
            try
            {
                var result = _employeeService.GetEmployeeById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
        [HttpGet]
        public IActionResult GetAllEmployee([FromQuery] JqueryDatatableParam param)
        {
            try
            {
                EmployeeCommand command = new EmployeeCommand();
                command.PagingData.Skip = param.iDisplayStart;
                command.PagingData.Take = param.iDisplayLength;
                if (param.sSearch != null && param.sSearch.Split(',').Length > 1)
                {
                    command.Name = param.sSearch.Split(',')[0].Split(":")[1];
                    command.Email = string.IsNullOrWhiteSpace(param.sSearch.Split(',')[1].Split(":")[1]) ? null : param.sSearch.Split(',')[1].Split(":")[1];
                    command.DepartmentName = string.IsNullOrWhiteSpace(param.sSearch.Split(',')[2].Split(":")[1]) ? null : param.sSearch.Split(',')[2].Split(":")[1];
                }
                var result = _employeeService.GetAllEmployee(command);
                if (result != null)
                {
                    var t = (new
                    {
                        param.sEcho,
                        iTotalRecords = result.Item2 / command.PagingData.Take,
                        iTotalDisplayRecords = result.Item2,
                        aaData = result.Item1
                    });
                    return Ok(t);
                }
                else
                {
                    return Ok(null);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPost]
        public IActionResult SaveEmployee([FromBody] Employee model)
        {
            try
            {
                var result = _employeeService.SaveEmployee(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmployee(long id)
        {
            try
            {
                var result = _employeeService.DeleteEmployee(id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}
