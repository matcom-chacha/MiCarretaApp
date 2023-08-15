using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class DateOfClosing
    {
        [Key]
        public DateTime Date { get; set; }

        public DateTime PreviousClousingDate { get; set; }

        [ForeignKey("PreviousClosingDate")]
        public DateOfClosing PreviousClosing { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public bool Approved { get; set; }
    }
}
