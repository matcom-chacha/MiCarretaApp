using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TrendyShop.Controllers
{
    public class InformeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}