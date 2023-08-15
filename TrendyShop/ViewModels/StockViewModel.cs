using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class StockViewModel
    {
        public List<Tuple<string, float,string,float>> Products { get; set; }
        public List<Lodging> Lodgins { get; set; }
        public float IncomePerRent { get; set; }
        public float IncomePerDamage { get; set; }
        public float ProductsIncome { get; set; }
        public float TotalIncome { get { return IncomePerRent + IncomePerDamage + ProductsIncome + MoneyOperationsIncome; } }
        public float LastFund { get; set;}
        public DateTime LastStockDate { get; set; }
        public float NewFund { get; set; }
        public float MoneyOperationsIncome { get; set; }
        public float TotalCash { get { return TotalIncome + LastFund; } }
        public bool IsPreviousStock { get; set; }
    }
}
