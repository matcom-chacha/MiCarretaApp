using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class RepositionRecord
    {
        //[Key, Column(Order = 0)]
        //public DateTime RepositionDate { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Especifique la fecha del reporte")]
        public DateTime Date { get; set; }

        [ForeignKey("Date")]
        public DateOfClosing DateOfClosing { get; set; }

        [Key, Column(Order = 2)]
        [Required(ErrorMessage = "Especifique el costo de adquisición del producto")]
        [Display(Name = "Costo de adquisición del producto")]
        public float Cost { get; set; }

        [Key, Column(Order = 3)]
        [Required(ErrorMessage = "Especifique el nombre del producto")]
        public string Name { get; set; }

        [ForeignKey("Cost,Name")]
        public GastronomicProduct Product { get; set; }

        //[ForeignKey("Date,Cost,Name")]
        //public MissingRecord MissingRecord { get; set; }

        [Required(ErrorMessage = "Especifique el tipo de reposición")]
        [Display(Name = "Reposición por pago")]
        public bool Paid { get; set; }

        [Required(ErrorMessage = "Especifique la cantidad a reponer")]
        [Display(Name = "Cantidad")]
        public float Amount { get; set; }
    }
}
