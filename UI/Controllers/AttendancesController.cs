﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using UI.Data;

namespace UI.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Attendances.Include(a => a.Employee).Where(c=>c.IsDeleted == false);
            //await applicationDbContext.ToListAsync();

            var divisionList = await _context.Departments.Where(c => c.IsDeleted == false).OrderBy(a=>a.DepartmentName).ToListAsync();
            ViewData["DepartmentId"] = new SelectList(divisionList, "Id", "DepartmentName");

            return View();
        }

        public async Task<IActionResult> GetAttendance(DateTime from, DateTime to, string division)
        {
            var result = await _context.Attendances.Include(a=>a.Employee)
                .Where(c => c.From >= from && c.To <= to && c.Employee.Department.Id == division && c.IsDeleted == false)
                .ToListAsync();

            return View("");
        }
        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            var empList = _context.Employees.Where(c => c.IsDeleted == false)
                .Select(s => new
                {
                    Id = s.Id,
                    FullName = s.FirstName + " " + s.MiddleName + " " + s.LastName +" ("+s.EmployeeId+")",
                }).ToList();

            ViewData["EmployeeId"] = new SelectList(empList, "Id", "FullName");

            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,NoDays,Id,From,To,NormalOT,NormalOT2,WeekendOT,HolyDayOT,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId,AttendanceType")] Attendance attendance)
        {   

            //check if the Date is in a correct order
            if (attendance.From > attendance.To)
            {
                ModelState.AddModelError("","From should before To");
            }

            if (attendance.From > DateTime.Today)
            {
                ModelState.AddModelError("","Payment should be paid until Today. Not Starting From Today. Please Change.");
            }

            if (attendance.To > DateTime.Today)
            {
                ModelState.AddModelError("","Future Date Payment NOT Allowed. Please Change.");
            }

            var hiredDate = _context.Employees.Where(c => c.Id == attendance.EmployeeId).Select(a => a.HireDate);
            
            

            if (ModelState.IsValid)
            {
                //Iterate and add the attendance
                var diff = (attendance.To - attendance.From).TotalDays;

                for (int i = 0; i <= diff ; i++) //attendance.NoDays
                {
                    attendance.Id = Guid.NewGuid().ToString();
                    attendance.CreationTime = DateTime.Today;
                    attendance.CreatorUserId = "";
                    attendance.IsDeleted = false;
                    attendance.Date = attendance.From.AddDays(i);

                    _context.Add(attendance);
                    await _context.SaveChangesAsync();
                }
                 
                return RedirectToAction(nameof(Index));
            }

            var empList = _context.Employees.Where(c => c.IsDeleted == false)
                .Select(s => new
                {
                    Id = s.Id,
                    FullName = s.FirstName + " " + s.MiddleName + " " + s.LastName +" ("+s.EmployeeId+")",
                }).ToList();


            ViewData["EmployeeId"] = new SelectList(empList, "Id", "FullName", attendance.EmployeeId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", attendance.EmployeeId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeId,NoDays,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", attendance.EmployeeId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var attendance = await _context.Attendances.FindAsync(id);

            attendance.IsDeleted = true;
            attendance.LastModificationTime = DateTime.Now;
            attendance.LastModifierUserId = "";

            //_context.Attendances.Remove(attendance);
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(string id)
        {
            return _context.Attendances.Any(e => e.Id == id);
        }
    }
}