using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrendyShop.Models;
using TrendyShop.ViewModels;
using TrendyShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace TrendyShop.Controllers
{
    [Authorize]
    public class PackageController : Controller
    {
        private DataContext context;

        public PackageController(DataContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var package = context.Package.Include(p => p.GastronomicProduct).ToList();
           
            return View(package);
        }

        public IActionResult Edit()
        {
            var packGastronomic = context.Package.Include(p => p.GastronomicProduct).ToList();
            var gProducts = context.GastronomicProducts.ToList();
            foreach (var item in packGastronomic)
                gProducts.Remove(item.GastronomicProduct);

            var viewModel = new PackageViewModel
            {
                PackGastronomics = context.Package.Include(p => p.GastronomicProduct).ToList(),
                PackToEdit = new Package {
                    GastronomicProduct = new GastronomicProduct(),
                },
                Products = gProducts,
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(float cost, string name, int quantity)
        {
            var product = context.GastronomicProducts.Single(p => p.Cost == cost && p.Name == name);
            //var product = context.Storage.Include(p => p.GastronomicProduct).Single(p => p.Cost == cost && p.Name == name);

            //if (product.AmountAvailable < quantity || quantity <= 0)
            //    throw new InvalidOperationException();

            context.Package.Add(new Package { Cost = cost, Name = name, GastronomicProduct = product, Quantity = quantity });
            context.SaveChanges();

            return RedirectToAction("Edit");
        }

        public IActionResult Delete(float cost, string name)
        {
            var packgGastronomic = context.Package.Find(cost, name);
            context.Package.Remove(packgGastronomic);
            context.SaveChanges();

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(float cost, string name, int quantity)
        {
            var packgGastronomic = context.Package.Find(cost, name);
            packgGastronomic.Quantity = quantity;
            context.SaveChanges();

            return RedirectToAction("Edit");
        }
    }
}