using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class MoneyOperation
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Cantidad")]
        public float Amount { get; set; }
        
        public bool IsExtraction { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Asunto")]
        public string Subject { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public int MOTypeId { get; set; }

        [Required]
        [ForeignKey("MOTypeId")]
        public MOType MOType { get; set; }
    }
}
