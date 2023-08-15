using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class YearStatistics
    {
        public string Month { get; set; }

        public int Lodgings { get; set; }
        public int DoubleLodgings { get; set; }

        public string DailyLodgings { get; set; }

        public string RentIncome { get; set; }

        public string ConsumptionIncome { get; set; }

        public string ConsumptionProfit { get; set; }

        public string DamageIncome { get; set; }

        public string GrossIncome { get; set; }

        public string Salary { get; set; }

        public string HouseExpenses { get; set; }
    }
}
