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
    public class EmployeeSalariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeSalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeSalaries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeeSalaries
                .Include(e => e.Employee)
                .Where(a=>a.SalaryStatus == SalaryStatus.Active);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeeSalaries/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeSalary == null)
            {
                return NotFound();
            }

            return View(employeeSalary);
        }

        // GET: EmployeeSalaries/Create
        public IActionResult Create()
        {
            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");

            var empList = _context.Employees.Where(c => c.IsDeleted == false)
                .Select(s => new
                {
                    Id = s.Id,
                    FullName = s.FirstName + " " + s.MiddleName + " " + s.LastName +" ("+s.EmployeeId+")",
                }).ToList();

            ViewData["EmployeeId"] = new SelectList(empList, "Id", "FullName");
            return View();
        }

        // POST: EmployeeSalaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BasicSalary,TransportAllowance,HomeAllowance,PositionAllowance,OtherAllowance,EmployeeId,SalaryStatus,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] EmployeeSalary employeeSalary)
        {
            employeeSalary.Id = Guid.NewGuid().ToString();
            employeeSalary.CreationTime = DateTime.Today;
            employeeSalary.CreatorUserId = "";

            var result = _context.Employees.Where(c=>c.Id == employeeSalary.EmployeeId).Select(a=>a.Id);

            var emp = _context.Employees.FindAsync(result.FirstOrDefault());
            
            emp.Result.Salary = employeeSalary.BasicSalary;
            emp.Result.LastModificationTime= DateTime.Now;
            emp.Result.LastModifierUserId = "";


            var check = _context.EmployeeSalaries.Where(c =>
                c.EmployeeId == employeeSalary.EmployeeId && c.SalaryStatus == SalaryStatus.Active);
            if (check.Any())
            {
                ModelState.AddModelError("","Duplicate Salary Exist. Please De-Activate First.");
            }


            if (ModelState.IsValid)
            {
                _context.Add(employeeSalary);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeSalary.Employee);

            var empList = _context.Employees.Where(c => c.IsDeleted == false)
                .Select(s => new
                {
                    Id = s.Id,
                    FullName = s.FirstName + " " + s.MiddleName + " " + s.LastName +" ("+s.EmployeeId+")",
                }).ToList();

            ViewData["EmployeeId"] = new SelectList(empList, "Id", "FullName", employeeSalary.Employee);
            return View(employeeSalary);
        }



        // GET: EmployeeSalaries/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries.FindAsync(id);
            if (employeeSalary == null)
            {
                return NotFound();
            }

            var empList = _context.Employees.Where(c => c.IsDeleted == false)
                .Select(s => new
                {
                    Id = s.Id,
                    FullName = s.FirstName + " " + s.MiddleName + " " + s.LastName +" ("+s.EmployeeId+")",
                }).ToList();

            ViewData["EmployeeId"] = new SelectList(empList, "Id", "FullName", employeeSalary.Employee);


            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeSalary.Employee);

            return View(employeeSalary);
        }

        // POST: EmployeeSalaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BasicSalary,TransportAllowance,HomeAllowance,OtherAllowance,EmployeeId,SalaryStatus,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] EmployeeSalary employeeSalary)
        {
            if (id != employeeSalary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //employeeSalary.IsDeleted = true;
                    employeeSalary.LastModificationTime = DateTime.Today;
                    employeeSalary.LastModifierUserId = "";
                    employeeSalary.SalaryStatus = SalaryStatus.InActive;
                    employeeSalary.IsDeleted = true;

                    _context.Update(employeeSalary);
                    await _context.SaveChangesAsync();

                     
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeSalaryExists(employeeSalary.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeSalary.Employee);
            return View(employeeSalary);
        }
         

        public async Task<IActionResult> NewSalary(string id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries.FindAsync(id);
            if (employeeSalary == null)
            {
                return NotFound();
            }

            var empList = _context.Employees.Where(c => c.IsDeleted == false)
                .Select(s => new
                {
                    Id = s.Id,
                    FullName = s.FirstName + " " + s.MiddleName + " " + s.LastName +" ("+s.EmployeeId+")",
                }).ToList();

            ViewData["EmployeeId"] = new SelectList(empList, "Id", "FullName", employeeSalary.Employee);


            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employeeSalary.Employee);

            return View(employeeSalary);
        }
         
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeSalary == null)
            {
                return NotFound();
            }

            return View(employeeSalary);
        }

        // POST: EmployeeSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employeeSalary = await _context.EmployeeSalaries.FindAsync(id);
            _context.EmployeeSalaries.Remove(employeeSalary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeSalaryExists(string id)
        {
            return _context.EmployeeSalaries.Any(e => e.Id == id);
        }
    }
}
