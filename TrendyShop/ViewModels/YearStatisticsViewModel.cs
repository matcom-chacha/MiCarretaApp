using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.ViewModels
{
    public class YearStatisticsViewModel
    {

        public int Year { get; set; }

        public string[] Months { get; set; }
        public string[] SalaryPerMoth { get; set; }

        public string AnnualSalary { get; set; }

        public int[] LodgingsPerMonth { get; set; }

        public int AnnualLodgings { get; set; }

        public int[] DoubleLodgingsPerMonth { get; set; }
        public int AnnualDoubleLodgings { get; set; }

        public string[] DailyLodgingsPerMonth { get; set; }

        public string[] ConsumptionIncomePerMonth { get; set; }

        public string AnnualConsumptionIncome { get; set; }

        public string[] ConsumptionProfitPerMonth { get; set; }

        public string AnnualConsumptionProfit { get; set; }

        public string[] HouseExpensesPerMoth { get; set; }

        public string AnnualHouseExpenses { get; set; }

        public string[] DamageIncomePerMoth { get; set; }

        public string AnnualDamageIncome { get; set; }

        public string[] RentIncomePerMoth { get; set; }

        public string AnnualRentIncome { get; set; }

        public string[] GrossIncomePerMonth { get; set; }

        public string AnnualGrossIncome { get; set; }

    }
}
