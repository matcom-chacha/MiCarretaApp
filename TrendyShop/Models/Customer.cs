using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Carnet de identidad ")]
        [Required(ErrorMessage ="Ingrese CI")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CustomerId { get; set; }

        [Required(ErrorMessage ="Ingrese nombre")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Teléfono")]
        //[Phone]
        public string Phone { get; set; }

        [Display(Name = "Bloquear Cliente")]
        public bool IsBlocked { get; set; }

        public DateTime LastBlockedDate { get; set; }

        public DateTime LastEntrance { get; set; }

    }
}
