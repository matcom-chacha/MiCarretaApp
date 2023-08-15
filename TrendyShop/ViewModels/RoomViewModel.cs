using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class RoomViewModel
    {
        public List<List<RoomProduct>> Rooms { get; set; }
        public List<StorageRow>[] Products { get; set; }
        public GastronomicProduct GastronomicProduct { get; set; }
        public float[] AmountToAdd1 { get; set; }
        public float[] AmountToAdd2 { get; set; }
        public float[] AmountToAdd3 { get; set; }
        public List<NamedRoom> RoomList { get; set; }
    }
}
