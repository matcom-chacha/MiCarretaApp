using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace TrendyShop.Models
{
    public class Lodging
    {
        [Key, Column(Order = 0)]
        public int RoomId { get; set; }

        public int LodgingNumber { get; set; }

        [Key, Column(Order = 1)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Especifique hora final")]
        [Display(Name = "Fin de reservación")]
        public DateTime FinalDate { get; set; }

        [Required(ErrorMessage = "Especifique hora de inicio")]
        [Display(Name = "Inicio del Hospedaje")]
        public DateTime ActualIDate { get; set; }

        [Required(ErrorMessage = "Especifique hora de cierre")]
        [Display(Name = "Fin del Hospedaje")]
        public DateTime ActualFDate { get; set; }

        public bool Active { get; set; }

        [Display(Name ="Empleado a cargo del hospedaje")]
        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")/*, Column(Order = 1)*/]//required
        public Employee Employee { get; set; }

        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Display(Name = "Número de acompañantes")]
        public int Companion { get; set; }

        public bool Romantic { get; set; }

        [Display(Name = "Cargo extra")]
        public float? ExtraCharge { get; set; }

        //verificar si consumption es necesario
        [Display(Name = "Costo por Renta")]
        [Required(ErrorMessage ="Especifique el costo de la renta")]
        public float? RentCost { get; set; }
        
        public float? Consumption { get; set; }
        public bool? IsDouble { get; set; }

        //[Required(ErrorMessage = "Especifique el precio total")]
        [NotMapped]
        [Display(Name = "Precio Total")]
        public float? TotalPrice { get { return Consumption + RentCost + ExtraCharge; }}

        [Display(Name = "Prepagado")]
        public float Prepaid { get; set; }

    }
}
