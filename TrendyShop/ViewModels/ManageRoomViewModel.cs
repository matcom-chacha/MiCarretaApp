using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class ManageRoomViewModel
    {
        public int RoomId { get; set; }
        public IEnumerable<StorageRow> Storage { get; set; }
        public IEnumerable<RoomProduct> RoomProducts { get; set; }
        public List<Package> Package { get; set; }
        public float[] AmountToAdd { get; set; }
        public float[] RealAmountToAdd { get; set; }
    }
}
