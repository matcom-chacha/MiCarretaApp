using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class EditStorageRowViewModel
    {
        public StorageRow StorageRow { get; set; }

        public float Cost { get; set; }

        public string Name { get; set; }

        [Display(Name = "Cantidad a añadir")]
        public int AmountToAdd { get; set; }
    }
}
