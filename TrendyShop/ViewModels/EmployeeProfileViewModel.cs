using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class EmployeeProfileViewModel
    {
        public Employee Employee { get; set; }

        public IEnumerable<IGrouping<DateTime, Lodging>> Lodgings { get; set; }
    }
}
