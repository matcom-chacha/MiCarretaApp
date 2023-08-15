using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrendyShop.Data;
using TrendyShop.ViewModels;

namespace TrendyShop.Controllers
{
    public class SettingsController : Controller
    {
        private DataContext context;
        public SettingsController(DataContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var vm = new SettingsViewModel
            {
                EmployeeSalary = context.SystemDefaultPrices.Single(s => s.Name == "Salary").Amount,
                RentCost = context.SystemDefaultPrices.Single(s => s.Name == "RentCost").Amount,
                RomanticTax = context.SystemDefaultPrices.Single(s => s.Name == "RomanticTax").Amount,
                LateHoursTax = context.SystemDefaultPrices.Single(s => s.Name == "LateHoursTax").Amount,
                CompanionTax = context.SystemDefaultPrices.Single(s => s.Name == "CompanionTax").Amount,
            };
            return View(vm);
        }

        public IActionResult SetEmployeeSalary(float employeeSalary)
        {
            //Assuming client-side validation
            context.SystemDefaultPrices.Single(s => s.Name == "Salary").Amount = employeeSalary;
            context.SaveChanges();


            //Let the client know the action was succesfull
            return RedirectToAction("Index");
        }

        public IActionResult SetRentCost(float rentCost)
        {
            //Assuming client-side validation
            context.SystemDefaultPrices.Single(s => s.Name == "RentCost").Amount = rentCost;
            context.SaveChanges();


            //Let the client know the action was succesfull
            return RedirectToAction("Index");
        }

        public IActionResult SetRomanticTax(float romanticTax)
        {
            //Assuming client-side validation
            context.SystemDefaultPrices.Single(s => s.Name == "RomanticTax").Amount = romanticTax;
            context.SaveChanges();

            
            //Let the client know the action was succesfull
            return RedirectToAction("Index");
        }

        public IActionResult SetLateHoursTax(float lateHOursTax)
        {
            //Assuming client-side validation
            context.SystemDefaultPrices.Single(s => s.Name == "LateHoursTax").Amount = lateHOursTax;
            context.SaveChanges();

            //Let the client know the action was succesfull
            return RedirectToAction("Index");
        }
        public IActionResult SetCompanionTax(float companionTax)
        {
            //Assuming client-side validation
            context.SystemDefaultPrices.Single(s => s.Name == "CompanionTax").Amount = companionTax;
            context.SaveChanges();

            //Let the client know the action was succesfull
            return RedirectToAction("Index");
        }
    }

}