using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;


namespace TrendyShop.ViewModels
{
    public class GastronomicProductDetailsViewModel
    {
        public List<GastronomicProductOperation> Deposits { get; set; }
        public List<GastronomicProductOperation> Extractions { get; set; }
        public string ProductName { get; set; }
    }
}
