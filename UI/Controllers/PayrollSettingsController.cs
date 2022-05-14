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
    public class PayrollSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PayrollSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PayrollSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.PayrollSettings.Where(p=>p.IsDeleted == false).ToListAsync());
        }

        // GET: PayrollSettings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollSetting = await _context.PayrollSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payrollSetting == null)
            {
                return NotFound();
            }

            return View(payrollSetting);
        }

        // GET: PayrollSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PayrollSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GeneralPSett,Value,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] PayrollSetting payrollSetting)
        {
            payrollSetting.Id = Guid.NewGuid().ToString();
            payrollSetting.CreationTime = DateTime.Today;
            payrollSetting.CreatorUserId = "";
            payrollSetting.IsDeleted = false;

            //check if there is already registered Setting that is currently Active? If Active prompt to De-Activate
            var check = _context.PayrollSettings.Where(s =>
                s.GeneralPSett == payrollSetting.GeneralPSett && s.IsDeleted == false);
            
            if (check.Any())
            {
                ModelState.AddModelError("GeneralPSett","There is active setting for "+ payrollSetting.GeneralPSett+" please De-Activate/ Delete first.");
            }


            if (ModelState.IsValid)
            {
                _context.Add(payrollSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payrollSetting);
        }

        // GET: PayrollSettings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollSetting = await _context.PayrollSettings.FindAsync(id);
            if (payrollSetting == null)
            {
                return NotFound();
            }
            return View(payrollSetting);
        }

        // POST: PayrollSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("GeneralPSett,Value,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] PayrollSetting payrollSetting)
        {
            if (id != payrollSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payrollSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayrollSettingExists(payrollSetting.Id))
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
            return View(payrollSetting);
        }

        // GET: PayrollSettings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollSetting = await _context.PayrollSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payrollSetting == null)
            {
                return NotFound();
            }

            return View(payrollSetting);
        }

        // POST: PayrollSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var payrollSetting = await _context.PayrollSettings.FindAsync(id);
            payrollSetting.IsDeleted = true;
            payrollSetting.DeletionTime = DateTime.Now;

            _context.PayrollSettings.Update(payrollSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayrollSettingExists(string id)
        {
            return _context.PayrollSettings.Any(e => e.Id == id);
        }
    }
}
