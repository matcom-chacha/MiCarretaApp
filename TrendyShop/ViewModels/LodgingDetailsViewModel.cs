using System.Collections.Generic;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class LodgingDetailsViewModel
    {
        public Lodging Lodging { get; set; }
        public List<PurchasePerLodging> Purchase { get; set; }
        public List<LodgingIncidence> Incidences { get; set; }
        
    }
}
