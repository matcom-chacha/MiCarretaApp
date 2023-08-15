using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class AddExpProductViewModel
    {
        public ExpendableProduct ExpendableProduct{ get; set; }

        [Display(Name ="Cantidad")]
        public float Quantity { get; set; }
    }
}
