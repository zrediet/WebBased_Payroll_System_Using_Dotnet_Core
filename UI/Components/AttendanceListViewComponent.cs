using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Data;

namespace UI.Components
{
    public class AttendanceListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AttendanceListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(DateTime from, DateTime to, string division)
        {
            if (division != null)
            {
                ModelState.AddModelError("", "Please Select Department");
            }

            var attendance = await _context.Attendances
                .Include(a => a.Employee)
                .Where(c => c.From >= from && c.To <= to && 
                            c.Employee.DepartmentId == division && 
                            c.IsDeleted == false).ToListAsync();
            return View("AttendanceList",attendance);
        }
    }
}
