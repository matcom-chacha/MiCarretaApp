using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class StorageRow
    {
        [Key, Column(Order = 0)]
        public float Cost { get; set; }//ProductCost
        
        [Key, Column(Order = 1)]
        public string Name { get; set; }//ProductName

        [ForeignKey("Cost,Name")]
        public GastronomicProduct GastronomicProduct { get; set; }

        public float AmountAvailable { get; set; }

        public float AmountInRooms { get; set; }

        [NotMapped]
        public float TotalAmount { get { return AmountAvailable + AmountInRooms; }}


        //
        //public DateTime LastInsertionDate { get; set; }
        //public float LastInsertionAmount { get; set; }


    }
}
