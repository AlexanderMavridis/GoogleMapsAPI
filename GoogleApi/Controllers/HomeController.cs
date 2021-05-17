using GoogleApi.Data;
using GoogleApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GoogleApiProjectDBContext _context;

        public HomeController(ILogger<HomeController> logger, GoogleApiProjectDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("getAttributes")]
        public ActionResult GetAttributes()
        {
            var attributes = _context.Attributes.ToList();
            return View(attributes);
        }

        
        [HttpGet]
        [Route("GetOneEmployees")]
        public ActionResult GetOneEmployees()
        {
            var employee = _context.Employees.FirstOrDefault(emp => emp.EmpName == "Aura");
            return View(employee);
        }

        //attributes
        public ActionResult Attributes()
        {
            return View();
        }

        public ActionResult AttributeDetails()
        {
            return View();
        }

        public ActionResult CreateAttribute()
        {
            return View();
        }

        //employees
        public ActionResult Employees()
        {
            return View();
        }

        public ActionResult EmployeeDetails()
        {
            return View();
        }

        public ActionResult CreateEmployee()
        {
            return View();
        }

        //Map
        public ActionResult TestingMap()
        {
            return View();
        }

        public ActionResult EmployeesWithRelatedAttributes()
        {
            return View();
        }

    }
}
