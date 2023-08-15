using System.Collections.Generic;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class ExpendableProductDetailsViewModel
    {
        public List<ExpendableProductOperation> Deposits { get; set; }
        public List<ExpendableProductOperation> Extractions { get; set; }
        public string ProductName { get; set; }
    }
}
