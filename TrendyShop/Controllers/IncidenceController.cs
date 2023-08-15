using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrendyShop.Data;
using TrendyShop.Models;

namespace TrendyShop.Controllers
{
    public class IncidenceController : Controller
    {

        private DataContext context;

        public IncidenceController(DataContext ctx)
        {
            context = ctx;
        }


        public IActionResult Index()
        {
            var incidences = context.Incidences.ToList();

            return View(incidences);
        }


        public IActionResult NewIncidence()
        {
            var incidence = new Incidence();

            return View(incidence);
        }


        public IActionResult InsertIncidence(Incidence incidence)
        {

            if ((!ModelState.IsValid) || incidence.Price == 0)
            {
                return View("NewIncidence", incidence);
            }

            context.Incidences.Add(incidence);
            context.SaveChanges();
            return RedirectToAction("Index", "Incidence");

        }


        public IActionResult Delete(int iid)
        {
            var incidence = context.Incidences.Find(iid);

            context.Incidences.Remove(incidence);
            context.SaveChanges();
            return RedirectToAction("Index", "Incidence");
        }

        public IActionResult Edit(int iid)
        {
            var incidence = context.Incidences.Find(iid);

            return View(incidence);

        }


        public IActionResult SaveEdit(Incidence incidence)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", incidence);
            }


            var incidenceInDb = context.Incidences.Find(incidence.IncidenceId);
            incidenceInDb.Subject = incidence.Subject;
            incidenceInDb.Price = incidence.Price;
            incidenceInDb.Description = incidence.Description;

            context.SaveChanges();
            return RedirectToAction("Index", "Incidence");



        }



    }
}