using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class ExtractGProductViewModel
    {
        public StorageRow ProductInfo { get; set; }

        [Display(Name = "Cantidad a extraer")]
        public float AmountToExtract { get; set; }

        public float ProductCost { get; set; }

        public string ProductName { get; set; }

    }
}
