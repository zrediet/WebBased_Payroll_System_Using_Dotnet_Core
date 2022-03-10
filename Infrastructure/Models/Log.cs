using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Infrastructure.Models
{
    public class Log : FullAuditEntity 
    {
        [Display(Name = "Log Action")]
        public LogAction LogAction { get; set; }
        public string TableName { get; set; }
        public string RowId { get; set; }
        
    }

    
    public enum LogAction
    {
        [Display(Name = "Create")]
        Create,
        [Display(Name = "Update")]
        Update,
        [Display(Name = "Delete")]
        Delete
    }
}
