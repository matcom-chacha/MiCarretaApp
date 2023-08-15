using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyShop.Data;
using TrendyShop.Models;
using TrendyShop.ViewModels;


namespace TrendyShop.Controllers
{
    [Authorize]
    public class HouseExpensesController : Controller
    {
        private DataContext context;
        public HouseExpensesController(DataContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var lastExpenses = new List<HouseExpense>
            {
              context.HouseExpenses.Include(h => h.ExpenseType).Where(h => h.ExpenseType.Name == "Electricidad").OrderByDescending(h => h.Date).FirstOrDefault(),
              context.HouseExpenses.Include(h => h.ExpenseType).Where(h => h.ExpenseType.Name == "Gas").OrderByDescending(h => h.Date).FirstOrDefault(),
              context.HouseExpenses.Include(h => h.ExpenseType).Where(h => h.ExpenseType.Name == "Lavandería").OrderByDescending(h => h.Date).FirstOrDefault(),
              context.HouseExpenses.Include(h => h.ExpenseType).Where(h => h.ExpenseType.Name == "Agua").OrderByDescending(h => h.Date).FirstOrDefault(),
            };

            var vm = new HouseExpensesViewModel
            {
                LastExpenses = lastExpenses,
                ExpenseTypes = context.ExpenseTypes.ToList(),
                DateFormat = DateTime.Today.ToString("yyyy-MM-dd")/* + "T" + DateTime.Today.ToString("hh:mm:ss")*/,
            };
            return View(vm);

        }

        [HttpPost]
        public IActionResult AddExpense(HouseExpense houseExpense)
        {
            //Si no es valido hay que avisar al usuario
            if (ModelState.IsValid)
            {
                houseExpense.ExpenseType = context.ExpenseTypes.Find(houseExpense.ExpenseTypeId);
                context.HouseExpenses.Add(houseExpense);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int heid)
        {
            var houseExpense = context.HouseExpenses.SingleOrDefault(c => c.Id == heid);
            context.Remove(houseExpense);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Records(int eid, string expenseType)
        {
            List<HouseExpense> expenses = context.HouseExpenses.Where(e => e.ExpenseTypeId == eid && e.Date.Year == DateTime.Now.Year).ToList();

            return View(new Tuple<string, List<HouseExpense>>(expenseType, expenses));

        }
    }
}