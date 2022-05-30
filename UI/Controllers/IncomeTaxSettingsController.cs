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
    public class IncomeTaxSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncomeTaxSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IncomeTaxSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.IncomeTaxSetting.OrderBy(a=>a.StartingAmount).ToListAsync());
        }

        // GET: IncomeTaxSettings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeTaxSetting = await _context.IncomeTaxSetting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeTaxSetting == null)
            {
                return NotFound();
            }

            return View(incomeTaxSetting);
        }

        // GET: IncomeTaxSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncomeTaxSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartingAmount,EndingAmount,Percent,Deductable,ActiveDate,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] IncomeTaxSetting incomeTaxSetting)
        {
            incomeTaxSetting.Id = Guid.NewGuid().ToString();
            incomeTaxSetting.CreationTime = DateTime.Today;
            incomeTaxSetting.IsDeleted = false;
            incomeTaxSetting.CreatorUserId = "";

            if (ModelState.IsValid)
            {
                _context.Add(incomeTaxSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incomeTaxSetting);
        }

        // GET: IncomeTaxSettings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeTaxSetting = await _context.IncomeTaxSetting.FindAsync(id);
            if (incomeTaxSetting == null)
            {
                return NotFound();
            }
            return View(incomeTaxSetting);
        }

        // POST: IncomeTaxSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StartingAmount,EndingAmount,Percent,Deductable,ActiveDate,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] IncomeTaxSetting incomeTaxSetting)
        {
            if (id != incomeTaxSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomeTaxSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeTaxSettingExists(incomeTaxSetting.Id))
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
            return View(incomeTaxSetting);
        }

        // GET: IncomeTaxSettings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeTaxSetting = await _context.IncomeTaxSetting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeTaxSetting == null)
            {
                return NotFound();
            }

            return View(incomeTaxSetting);
        }

        // POST: IncomeTaxSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var incomeTaxSetting = await _context.IncomeTaxSetting.FindAsync(id);
            _context.IncomeTaxSetting.Remove(incomeTaxSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeTaxSettingExists(string id)
        {
            return _context.IncomeTaxSetting.Any(e => e.Id == id);
        }
    }
}
