using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class Employee
    {
        [Key]
        [Display(Name = "Carnet de identidad")]
        [Required(ErrorMessage ="Ingrese CI")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string EmployeeId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Ingrese nombre")]
        public string Name { get; set; }

        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        public DateTime LastPayment { get; set; }

        public DateTime LastRestart { get; set; }

        public float PendingPayment { get; set; }

        public float TotalSalary { get; set; }

        public float DefaultSalary { get; set; }

    }
}
