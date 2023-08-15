using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrendyShop.Data;
using TrendyShop.Models;
using TrendyShop.Utility;
using TrendyShop.ViewModels;

namespace TrendyShop.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class StockController : Controller
    {
        private DataContext context;

        public StockController(DataContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index(DateTime lastStockDate)
        {
            bool isPreviousStock = lastStockDate != default ? true : false;

            var lastStock = Statics.GetLastStock(context, default, lastStockDate);

            var vm = new StockViewModel
            {
                Lodgins = Statics.GetLodgings(context, lastStock.Date).OrderByDescending(l => l.LodgingNumber).ToList(),
                IncomePerRent = Statics.GetIncomePerRent(context, lastStock.Date),
                IncomePerDamage = Statics.GetIncomePerDamage(context, lastStock.Date),
                Products = Statics.GetIncomePerProduct(context, lastStock.Date),
                ProductsIncome = Statics.GetPurchasedProductsIncome(context, lastStock.Date),
                LastFund = lastStock.Fund,
                LastStockDate = lastStock.Date,
                MoneyOperationsIncome = Statics.GetMoneyOperationsIncome(context, lastStock.Date),
                IsPreviousStock = isPreviousStock
            };
            return View(vm);
        }

        public IActionResult Save(float fund)
        {
            context.Stocks.Add(
                new StockTaking
                {
                    Date = DateTime.Now,
                    Fund = fund
                }
                );
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult PreviousStocks()
        {
            var result = context.Stocks.ToList();
            return View(result);
        }
    }
}