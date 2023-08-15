using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class ProductOperationType
    {
        [Key]
        public int POTypeId { get; set; }

        public string Name { get; set; }
    }
}
