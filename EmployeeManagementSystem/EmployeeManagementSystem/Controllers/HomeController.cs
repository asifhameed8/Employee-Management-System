﻿
using EMS.Service.EmplyeeSrv;
using EMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult AddEmployee(int employeeId)
        {
            var employee = _employeeService.GetEmployeeById(employeeId);
            return PartialView(employee);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}