using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class DailyClosing
    {
        [Key, Column(Order = 0)]
        public DateTime Date { get; set; }

        [ForeignKey("Date")]
        public DateOfClosing DateOfClosing { get; set; }

        [Key, Column(Order = 1)]
        public float Cost { get; set; }

        [Key, Column(Order = 2)]
        public string Name { get; set; }

        [ForeignKey("Cost,Name")]
        public GastronomicProduct Product { get; set; }

        public float AmountInStorage { get; set; }
        public float Room1 { get; set; }
        public float Room2 { get; set; }
        public float Room3 { get; set; }
        public float TotalAmount { get; set; }
        public float ConsuptionSincePreviuosClosing { get; set; }
        public float Replenished { get; set; }
    }
}
