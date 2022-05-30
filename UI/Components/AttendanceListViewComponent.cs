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

        public async Task<IViewComponentResult> InvokeAsync(int month, int year, string studentId)
        { 
            //If studentID is zero (Default value sent from the view), select all the employee attendance
            //If StudentID is different than zero search according to the given ID
            

            var  attend =_context.Attendances
                .Include(a => a.Employee)
                .Where(c => c.Date.Month == month && c.Date.Year == year && 
                            c.Employee.Id == studentId && 
                            c.IsDeleted == false).ToList();

            if (studentId == "0")
            {
                attend = _context.Attendances
                    .Include(a => a.Employee)
                    .Where(c => c.Date.Month == month && c.Date.Year == year && 
                                c.IsDeleted == false).ToList();
            }
            
              
            

            
            
            return View("AttendanceList",attend);
        }
    }
}
