using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrendyShop.Models
{
    public class GastronomicProduct
    {
        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "Especifique el costo de adquisición del producto")]
        [Display(Name = "Costo")]
        public float Cost { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Especifique el nombre del producto")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Especifique el precio de oferta del producto")]
        [Display(Name = "Precio")]
        public float Price { get; set; }
    }
}
