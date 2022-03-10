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
    public class TerminationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TerminationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Terminations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Terminations.Include(t => t.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Terminations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termination = await _context.Terminations
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (termination == null)
            {
                return NotFound();
            }

            return View(termination);
        }

        // GET: Terminations/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: Terminations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,TerminationDate,TerminationReason,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] Termination termination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(termination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", termination.EmployeeId);
            return View(termination);
        }

        // GET: Terminations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termination = await _context.Terminations.FindAsync(id);
            if (termination == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", termination.EmployeeId);
            return View(termination);
        }

        // POST: Terminations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeId,TerminationDate,TerminationReason,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] Termination termination)
        {
            if (id != termination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(termination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminationExists(termination.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", termination.EmployeeId);
            return View(termination);
        }

        // GET: Terminations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termination = await _context.Terminations
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (termination == null)
            {
                return NotFound();
            }

            return View(termination);
        }

        // POST: Terminations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var termination = await _context.Terminations.FindAsync(id);
            _context.Terminations.Remove(termination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminationExists(string id)
        {
            return _context.Terminations.Any(e => e.Id == id);
        }
    }
}
