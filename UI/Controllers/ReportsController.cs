using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class ReportsController : Controller
    {
        //public ReportsController(IReportServiceConfiguration reportServiceConfiguration)
        //    : base(reportServiceConfiguration)
        //{
        //}
        
        public IActionResult PayrollReport()
        {
            return View();
        }

        public IActionResult BankReport()
        {
            return View();
        }
    }
}