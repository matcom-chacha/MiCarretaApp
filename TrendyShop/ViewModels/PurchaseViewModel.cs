using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class PurchaseViewModel
    {
        public IList<RoomProduct> RoomProducts { get; set; }

        public float[] AmountToMove { get; set; }

        public int RoomId { get; set; }

        public DateTime InitialDate { get; set; }
    }
}
