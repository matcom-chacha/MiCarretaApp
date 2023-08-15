using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class HouseExpense
    {
        public int Id { get; set; }
        
        [Required]
        public float Amount { get; set; }
        
        public DateTime Date { get; set; }

        public int ExpenseTypeId { get; set; }

        [ForeignKey("ExpenseTypeId")]
        public ExpenseType ExpenseType { get; set; }
    }
}
