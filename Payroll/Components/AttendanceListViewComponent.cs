using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;

namespace Payroll.Components
{
    public class AttendanceListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AttendanceListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> ViewAttendanceList(DateTime from, DateTime to, PaymentRound round, string division)
        {
            //if (division != null)
            //{
            //    ModelState.AddModelError("","Please Select Division");
            //}


            var attendance = await _context.Attendances
                .Include(a => a.Employee)
                .Where(c => c.From >= from && c.To <= to && 
                            c.Employee.DivisionId == division && 
                            c.IsDeleted == false).ToListAsync();
            return View("AttendanceList",attendance);
        }
    }
}
