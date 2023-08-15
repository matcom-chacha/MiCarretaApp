using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrendyShop.Models
{
    public class FundMoneyOperation
    {
        public int FundMoneyOperationId { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public float Amount { get; set; }

        public bool IsExtraction { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Asunto")]
        public string Subject { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
