using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class ExpendableProductStorage
    {

        [Key]
        public float? Cost { get; set; }

        [Key]
        public string Name { get; set; }

        [ForeignKey("Cost,Name")]
        public ExpendableProduct ExpendableProduct { get; set; }

        public DateTime LastInsertionDate { get; set; }

        public float? LastInsertedQuantity { get; set; }

        public float? TotalQuantity { get; set; }

    }
}
