using System;
using System.Collections.Generic;
using System.Linq;
using TrendyShop.Models;
using System.Threading.Tasks;

namespace TrendyShop.ViewModels
{
    public class Informe
    {
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public ExpendableProduct ExpProduct { get; set; }
        public GastronomicProduct GastronomicProduct { get; set; }
        public Incidence Incidence { get; set; }
        public Lodging Lodging { get; set; }
        public Maintenance Maintenance { get; set; }
        public Reservation Reservation { get; set; }
        public Room Room { get; set; }
        public RoomProduct RoomProduct { get; set; }
        public StorageRow Storage { get; set; }
        public PurchasePerLodging PurchasePerLodging { get; set; }
    }
}
