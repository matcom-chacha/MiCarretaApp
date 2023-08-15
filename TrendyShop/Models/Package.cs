using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class Package
    {        
        [Key, Column(Order = 0)]
        public float Cost { get; set; }

        [Key, Column(Order = 1)]
        public string Name { get; set; }

        [ForeignKey("Cost,Name")]
        public GastronomicProduct GastronomicProduct { get; set; }

        [Required(ErrorMessage ="Introduzca la cantidad")]
        public int Quantity { get; set; }
    }
}
