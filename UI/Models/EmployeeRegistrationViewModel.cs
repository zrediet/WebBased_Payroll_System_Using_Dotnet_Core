using Infrastructure.Models;

namespace Payroll.Models
{
    public class EmployeeRegistrationViewModel
    {
        public Employee Employee { get; set; }
        public EmployeeSalary EmployeeSalary { get; set; }
        //public EmployeeFamily EmployeeFamily { get; set; }
    }
}
