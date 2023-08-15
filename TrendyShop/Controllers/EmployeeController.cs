using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyShop.Data;
using TrendyShop.Models;
using TrendyShop.Utility;
using TrendyShop.ViewModels;

namespace TrendyShop.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class EmployeeController : Controller
    {
        private DataContext context;

        public EmployeeController(DataContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index() //Me Lista a los empleados
        {

            var employees = context.Employees.ToList();
            return View(employees);
        }

        public IActionResult NewEmployee()
        {
            var employee = new Employee();
            return View(employee);
        }

        public IActionResult CreateEmployee(Employee employee) //Ingresar un empleado
        {
            var e = context.Employees.Find(employee.EmployeeId);
            if (e != null)
            {
                //e.Already = true;
                // return View("NewEmployee", employee);
                return PartialView("ErrorModal");
            }



            if (!ModelState.IsValid)
            {
                return View("NewEmployee", employee);
            }

            context.Employees.Add(employee);
            context.SaveChanges();

            return RedirectToAction("Index", "Employee");
        }

        public IActionResult EmployeeProfile(string id) //Perfil del empleado
        {

            var employee = context.Employees.Find(id);
            var lodgings = context.Lodgings.Where(l => l.EmployeeId == id && l.Date > employee.LastPayment).OrderBy(l => l.Date).ToList();

            var lodgingsDates = lodgings.GroupBy(c => c.Date);

            var profileViewModel = new EmployeeProfileViewModel()
            {
                Employee = employee,
                Lodgings = lodgingsDates
            };



            int salary = 0;
            foreach (var item in lodgingsDates)
            {
                salary += item.Count() * 3;               //Le puse cualquier salario
            }


            employee.PendingPayment = salary;
            //employee.TotalSalary += salary;
            context.SaveChanges();
            return View(profileViewModel);
        }

        public IActionResult PaymentRecords(string eid)
        {
            var paymentRecords = context.EmployeePaymentRecords.Where(e => e.EmployeeId == eid).ToList();
            var e = context.Employees.Single(e => e.EmployeeId == eid);

            return View(new Tuple<string, List<EmployeePaymentRecords>, string>(e.Name, paymentRecords, eid));

        }

        [HttpGet]
        public IActionResult Edit([Bind("eid")] string eid) //Editar un empleado
        {
            var employee = context.Employees.Single(a => a.EmployeeId == eid);

            return View(employee);
        }


        public IActionResult ToPay(string eid) //Pagar a un empleado
        {
            var employee = context.Employees.Single(a => a.EmployeeId == eid);
            if (employee.PendingPayment > 0)
            {

                employee.LastPayment = DateTime.UtcNow;
                EmployeePaymentRecords paymentRecord = new EmployeePaymentRecords
                {
                    EmployeeId = employee.EmployeeId,
                    Employee = employee,
                    Date = employee.LastPayment,
                    Payment = employee.PendingPayment
                };

                context.EmployeePaymentRecords.Add(paymentRecord);
                employee.PendingPayment = 0;

                context.SaveChanges();
            }
            return RedirectToAction("EmployeeProfile", "Employee", new { id = eid });
        }


        public IActionResult Restart(string eid) //Reiniciar el pago de un empleado cndo admin decida
        {
            var employee = context.Employees.Single(a => a.EmployeeId == eid);
            employee.TotalSalary = 0;
            employee.LastRestart = DateTime.UtcNow;
            context.SaveChanges();
            return RedirectToAction("EmployeeProfile", "Employee", new { id = eid });
        }



        public IActionResult SaveEdit(Employee employee) //Salvar la edicion
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", employee);
            }

            var employeeDb = context.Employees.SingleOrDefault(a => a.EmployeeId == employee.EmployeeId);

            employeeDb.Name = employee.Name;
            employeeDb.Phone = employee.Phone;

            context.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }

        public IActionResult Delete(string eid) //Elimiar un empleado
        {
            var employee = context.Employees.Find(eid);

            context.Employees.Remove(employee);
            context.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }

    }
}
