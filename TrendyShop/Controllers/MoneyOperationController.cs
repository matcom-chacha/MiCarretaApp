using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrendyShop.Models;
using TrendyShop.Data;
using Microsoft.EntityFrameworkCore;
using TrendyShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TrendyShop.Controllers
{
    [Authorize]
    public class MoneyOperationController : Controller
    {
        DataContext context;
        UserManager<IdentityUser> userManager;
        public MoneyOperationController(DataContext ctx, UserManager<IdentityUser> _userManager)
        {
            context = ctx;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            var lastStock = Statics.GetLastStock(context);
            //var incomePerRent = Statics.GetIncomePerRent(context, lastStock.Date);
            //var incomePerDamage = Statics.GetIncomePerDamage(context, lastStock.Date);

            bool approved = false;
            if(context.DatesOfClosings.Count() > 0)
                approved = context.DatesOfClosings.ToList().Last().Approved;
            List<MoneyOperation> MOperations = Statics.GetMoneyOperations(context, lastStock.Date);


            //float productsTotalIncome = Statics.GetPurchasedProductsIncome(context, lastStock.Date);

            var moneyOperationsIncome = Statics.GetMoneyOperationsIncome(context, lastStock.Date);

            //var houseExpenses = Statics.GetHouseExpenses(context, lastStock.Date);

            //var totalIncome = incomePerRent + incomePerDamage + productsTotalIncome + moneyOperationsIncome + lastStock.Fund - houseExpenses;

            var fund = Statics.GetFundOperationsIncome(context);
            var fundOperations = Statics.GetFundOperations(context).FindAll(fo => (fo.Date.Day == DateTime.Now.Day) && (fo.Date.Month == DateTime.Now.Month) && (fo.Date.Year == DateTime.Now.Year));
            if (fundOperations == null)
            {
                fundOperations = new List<FundMoneyOperation>();
            }

            var vm = new MoneyOperationViewModel
            {
                //TotalIncome = totalIncome,
                TotalIncome = moneyOperationsIncome,
                MoneyOperations = MOperations.FindAll(mo => (mo.Date.Day == DateTime.Now.Day) && (mo.Date.Month == DateTime.Now.Month) && (mo.Date.Year == DateTime.Now.Year)).ToList(),
                Fund = fund,
                FundMoneyOperations = fundOperations,
                Approved = approved
            };
            return View(vm);
        }
        public IActionResult ExtractForm()
        {
            return View(new MoneyOperation());
        }

        public IActionResult Extract(float amount, string subject)
        {
            string employeeId = userManager.GetUserId(User);
            var employee = context.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);

            if (amount > 0)//si no reportar!!!
            {
                context.MoneyOperations.Add(
                    new MoneyOperation
                    {
                        IsExtraction = true,
                        Amount = amount,
                        Date = DateTime.Now,
                        Subject = subject,
                        EmployeeId = employee.EmployeeId,
                        Employee = employee,
                        MOTypeId = context.MOTypes.SingleOrDefault(mot => mot.Name == "Withdraw").MOTypeId//new
                    }
                );
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //public bool PerformExtraction(float amount, string subject, string motype, DateTime extractionDate)
        //{
        //    var userName = HttpContext.User.Identity.Name;
        //    var user = usersContext.Users.Single(u => u.UserName == userName);
        //    var employee = context.Employees.SingleOrDefault(e => e.EmployeeId == user.Id);//verificar que esto setea adecuadamente el empleado

        //    if (amount > 0)
        //    {
        //        context.MoneyOperations.Add(
        //            new MoneyOperation
        //            {
        //                IsExtraction = true,
        //                Amount = amount,
        //                Date = extractionDate,
        //                Subject = subject,
        //                EmployeeId = employee.EmployeeId,
        //                Employee = employee,
        //                MOTypeId = context.MOTypes.SingleOrDefault(mot => mot.Name == motype).MOTypeId//new
        //            });
        //        context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        public IActionResult DepositForm()
        {
            return View(new MoneyOperation());
        }
        public IActionResult Deposit(float amount, string subject)
        {
            string employeeId = userManager.GetUserId(User);
            var employee = context.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);

            if (amount > 0)//si no reportar
            {
                context.MoneyOperations.Add(
                new MoneyOperation
                {
                    IsExtraction = false,
                    Amount = amount,
                    Date = DateTime.Now,
                    Subject = subject,
                    EmployeeId = employee.EmployeeId,
                    Employee = employee,
                    MOTypeId = context.MOTypes.SingleOrDefault(mot => mot.Name == "Injection").MOTypeId//new
                });
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //public bool MakeDeposit(float amount, string subject, string motype, DateTime extractionDate)
        //{
        //    var userName = HttpContext.User.Identity.Name;
        //    var user = usersContext.Users.Single(u => u.UserName == userName);
        //    var employee = context.Employees.SingleOrDefault(e => e.EmployeeId == user.Id);

        //    if (amount > 0)//si no reportar
        //    {
        //        context.MoneyOperations.Add(
        //        new MoneyOperation
        //        {
        //            IsExtraction = false,
        //            Amount = amount,
        //            Date = extractionDate,
        //            Subject = subject,
        //            EmployeeId = employee.EmployeeId,
        //            Employee = employee,
        //            MOTypeId = context.MOTypes.SingleOrDefault(mot => mot.Name == motype).MOTypeId//new
        //        });
        //        context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}
        //Fund
        public IActionResult FundExtractForm()
        {
            return View(new FundMoneyOperation());
        }

        public IActionResult FundExtract(float amount, string subject)
        {
            string employeeId = userManager.GetUserId(User);
            var employee = context.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);

            if (amount > 0)
            {
                context.FundMoneyOperations.Add(
                    new FundMoneyOperation
                    {
                        IsExtraction = true,
                        Amount = amount,
                        Date = DateTime.Now,
                        Subject = subject,
                        EmployeeId = employee.EmployeeId,
                        Employee = employee
                    }
                );
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult FundDepositForm()
        {
            return View(new FundMoneyOperation());
        }
        public IActionResult FundDeposit(float amount, string subject)
        {

            string employeeId = userManager.GetUserId(User);
            var employee = context.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);

            if (amount > 0)
            {
                context.FundMoneyOperations.Add(
                new FundMoneyOperation
                {
                    IsExtraction = false,
                    Amount = amount,
                    Date = DateTime.Now,
                    Subject = subject,
                    EmployeeId = employee.EmployeeId,
                    Employee = employee

                }
            );
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
