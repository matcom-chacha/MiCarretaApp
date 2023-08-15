using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class PackageViewModel
    {
        public IEnumerable<Package> PackGastronomics { get; set; }
        public IEnumerable<GastronomicProduct> Products { get; set; }
        public Dictionary<GastronomicProduct,string> ProductString { get; set; }
        public Package PackToEdit { get; set; }

    }
}
