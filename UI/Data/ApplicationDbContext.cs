using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Models;
namespace UI.Data
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
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<PayrollSetting> PayrollSettings { get; set; }
        public DbSet<PayrollSheet> PayrollSheets { get; set; }
        public DbSet<IncomeTaxSetting> IncomeTaxSetting { get; set; }
        public DbSet<Termination> Terminations { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }

        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Overtime>().Property(p=>p.HolyDayOT).HasPrecision(18,2);
            builder.Entity<Overtime>().Property(p=>p.NormalOT).HasPrecision(18,2);
            builder.Entity<Overtime>().Property(p=>p.NormalOT2).HasPrecision(18,2);
            builder.Entity<Overtime>().Property(p=>p.WeekendOT).HasPrecision(18,2);
            base.OnModelCreating(builder);
        }

    }


}
