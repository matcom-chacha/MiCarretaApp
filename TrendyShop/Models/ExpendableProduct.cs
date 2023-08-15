using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class ExpendableProduct
    {
        [Key]
        [Display(Name="Costo")]
        public float? Cost { get; set; }

        [Key]
        [Display(Name = "Nombre")]
        [Required(ErrorMessage ="Especifique el nombre del producto")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Especifique la unidad de medida")]
        [Display(Name="Unidad de Medida")]
        public string UnitOfMeasurement { get; set; }

    }
}
