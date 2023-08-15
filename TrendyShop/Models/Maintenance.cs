using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class Maintenance
    {
        [Key]
        public int MaintenanceId { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage ="Especifique el tipo de mantenimiento")]
        public string Description { get; set; }

        [Display(Name = "Costo")]
        public float Cost { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
    }
}
