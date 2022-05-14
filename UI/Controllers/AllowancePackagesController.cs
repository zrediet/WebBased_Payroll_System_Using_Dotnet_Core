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
    public class AllowancePackagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllowancePackagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AllowancePackages
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllowancePackages.ToListAsync());
        }

        // GET: AllowancePackages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancePackage = await _context.AllowancePackages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allowancePackage == null)
            {
                return NotFound();
            }

            return View(allowancePackage);
        }

        // GET: AllowancePackages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllowancePackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackageName,PackageVersion,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] AllowancePackage allowancePackage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allowancePackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allowancePackage);
        }

        // GET: AllowancePackages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancePackage = await _context.AllowancePackages.FindAsync(id);
            if (allowancePackage == null)
            {
                return NotFound();
            }
            return View(allowancePackage);
        }

        // POST: AllowancePackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PackageName,PackageVersion,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] AllowancePackage allowancePackage)
        {
            if (id != allowancePackage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allowancePackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllowancePackageExists(allowancePackage.Id))
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
            return View(allowancePackage);
        }

        // GET: AllowancePackages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancePackage = await _context.AllowancePackages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allowancePackage == null)
            {
                return NotFound();
            }

            return View(allowancePackage);
        }

        // POST: AllowancePackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var allowancePackage = await _context.AllowancePackages.FindAsync(id);
            _context.AllowancePackages.Remove(allowancePackage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllowancePackageExists(string id)
        {
            return _context.AllowancePackages.Any(e => e.Id == id);
        }
    }
}
