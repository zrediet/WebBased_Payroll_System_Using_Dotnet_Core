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
    public class SubProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubProjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubProjects.Include(s => s.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubProjects/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProjects
                .Include(s => s.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProject == null)
            {
                return NotFound();
            }

            return View(subProject);
        }

        // GET: SubProjects/Create
        public IActionResult Create()
        {
            ViewData["ProjectMainId"] = new SelectList(_context.Projects.OrderBy(c=>c.ProjectName), "Id", "ProjectName");
            ViewData["SubProjectId"] = GenerateSubProjectId();
            return View();
        }

        // POST: SubProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubProjectId,SubProjectName,ProjectId,Remark,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] SubProject subProject)
        {
            subProject.Id = Guid.NewGuid().ToString();
            subProject.CreatorUserId = "";
            subProject.CreationTime = DateTime.Today;
            subProject.IsDeleted = false;

            if (ModelState.IsValid)
            {
                _context.Add(subProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", subProject.ProjectId);
            ViewData["ProjectMainId"] = new SelectList(_context.Projects.OrderBy(c=>c.ProjectName), "Id", "ProjectName",subProject.ProjectId);
            ViewData["SubProjectId"] = GenerateSubProjectId();

            return View(subProject);
        }

        // GET: SubProjects/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProjects.FindAsync(id);
            if (subProject == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName", subProject.ProjectId);
            return View(subProject);
        }

        // POST: SubProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SubProjectId,SubProjectName,ProjectId,Remark,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] SubProject subProject)
        {
            if (id != subProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubProjectExists(subProject.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", subProject.ProjectId);
            return View(subProject);
        }

        // GET: SubProjects/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProjects
                .Include(s => s.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProject == null)
            {
                return NotFound();
            }

            return View(subProject);
        }

        // POST: SubProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var subProject = await _context.SubProjects.FindAsync(id);
            _context.SubProjects.Remove(subProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubProjectExists(string id)
        {
            return _context.SubProjects.Any(e => e.Id == id);
        }

        private string GenerateSubProjectId()
        {
            int count = _context.SubProjects.Count();
            var result = "YEN_SP-" + (count + 1);


            return result;
        }
    }
}
