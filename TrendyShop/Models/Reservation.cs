using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace TrendyShop.Models
{
    public class Reservation
    {
        [Key, Column(Order = 0)]
        [Display(Name = "Número de Habitación")]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "Fecha de inicio")]
        [Required(ErrorMessage = "Ingrese fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha final")]
        [Required(ErrorMessage = "Ingrese fecha final")]
        public DateTime FinalDate { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Ingrese nombre")]
        public string Customer { get; set; }

        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [Display(Name = "Decoración Romántica")]
        public bool Romantic { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsActive { get; set; }

    }
}
