using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employee = await _context.Employees.Include(d => d.Department)
                .Where(a=>a.IsDeleted == false)
                .ToListAsync();

            return View(employee);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employee = await _context.Employees
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var employee = await _context.Employees.Include(d => d.Department)
                .FirstOrDefaultAsync(m=>m.Id==id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.EmpId = GenerateEmployeeId();
            //ViewData["ProjectId"] = new SelectList(_context.Projects.OrderBy(c=>c.ProjectName), "Id", "ProjectName");
            //ViewData["SubProjectId"] = new SelectList(_context.SubProjects.OrderBy(c=>c.SubProjectName), "Id", "SubProjectName");
            //ViewData["DivisionId"] = new SelectList(_context.Divisions.OrderBy(c=>c.DivisionName), "Id", "DivisionName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeRegistrationViewModel registration) //[Bind("EmployeeId,FirstName,MiddleName,LastName,Gender,PhoneNo,EmployeeStatus,EmployeementType,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] Employee employee
        {
            var empSystemID = registration.Employee.Id = Guid.NewGuid().ToString();
            registration.Employee.CreationTime = DateTime.Today;
            registration.Employee.CreatorUserId = "";
            registration.Employee.IsDeleted = false;
            registration.Employee.EmployeeId = Request.Form["Employee.EmployeeId"];
            registration.Employee.Salary = Convert.ToSingle(Request.Form["EmployeeSalary.BasicSalary"].ToString());
            //Employee Salary
            registration.EmployeeSalary.Id = Guid.NewGuid().ToString();
            
            registration.EmployeeSalary.EmployeeId = empSystemID;
            registration.EmployeeSalary.CreationTime = DateTime.Today;
            registration.EmployeeSalary.IsDeleted = false;
            registration.EmployeeSalary.SalaryStatus = SalaryStatus.Active;
             

            if (ModelState.IsValid)
            {
                _context.Add(registration.Employee);
                _context.Add(registration.EmployeeSalary);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.EmpId = GenerateEmployeeId();
            //ViewData["ProjectId"] = new SelectList(_context.Projects.OrderBy(c=>c.ProjectName), "Id", "ProjectName");
            //ViewData["SubProjectId"] = new SelectList(_context.SubProjects.OrderBy(c=>c.SubProjectName), "Id", "SubProjectName");
            //ViewData["DivisionId"] = new SelectList(_context.Divisions.OrderBy(c=>c.DivisionName), "Id", "DivisionName");

            return View(registration);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            //ViewData["ProjectId"] = new SelectList(_context.Projects.OrderBy(c=>c.ProjectName), "Id", "ProjectName");
            //ViewData["SubProjectId"] = new SelectList(_context.SubProjects.OrderBy(c=>c.SubProjectName), "Id", "SubProjectName");
            ViewData["DivisionId"] = new SelectList(_context.Departments.OrderBy(c=>c.DepartmentName), "Id", "DepartmentName");

            if (id == null)
            {
                return NotFound();
            }

            //var employee = await _context.Employees.FindAsync(id);
            var employee = await _context.Employees.Include(d => d.Department)
                .FirstOrDefaultAsync(m=>m.Id==id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,EmployeeRegistrationViewModel empUpdate) //string id, [Bind("EmployeeId,FirstName,MiddleName,LastName,Gender,PhoneNo,EmployeeStatus,EmployeementType,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] Employee employee
        {
            if (id != empUpdate.Employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(empUpdate.Employee.Id))
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
            return View(empUpdate);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            
            employee.IsDeleted = true;
            employee.LastModificationTime = DateTime.Now;
            employee.LastModifierUserId = "";

            _context.Employees.Update(employee);
            //_context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        private string GenerateEmployeeId()
        {
            int count = _context.Employees.Count();
            var result = "EMP-" + (count + 1);


            return result;
        }
    }
}
