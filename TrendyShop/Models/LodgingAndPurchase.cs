using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class LodgingAndPurchase
    {
        public Lodging Lodging { get; set; }
        public IGrouping<Lodging,PurchasePerLodging> Purchase { get; set; }

        //Para ordenar en Lodgings/Index de mayor a menor
        public int Compare([AllowNull] LodgingAndPurchase y)
        {
            if (Lodging == null && y.Lodging == null)
                return Purchase.Key.LodgingNumber.CompareTo(y.Purchase.Key.LodgingNumber) * -1;
            else if (Lodging == null)
                return Purchase.Key.LodgingNumber.CompareTo(y.Lodging.LodgingNumber) * -1;
            else if (y.Lodging == null)
                return Lodging.LodgingNumber.CompareTo(y.Purchase.Key.LodgingNumber) * -1;
            else 
                return Lodging.LodgingNumber.CompareTo(y.Lodging.LodgingNumber) * -1;
        }
    }
}
