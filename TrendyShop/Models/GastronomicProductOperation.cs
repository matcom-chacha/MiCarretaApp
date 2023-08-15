using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class GastronomicProductOperation
    {
        [Key, Column(Order = 0)]
        public float Cost { get; set; }//ProductCost

        [Key, Column(Order = 1)]
        public string Name { get; set; }//ProductName

        [ForeignKey("Cost,Name")]
        public GastronomicProduct GastronomicProduct { get; set; }

        [Key,Column(Order = 2)]
        public DateTime Date { get; set; }

        public float AmountAvailable { get; set; }
        
        public float AmountInrooms { get; set; }

        public float OperationAmount { get; set; }

        public int POTypeId { get; set; }

        [ForeignKey("POTypeId")]
        public ProductOperationType POType { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
