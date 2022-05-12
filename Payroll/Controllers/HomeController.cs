using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Models;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            GetAdminBasicInformation();
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

        public bool GetAdminBasicInformation()
        {

            int totalEmployee = _context.Employees.Count(c=>c.IsDeleted == false);
            int divisions = _context.Departments.Count(c => c.IsDeleted == false);


            ViewBag.TotalEmployee = totalEmployee;
            ViewBag.TotalDivisions = divisions;
            //ViewBag.TotalProjects = projects;

            return true;
        }

       
    }

}
