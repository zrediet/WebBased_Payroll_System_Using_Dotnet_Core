using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Employee : FullAuditEntity
    {
        [MaxLength(10)]
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string MiddleName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone No.")]
        public string PhoneNo { get; set; }
        [Display(Name = "Employment Status")]public EmployeeStatus EmployeeStatus { get; set; }

        [Display(Name = "Employment Type")]public EmploymentType EmploymentType { get; set; }

        [Display(Name = "Payment Period")]public PaymentPeriod PaymentPeriod { get; set; }

        public virtual Division Division { get; set; }
        public string DivisionId { get; set; }

        public virtual Project Project { get; set; }
        public string ProjectId { get; set; }

        public virtual SubProject SubProject { get; set; }
        public string SubProjectId { get; set; }

        [Display(Name = "Is Pension?")]
        public bool IsPension { get; set; }

        [Display(Name="City")] public string City { get; set; }
        [Display(Name="Sub-City")] public string SubCity { get; set; }
        [Display(Name="Region")] public string Region { get; set; }

        [Required]
        [Display(Name = "Salary")]
        public float Salary { get; set; }

        [Required]
        [Display(Name = "Bank Account")]
        public string BankAccount { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        
    }

    public enum EmployeeStatus
    {
        [Display(Name="Active")]
        Active,
        [Display(Name = "In-Active")]
        Inactive,
    }

    public enum Gender
    {
        [Display(Name="Male")]
        Male,
        [Display(Name="Female")]
        Female
    }

    public enum EmploymentType
    {
        [Display(Name="Permanent")]
        Permanent,
        [Display(Name = "Contract")]
        Contract
    }

    public enum PaymentPeriod
    {
        [Display(Name = "Every 15 Days")] EveryFifteenDays,
        [Display(Name = "Monthly")] Monthly
    }
}
