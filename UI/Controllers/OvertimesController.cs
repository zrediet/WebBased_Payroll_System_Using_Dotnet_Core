using System;
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
    public class OvertimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OvertimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Overtimes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Overtimes.Include(o => o.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Overtimes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var overtime = await _context.Overtimes
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (overtime == null)
            {
                return NotFound();
            }

            return View(overtime);
        }

        // GET: Overtimes/Create
        public IActionResult Create()
        {
            var empList = _context.Employees.Where(c => c.IsDeleted == false)
                .Select(s => new
                {
                    Id = s.Id,
                    FullName = s.FirstName + " " + s.MiddleName + " " + s.LastName +" ("+s.EmployeeId+")",
                }).ToList();

            ViewData["EmployeeId"] = new SelectList(empList, "Id", "FullName");

            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: Overtimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,NormalOT,NormalOT2,WeekendOT,HolyDayOT,Date,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] Overtime overtime)
        {
            if (overtime.Date > DateTime.Today)
            {
                ModelState.AddModelError("","Registering OT in future date is not allowed.");
            }

            //check if the employee has already registered OT that is Active for same DATE
            var check = _context.Overtimes.Where(c =>
                c.EmployeeId == overtime.EmployeeId && c.Date == overtime.Date && c.IsDeleted == false);

            if (check.Any())
            {
                ModelState.AddModelError("","Employee has active OT in this date. If you want to change, Delete first.");
            }

            


            if (ModelState.IsValid)
            {
                overtime.CreationTime = DateTime.Today;
                overtime.IsDeleted = false;
                overtime.CreatorUserId = "";
                overtime.Id = Guid.NewGuid().ToString();


                _context.Add(overtime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var empList = _context.Employees.Where(c => c.IsDeleted == false)
                .Select(s => new
                {
                    Id = s.Id,
                    FullName = s.FirstName + " " + s.MiddleName + " " + s.LastName +" ("+s.EmployeeId+")",
                }).ToList();

            ViewData["EmployeeId"] = new SelectList(empList, "Id", "FullName", overtime.EmployeeId);

            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", overtime.EmployeeId);
            return View(overtime);
        }

        // GET: Overtimes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var overtime = await _context.Overtimes.FindAsync(id);
            if (overtime == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", overtime.EmployeeId);
            return View(overtime);
        }

        // POST: Overtimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeId,NormalOT,NormalOT2,WeekendOT,HolyDayOT,Date,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] Overtime overtime)
        {
            if (id != overtime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(overtime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OvertimeExists(overtime.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", overtime.EmployeeId);
            return View(overtime);
        }

        // GET: Overtimes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var overtime = await _context.Overtimes
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (overtime == null)
            {
                return NotFound();
            }

            return View(overtime);
        }

        // POST: Overtimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var overtime = await _context.Overtimes.FindAsync(id);
            _context.Overtimes.Remove(overtime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OvertimeExists(string id)
        {
            return _context.Overtimes.Any(e => e.Id == id);
        }
    }
}
