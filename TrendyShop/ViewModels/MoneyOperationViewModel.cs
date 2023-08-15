using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class MoneyOperationViewModel
    {
        public float TotalIncome { get; set; }
        public List<MoneyOperation> MoneyOperations{ get; set; }
        public List<FundMoneyOperation> FundMoneyOperations { get; set; }
        public float Fund { get; set; }
        public bool Approved { get; set; }
    }
}
