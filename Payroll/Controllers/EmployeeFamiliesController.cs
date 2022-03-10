using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Payroll.Data;

namespace Payroll.Controllers
{
    public class EmployeeFamiliesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeFamiliesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeFamilies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeeFamilies.Include(e => e.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeeFamilies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeFamily = await _context.EmployeeFamilies
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeFamily == null)
            {
                return NotFound();
            }

            return View(employeeFamily);
        }

        // GET: EmployeeFamilies/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: EmployeeFamilies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,EmployeeId,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] EmployeeFamily employeeFamily)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeFamily);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeFamily.EmployeeId);
            return View(employeeFamily);
        }

        // GET: EmployeeFamilies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeFamily = await _context.EmployeeFamilies.FindAsync(id);
            if (employeeFamily == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeFamily.EmployeeId);
            return View(employeeFamily);
        }

        // POST: EmployeeFamilies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FullName,EmployeeId,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] EmployeeFamily employeeFamily)
        {
            if (id != employeeFamily.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeFamily);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeFamilyExists(employeeFamily.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeFamily.EmployeeId);
            return View(employeeFamily);
        }

        // GET: EmployeeFamilies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeFamily = await _context.EmployeeFamilies
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeFamily == null)
            {
                return NotFound();
            }

            return View(employeeFamily);
        }

        // POST: EmployeeFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employeeFamily = await _context.EmployeeFamilies.FindAsync(id);
            _context.EmployeeFamilies.Remove(employeeFamily);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeFamilyExists(string id)
        {
            return _context.EmployeeFamilies.Any(e => e.Id == id);
        }
    }
}
