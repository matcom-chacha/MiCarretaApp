using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class EditExpProdStorageViewModel
    {
        public ExpendableProductStorage ExpProduct{ get; set; }

        [Display(Name = "Cantidad a añadir")]
        public float AmountToAdd { get; set; }
    }
}
