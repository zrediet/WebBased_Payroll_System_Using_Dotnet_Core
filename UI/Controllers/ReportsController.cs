using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ReportsController : Controller
    {
        //public ReportsController(IReportServiceConfiguration reportServiceConfiguration)
        //    : base(reportServiceConfiguration)
        //{
        //}
        
        public IActionResult Viewer()
        {
            return View();
        }
    }
}