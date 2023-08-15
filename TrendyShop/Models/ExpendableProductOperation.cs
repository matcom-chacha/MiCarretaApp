using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrendyShop.Models;

namespace TrendyShop.Models
{
    public class ExpendableProductOperation
    {
        [Key, Column(Order = 0)]
        public float? Cost { get; set; }

        [Key, Column(Order = 1)]
        public string Name { get; set; }

        [ForeignKey("Cost,Name")]
        public ExpendableProduct ExpendableProduct { get; set; }

        [Key, Column(Order = 2)]
        public DateTime Date { get; set; }

        public float AlreadyInStorageAmount { get; set; }

        public float OperationAmount { get; set; }

        public int POTypeId{ get; set; }

        [ForeignKey("POTypeId")]
        public ProductOperationType POType { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}

