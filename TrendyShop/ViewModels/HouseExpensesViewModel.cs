using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class HouseExpensesViewModel
    {
        public List<HouseExpense> LastExpenses { get; set; }
        public List<ExpenseType> ExpenseTypes { get; set; }
        public HouseExpense NewExpense { get; set; }
        public string DateFormat { get; set; }
    }
}
