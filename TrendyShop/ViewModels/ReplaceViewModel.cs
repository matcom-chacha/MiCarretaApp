using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class ReplaceViewModel
    {
        //public List<StorageRow> Storage { get; set; }
        public List<GastronomicProduct> ProductsInSource { get; set; }
        //public List<RoomProduct> Source { get; set; }
        public List<float> Source { get; set; }
        //public List<RoomProduct> Destination { get; set; }
        public float[] Destination { get; set; }
        public float[] AmountToMove { get; set; }

        [Display(Name = "Fuente")]
        public int SourceId { get; set; }

        [Display(Name = "Destino")]
        public int DestinationId { get; set; }

        public string SourceName { get; set; }
        public string DestinationName { get; set; }
    }
}
