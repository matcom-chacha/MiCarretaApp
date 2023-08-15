using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.ViewModels
{
    public class SettingsViewModel
    {
        [Display(Name = "Pago del Empleado por Hospedaje")]
        public float EmployeeSalary { get; set; }

        [Display(Name = "Costo por Renta")]
        public float RentCost { get; set; }

        [Display(Name = "Precio de Decoración Romántica")]
        public float RomanticTax { get; set; }

        [Display(Name = "Precio de Reservación Doble")]
        public float LateHoursTax { get; set; }

        [Display(Name = "Precio Extra por 3er Acompañante")]
        public float CompanionTax { get; set; }
    }
}
