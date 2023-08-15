using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrendyShop.Data;
using TrendyShop.Models;

namespace TrendyShop.Controllers
{
    public class MaintenanceController : Controller
    {

        private DataContext context;

        public MaintenanceController(DataContext ctx)
        {
            context = ctx;
        }



        public IActionResult Index()
        {
            var maintenances = context.Maintenances.ToList(); 

            return View(maintenances);
        }


        public IActionResult NewMaintenance()
        {
            var maintenance = new Maintenance();

            return View(maintenance);
        }


        public IActionResult InsertMaintenance(Maintenance maintenance)
        {
            if (!ModelState.IsValid)
            {
                return View("NewMaintenance", maintenance);
            }

            context.Maintenances.Add(maintenance);
            context.SaveChanges();
            return RedirectToAction("Index", "Maintenance");

        }

        public IActionResult Edit(int mid)
        {

            var maintenace = context.Maintenances.Find(mid);

            return View(maintenace);
        }

        public IActionResult SaveEdit(Maintenance maintenance)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", maintenance);
            }

            var maintenanceDB = context.Maintenances.Find(maintenance.MaintenanceId);
            maintenanceDB.Description = maintenance.Description;
            maintenanceDB.Cost= maintenance.Cost;
            maintenanceDB.Date= maintenance.Date;
            context.SaveChanges();

            return RedirectToAction("Index", "Maintenance");
        }

        public IActionResult Delete(int mid)
        {
            var maintenance = context.Maintenances.Find(mid);

            context.Maintenances.Remove(maintenance);
            context.SaveChanges();
            return RedirectToAction("Index", "Maintenance");
        }



    }
}