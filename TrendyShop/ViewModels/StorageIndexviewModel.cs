using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class StorageIndexviewModel
    {
        public List<ExpendableProductStorage>  ExpProducts { get; set; }
        public List<StorageRow> GastronomicProducts { get; set; }
        public List<NamedRoom> Rooms { get; set; }
        //public int SourceId { get; set; }
        //public int DestinationId { get; set; }
        public float ProductCost { get; set; }
        public string ProductName { get; set; }
        public int AmountToAdd { get; set; }
    }
}
