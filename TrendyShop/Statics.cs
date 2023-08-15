using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;
using TrendyShop.Data;
using Microsoft.EntityFrameworkCore;

namespace TrendyShop
{
    public static class Statics
    {
        #region Date validation
        private static DateTime ValidateInitialDate(DateTime initialDate)
        {
            if (initialDate == default)
                return DateTime.MinValue;

            return initialDate;
        }
        private static DateTime ValidateFinalDate(DateTime finalDate)
        {
            if (finalDate == default)
                return DateTime.Now;

            return finalDate;
        }
        #endregion

        #region Stock and MoneyOperations
        public static StockTaking GetLastStock(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            var stock = context.Stocks.Where(s => s.Date >= initialDate && s.Date < finalDate).OrderByDescending(s => s.Date);
            if (stock.Count() == 0)
                return new StockTaking { Date = DateTime.MinValue, Fund = 0 };

            return stock.First();
        }
        public static List<Lodging> GetLodgings(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            return context.Lodgings.Include(l => l.Customer).Where(l => l.Date >= initialDate && l.Date <= finalDate).ToList();
        }
        
        //Returns a  list with the product's name, cost and the amount of purchase
        public static dynamic GetPurchasedProducts(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            var purchasedProducts = context.PurchasePerLodgings
                                      .Include(p => p.GastronomicProduct)
                                      .AsEnumerable()
                                      .Where(p => p.Date >= initialDate && p.Date <= finalDate)
                                      .GroupBy(l => new { l.Cost, l.Name })
                                      .Select(l => new { ProductName = l.Key.Name, ProductCost = l.Key.Cost, ProductAmount = l.Sum(p => p.Amount) })
                                      .ToList();

            return purchasedProducts;
        }
        
        public static List<Tuple<string, float, string, float>> GetIncomePerProduct(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            var purchasedProducts = GetPurchasedProducts(context, initialDate, finalDate);

            var incomePerProduct = new List<Tuple<string, float, string, float>>();
            foreach (var item in purchasedProducts)
            {
                var gp = context.GastronomicProducts.Find(item.ProductCost, item.ProductName);
                string formula = $"{item.ProductAmount}x{gp.Price}";
                float income = item.ProductAmount * gp.Price;
                incomePerProduct.Add(new Tuple<string, float, string, float>(item.ProductName, item.ProductAmount, formula, income));
            }

            return incomePerProduct;
        }
        public static float GetPurchasedProductsIncome(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            var purchasedProducts = GetPurchasedProducts(context, initialDate, finalDate);
            float totalIncome = 0;
            foreach (var item in purchasedProducts)
            {
                var gp = context.GastronomicProducts.Find(item.ProductCost, item.ProductName);
                totalIncome += item.ProductAmount * gp.Price;
            }
            return totalIncome;
        }
        public static float GetTotalMoneyDeposits(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            return context.MoneyOperations.Where(m => m.Date >= initialDate && m.Date <= finalDate && m.IsExtraction == false).Select(m => m.Amount).Sum();
        }
        public static float GetTotalMoneyExtractions(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            return context.MoneyOperations.Where(m => m.Date >= initialDate && m.Date <= finalDate && m.IsExtraction).Select(m => m.Amount).Sum();
        }
        public static float GetMoneyOperationsIncome(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            var totalMoneyDeposits = GetTotalMoneyDeposits(context, initialDate, finalDate);
            var totalMoneyExtractions = GetTotalMoneyExtractions(context, initialDate, finalDate);

            return totalMoneyDeposits - totalMoneyExtractions;
        }
        public static List<MoneyOperation> GetMoneyOperations(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            return context.MoneyOperations.Include(m => m.Employee).Where(m => m.Date >= initialDate && m.Date <= finalDate).ToList();
        }
        public static float GetIncomePerRent(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            var lodgings = GetLodgings(context, initialDate, finalDate);

            return (float)lodgings.Sum(l => l.RentCost);
        }
        public static float GetIncomePerDamage(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            var lodgings = GetLodgings(context, initialDate, finalDate);

            return (float)lodgings.Sum(l => l.ExtraCharge);
        }
        public static float GetHouseExpenses(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            return context.HouseExpenses.Where(h => h.Date >= initialDate && h.Date <= finalDate).Sum(h => h.Amount);
        }
        
        //return a list with the lodging's purchased products, the amount bought and the income
        public static dynamic GetLodgingProducts(DataContext context, int roomId, DateTime date)
        {
            return context.PurchasePerLodgings
                .Include(p => p.GastronomicProduct)
                .Where(p => p.Date == date && p.RoomId == roomId)
                .Select(p => new { p.GastronomicProduct, p.Amount, Income = p.GastronomicProduct.Price * p.Amount })
                .ToList();
        }
        #endregion

        #region Employee
        public static float GetEmployeeSalary(DataContext context, DateTime initialDate, DateTime finalDate, string employeeId)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);


            float salary = context.SystemDefaultPrices.Single(s => s.Name == "Salary").Amount;
            var lodgings = (from l in context.Lodgings
                            where l.EmployeeId == employeeId && l.Date >= initialDate && l.Date <= finalDate
                            select l).ToList();

            return lodgings.Count * salary;
        }
        #endregion

        #region Anual statics
        public static float GetPurchasedProductsProfit(DataContext context, DateTime initialDate, DateTime finalDate)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);


            float purchasedProductsCost = 0;
            var purchasedProducts = GetPurchasedProducts(context, initialDate, finalDate);
            foreach (var item in purchasedProducts)
            {
                purchasedProductsCost += item.ProductAmount * item.ProductCost;
            }
            float purchasedProductIncome = GetPurchasedProductsIncome(context, initialDate, finalDate);
            return purchasedProductIncome - purchasedProductsCost;
        }
        public static float GetGrossIncome(DataContext context, DateTime initialDate, DateTime finalDate)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            float purchasedProductIncome = GetPurchasedProductsIncome(context, initialDate, finalDate);
            float IncomePerRent = GetIncomePerRent(context, initialDate, finalDate);
            float IncomePerDamage = GetIncomePerDamage(context, initialDate, finalDate);

            return purchasedProductIncome + IncomePerRent + IncomePerDamage;
        }
        public static float GetMaintenanceExpenses(DataContext context, DateTime initialDate, DateTime finalDate)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            var maintenancesExpenses = context.Maintenances.Where(m => m.Date >= initialDate && m.Date <= finalDate);

            return maintenancesExpenses.Sum(m => m.Cost);
        }
        public static bool IsLeapYear(int year)
        {
            if (year % 4 == 0)
                return true;

            if (year % 100 == 0)
                return false;

            if (year % 4 == 0)
                return true;

            return false;
        }
        #endregion

        #region Fund Operations
        public static List<FundMoneyOperation> GetFundOperations(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            return context.FundMoneyOperations.Include(m => m.Employee).Where(m => m.Date >= initialDate && m.Date <= finalDate).ToList();
        }
        public static float GetTotalFundDeposits(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            return context.FundMoneyOperations.Where(m => m.Date >= initialDate && m.Date <= finalDate && m.IsExtraction == false).Select(m => m.Amount).Sum();
        }
        public static float GetTotalFundExtractions(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            return context.FundMoneyOperations.Where(m => m.Date >= initialDate && m.Date <= finalDate && m.IsExtraction).Select(m => m.Amount).Sum();
        }
        public static float GetFundOperationsIncome(DataContext context, DateTime initialDate = default, DateTime finalDate = default)
        {
            initialDate = ValidateInitialDate(initialDate);
            finalDate = ValidateFinalDate(finalDate);

            var totalMoneyDeposits = GetTotalFundDeposits(context, initialDate, finalDate);
            var totalMoneyExtractions = GetTotalFundExtractions(context, initialDate, finalDate);

            return totalMoneyDeposits - totalMoneyExtractions;
        }
        #endregion

        #region GetShifts
        public static List<Shift> GetShifts()
        {
            List<Shift> result = new List<Shift>
            {
                new Shift{ InitialDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,9,0,0),
                           FinalDate   = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,12,0,0) },
                new Shift{ InitialDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,12,0,0),
                           FinalDate   = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,15,0,0) },
                new Shift{ InitialDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,15,0,0),
                           FinalDate   = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,18,0,0) },
                new Shift{ InitialDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,18,0,0),
                           FinalDate   = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,21,0,0) },
                new Shift{ InitialDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,22,0,0),
                           FinalDate   = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.AddDays(1).Day,8,0,0) }
            };
            return result;
        }
        #endregion
    }
}
