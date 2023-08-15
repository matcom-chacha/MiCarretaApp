using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class StockTaking
    {
        public int StockTakingId { get; set; }
        public DateTime Date { get; set; }

        //Add validation ( fund >= 0)
        public float Fund { get; set; }
    }
}
