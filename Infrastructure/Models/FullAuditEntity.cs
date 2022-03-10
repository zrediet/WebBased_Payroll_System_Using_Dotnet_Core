using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class FullAuditEntity
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "Creation Time")]
        public DateTime? CreationTime { get; set; }
        [Display(Name = "Creator User Id")]
        public string CreatorUserId { get; set; }

        [Display(Name = "Last Modification Time")]
        public DateTime? LastModificationTime { get; set; }
        [Display(Name = "Last Modifier User Id")]
        public string LastModifierUserId { get; set; }
        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }
        [Display(Name = "Deletion Time")]
        public DateTime? DeletionTime { get; set; }
        [Display(Name = "Deleter User Id")]
        public string DeleterUserId { get; set; }
  


    }
}
