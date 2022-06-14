using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Models;

namespace Payroll.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<DeductionPaymentHistory> DeductionPaymentHistories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<EmployeeFamily> EmployeeFamilies { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<PayrollSetting> PayrollSettings { get; set; }
        public DbSet<PayrollSheet> PayrollSheets { get; set; }
        //public DbSet<Project> Projects { get; set; }
        //public DbSet<SubProject> SubProjects { get; set; }
        public DbSet<IncomeTaxSetting> IncomeTaxSetting { get; set; }
        public DbSet<Termination> Terminations { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }

        //public DbSet<AllowancePackage> AllowancePackages { get; set; }
        //public DbSet<AllowancePackageDetail> AllowancePackageDetails { get; set; }


    }
}
