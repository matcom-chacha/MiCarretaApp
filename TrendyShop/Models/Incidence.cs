using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class Incidence
    {
        //[Key]
        //public int IncidenceId { get; set; }

        //[Required]
        //public float Price { get; set; }

        //[Required]
        //public string Description { get; set; }

        [Key]
        public int IncidenceId { get; set; }


        [Display(Name = "Asunto")]
        [Required(ErrorMessage = "Especifique tipo de incidencia")]
        public string Subject { get; set; }


        [Display(Name = "Precio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe tener un precio")]
        [Required(ErrorMessage = "Especifique el precio")]
        public float Price { get; set; }


        [Display(Name = "Descripción")]
        public string Description { get; set; }



    }

}
