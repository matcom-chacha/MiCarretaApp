using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyShop.Data;
using TrendyShop.Models;
using TrendyShop.ViewModels;

namespace TrendyShop.Controllers
{
    [Authorize]
    public class DailyClosingController : Controller
    {
        private DataContext dataContext;
        private UserManager<IdentityUser> userManager;

        public DailyClosingController(DataContext ctx, UserManager<IdentityUser> _userManager)
        {
            dataContext = ctx;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            var datesOfClosings = dataContext.DatesOfClosings.OrderByDescending(d => d.Date).ToList();

            return View(datesOfClosings);
        }

        public List<MissingRecord> GetPendingMissings(DateTime dateOfClosing)
        {
            var missings = dataContext.Missings
                .Where(m => m.Date <= dateOfClosing)
                .GroupBy(g => new { g.Name, g.Cost })
                .Select(g => new { Name = g.Key.Name, Cost = g.Key.Cost, Amount = g.Sum(m => m.Amount), });

            var repositions = dataContext.Repositions
                .Where(m => m.Date <= dateOfClosing)
                .GroupBy(g => new { g.Name, g.Cost })
                .Select(g => new { Name = g.Key.Name, Cost = g.Key.Cost, Amount = g.Sum(m => m.Amount), });

            var missingProductsTable = from missing in missings
                                       join reposition in repositions
                                       on new { missing.Name, missing.Cost } equals new { reposition.Name, reposition.Cost }
                                       into joined
                                       from j in joined.DefaultIfEmpty()
                                       select new
                                       {
                                           Name = missing.Name,
                                           Cost = missing.Cost,
                                           AmountMissing = missing.Amount,
                                           AmountReturned = j.Amount
                                       };
            var missingProducts = new List<MissingRecord>();
            foreach (var item in missingProductsTable)
            {
                float dif = item.AmountMissing - item.AmountReturned;
                if (dif > 0)
                {
                    var missingProduct = new MissingRecord
                    {
                        Name = item.Name,
                        Cost = item.Cost,
                        Amount = dif
                    };
                    missingProducts.Add(missingProduct);
                }
            }

            return missingProducts;
        }

        public IActionResult Details(long dateOfClosingTicks)
        {
            var date = new DateTime(dateOfClosingTicks);
            var dateOfClosing = dataContext.DatesOfClosings.Include(dc => dc.Employee).Single(dc => dc.Date == date);
            var closing = dataContext.DailyClosings.Where(dc => dc.Date == date).ToList();
            var missingOfTheDay = dataContext.Missings.Where(m => m.Date == date).ToList();
            var repositionsOfTheDay = dataContext.Repositions.Where(r => r.Date == date).ToList();
            var missingProducts = GetPendingMissings(date);

            var dcDetaislViewModel = new DailyClosingDetailsViewModel
            {
                Date = date,
                DailyClosingRows = closing,
                MissingsOfTheDay = missingOfTheDay,
                Repositions = repositionsOfTheDay,
                Missings = missingProducts,
                Employee = dateOfClosing.Employee,
                Approved = dateOfClosing.Approved
            };
            return View(dcDetaislViewModel);
        }

        public List<GastronomicProduct> GetProductsInStorage()
        {
            var storageWithProducts = dataContext.Storage.Include(s => s.GastronomicProduct).ToList();
            var productsInStorage = new List<GastronomicProduct>();
            foreach (var item in storageWithProducts)
            {
                productsInStorage.Add(item.GastronomicProduct);
            }
            return productsInStorage;
        }

        public List<DailyClosing> GetDailyClosingNumbers(DateTime currentClosingDate, DateTime lastClosingDate, out List<float> previousClosingNumbers)
        {
            var products = dataContext.GastronomicProducts.ToList();
            var storage = dataContext.Storage.ToList();
            var roomProducts1 = dataContext.RoomProducts.Where(r => r.RoomId == 1).ToList();
            var roomProducts2 = dataContext.RoomProducts.Where(r => r.RoomId == 2).ToList();
            var roomProducts3 = dataContext.RoomProducts.Where(r => r.RoomId == 3).ToList();

            var todaysConsuptionPerLodgings = dataContext.PurchasePerLodgings
                .Include(pl => pl.Lodging)
                .Where(pl => pl.Lodging.ActualIDate > lastClosingDate
                && pl.Lodging.ActualFDate <= currentClosingDate)
                /*.ToList()*/;//para esto ActualIDate and ActualFDate can't be null

            var todaysConsuption = (from pl in todaysConsuptionPerLodgings
                                    group pl by new { pl.Cost, pl.Name }
                                    into grp
                                    select new
                                    {
                                        grp.Key.Cost,
                                        grp.Key.Name,
                                        Amount = grp.Sum(pl => pl.Amount)
                                    }).ToList();


            //List<DailyClosing> lastClosing = null;
            List<DailyClosing> lastClosing = new List<DailyClosing>();
            if (lastClosingDate != currentClosingDate)//ni idea de por que me deja comparar dateOfClosing con DateTime
            {
                lastClosing = dataContext.DailyClosings/*.Include(dc=>dc.Product)*/.
                    Where(dc => dc.Date == lastClosingDate).ToList();
            }

            var TodaysReplenish = dataContext.GastronomicProductOperations
                .Where(ri => ri.Date > lastClosingDate.Date && ri.Date < currentClosingDate);

            var replenishedProducts = (from ri in TodaysReplenish
                                       group ri by new { ri.Cost, ri.Name }
                                       into grp
                                       select new
                                       {
                                           grp.Key.Cost,
                                           grp.Key.Name,
                                           Amount = grp.Sum(ri => ri.OperationAmount)
                                       }).ToList();

            var todaysClosings = new List<DailyClosing>();
            previousClosingNumbers = new List<float>();
            foreach (var product in products)
            {
                //var storageAmount = from sr in storage where (sr.Name == product.Name && sr.Cost == product.Cost) select(sr.AmountAvailable);
                var sr = storage.Where(sr => sr.Name == product.Name && sr.Cost == product.Cost).SingleOrDefault();
                var storageAmount = sr == null ? 0 : sr.AmountAvailable;

                var previousClosing = lastClosing.Where(dc => dc.Name == product.Name && dc.Cost == product.Cost).SingleOrDefault();
                var previousClosingAmount = previousClosing == null ? 0 : previousClosing.TotalAmount;

                //Ahora se chequea por cero aqui en previous closing
                if (sr != null || previousClosingAmount != 0)
                {
                    var r1 = roomProducts1.SingleOrDefault(r1 => r1.Name == product.Name && r1.Cost == product.Cost);
                    var r1Amount = r1 == null ? 0 : r1.Amount;

                    var r2 = roomProducts2.SingleOrDefault(r2 => r2.Name == product.Name && r2.Cost == product.Cost);
                    var r2Amount = r2 == null ? 0 : r2.Amount;

                    var r3 = roomProducts3.SingleOrDefault(r3 => r3.Name == product.Name && r3.Cost == product.Cost);
                    var r3Amount = r3 == null ? 0 : r3.Amount;

                    //var totalAmount = storageAmount + r1Amount + r2Amount + r3Amount;

                    var consumption = todaysConsuption.SingleOrDefault(c => c.Name == product.Name && c.Cost == product.Cost);
                    //var consumptionAmount = consumption == null ? 0 : consumption.Amount;

                    //añadir los repuestos por faltantes
                    //var reposition = dataContext.Repositions.SingleOrDefault(r => r.Name == product.Name && r.Cost == product.Cost && r.Date > lastClosingDate);
                    //var repositionAmount = reposition == null ? 0 : reposition.Amount;
                    //var replenish = replenishedProducts.Where(r => r.Name == product.Name && r.Cost == product.Cost).SingleOrDefault();
                    var replenish = replenishedProducts.SingleOrDefault(r => r.Name == product.Name && r.Cost == product.Cost);
                    var replenishAmount = replenish == null ? 0 : replenish.Amount;

                    todaysClosings.Add(
                        new DailyClosing
                        {
                            Date = currentClosingDate,
                            Cost = product.Cost,
                            Name = product.Name,
                            AmountInStorage = storageAmount,
                            Room1 = r1Amount,
                            Room2 = r2Amount,
                            Room3 = r3Amount,
                            TotalAmount = storageAmount + r1Amount + r2Amount + r3Amount,
                            ConsuptionSincePreviuosClosing = consumption == null ? 0 : consumption.Amount,
                            Replenished = replenishAmount //+ repositionAmount,
                            //PreviousClousing = lastClosingDate
                        });
                    previousClosingNumbers.Add(previousClosingAmount);
                }
            }
            return todaysClosings;
        }

        public IActionResult Close()
        {
            string employeeId = userManager.GetUserId(User);
            var employee = dataContext.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);

            var currentClosingDate = DateTime.Now;

            var lastClosingDate = dataContext.DatesOfClosings.Any() ? dataContext.DatesOfClosings.Max(d => d.Date) : currentClosingDate;

            var missingProducts = GetPendingMissings(currentClosingDate);
            var productsInStorage = GetProductsInStorage();
            var previousClosingNumbers = new List<float>();
            var todaysClosings = GetDailyClosingNumbers(currentClosingDate, lastClosingDate, out previousClosingNumbers);

            var dcvm2 = new DailyClosingViewModel
            {
                Date = currentClosingDate,
                PreviousClosingDate = lastClosingDate,
                TodaysClosings = todaysClosings,
                PreviousClosingNumbers = previousClosingNumbers,
                MissingProduct = new MissingRecord(),
                MissingProducts = missingProducts,//a;adir los que se han hecho si hay
                MissingProductsToReport = new List<MissingRecord>(),
                RepositionRecord = new RepositionRecord(),
                Repositions = new List<RepositionRecord>(),//a;adir las pendientes is hay
                Products = productsInStorage,
                Employee = employee
            };

            return View(dcvm2);
        }

        public void SaveDateOfClosing(DateTime currentDate, DateTime previousClosingDate, string employeeId)
        {
            DateOfClosing todaysDateOfClosing = new DateOfClosing
            {
                Date = currentDate,
                PreviousClousingDate = previousClosingDate,
                EmployeeId = employeeId
            };
            dataContext.DatesOfClosings.Add(todaysDateOfClosing);
            dataContext.SaveChanges();
        }

        public void UpdateStorageRow(string name, float cost, float amount)
        {//deberia hacer esto solo si amount es mayor que 0
            var storageRowToUpdate = dataContext.Storage
                       .SingleOrDefault(sr => sr.Cost == cost && sr.Name == name);

            if (storageRowToUpdate == null)//en este caso amount es positivo porque se está añadiendo BD
            {
                var newSR = new StorageRow()
                {
                    Cost = cost,
                    Name = name,
                    AmountAvailable = amount
                };
                dataContext.Storage.Add(newSR);
            }
            else
            {
                storageRowToUpdate.AmountAvailable += amount;
            }
            dataContext.SaveChanges();

            var storageRowToDelete = dataContext.Storage
                          .SingleOrDefault(sr => sr.Cost == cost && sr.Name == name);
            if (storageRowToDelete.TotalAmount == 0)
            {
                dataContext.Storage.Remove(storageRowToDelete);
                dataContext.SaveChanges();
            }
        }

        public void UpdateMoneyOperations(DateTime currentDate, float amount, string employeeId, float cost)
        {
            var absoluteValue = Math.Abs(amount);
            if (absoluteValue > 0)
            {
                dataContext.MoneyOperations.Add(
                new MoneyOperation
                {
                    IsExtraction = (absoluteValue == amount) ? false : true,
                    Amount = amount * cost,
                    Date = currentDate,
                    //Subject = "",
                    EmployeeId = employeeId,
                    //Employee = employee,
                    MOTypeId = dataContext.MOTypes.SingleOrDefault(mot => mot.Name == "Reposition").MOTypeId//new
                });
                dataContext.SaveChanges();
            }
        }

        public void UpdateAmounts(string name, float cost, DateTime currentDate, float amount, float forStorage, float forMoneyOperations, bool oldType, bool newType, string employeeId)
        {
            if (oldType == newType)
            {
                if (!newType)
                    UpdateStorageRow(name, cost, amount);
                else
                    UpdateMoneyOperations(currentDate, amount, employeeId, cost);
            }
            else
            {
                UpdateStorageRow(name, cost, forStorage);
                UpdateMoneyOperations(currentDate, forMoneyOperations, employeeId, cost);
            }
        }

        public void SaveReports(DateTime currentDate, IList<MissingRecord> missingRecords)
        {
            var reportsInDb = dataContext.Missings.Where(r => r.Date == currentDate).ToList();

            var leftOuterJoin = from dbreport in reportsInDb
                                join report in missingRecords
                                on new { dbreport.Name, dbreport.Cost } equals new { report.Name, report.Cost }
                                into joined
                                from j in joined.DefaultIfEmpty()
                                select new
                                {
                                    Name = dbreport.Name,
                                    Cost = dbreport.Cost,
                                    AmountInDb = dbreport.Amount,
                                    CorrectAmount = j == null ? 0 : j.Amount
                                };

            var rightOuterJoin = from report in missingRecords
                                 join dbreport in reportsInDb
                                 on new { report.Name, report.Cost } equals new { dbreport.Name, dbreport.Cost }
                                into joined
                                 from j in joined.DefaultIfEmpty()
                                 select new
                                 {
                                     Name = report.Name,
                                     Cost = report.Cost,
                                     AmountInDb = j == null ? 0 : j.Amount,
                                     CorrectAmount = report.Amount
                                 };

            var missingsjoin = leftOuterJoin.Union(rightOuterJoin).ToList();
            float amount = -1;

            foreach (var record in missingsjoin)
            {
                if (record.AmountInDb == 0)
                {
                    //añade a BD
                    var missingProduct = new MissingRecord
                    {
                        Date = currentDate,
                        Name = record.Name,
                        Cost = record.Cost,
                        Amount = record.CorrectAmount
                    };
                    dataContext.Missings.Add(missingProduct);
                    amount = -record.CorrectAmount;
                }
                else
                {
                    var toUpdate = dataContext.Missings
                        .Single(m => m.Date == currentDate
                        && m.Cost == record.Cost
                        && m.Name == record.Name);

                    if (record.CorrectAmount == 0)
                    {//elimina de BD
                        dataContext.Missings.Remove(toUpdate);

                        amount = record.AmountInDb;
                    }
                    else
                    { //update en BD
                        toUpdate.Amount = record.CorrectAmount;
                        amount = record.AmountInDb - record.CorrectAmount;
                    }
                }

                dataContext.SaveChanges();

                UpdateStorageRow(record.Name, record.Cost, amount);
            }
        }

        public void SaveRepositions(DateTime currentDate, IList<RepositionRecord> repositions, string employeeId)
        {
            var reportsInDb = dataContext.Repositions.Where(r => r.Date == currentDate).ToList();

            var leftOuterJoin = from dbreport in reportsInDb
                                join report in repositions
                                on new { dbreport.Name, dbreport.Cost } equals new { report.Name, report.Cost }
                                into joined
                                from j in joined.DefaultIfEmpty()
                                select new
                                {
                                    Name = dbreport.Name,
                                    Cost = dbreport.Cost,
                                    AmountInDb = dbreport.Amount,
                                    CorrectAmount = j == null ? 0 : j.Amount,
                                    OldType = dbreport.Paid,
                                    NewType = j == null ? false : j.Paid
                                };

            var rightOuterJoin = from report in repositions
                                 join dbreport in reportsInDb
                                 on new { report.Name, report.Cost } equals new { dbreport.Name, dbreport.Cost }
                                 into joined
                                 from j in joined.DefaultIfEmpty()
                                 select new
                                 {
                                     Name = report.Name,
                                     Cost = report.Cost,
                                     AmountInDb = j == null ? 0 : j.Amount,
                                     CorrectAmount = report.Amount,
                                     OldType = j == null ? false : j.Paid,
                                     NewType = report.Paid
                                 };

            var reportsjoin = leftOuterJoin.Union(rightOuterJoin).ToList();
            float amount = -1;
            //igual tipo actualizo, diferente borro y añado en la tabla correspondiente
            foreach (var reposition in reportsjoin)
            {
                float forStorage = 0;
                float forMoneyOperations = 0;

                if (reposition.AmountInDb == 0)
                {
                    //añade a BD
                    var repositionRow = new RepositionRecord
                    {
                        Date = currentDate,
                        Name = reposition.Name,
                        Cost = reposition.Cost,
                        Amount = reposition.CorrectAmount,
                        Paid = reposition.NewType
                    };
                    dataContext.Repositions.Add(repositionRow);

                    amount = reposition.CorrectAmount;
                    if (reposition.NewType)//si es pagado
                        forMoneyOperations = reposition.CorrectAmount;
                    else
                        forStorage = reposition.CorrectAmount;
                }
                else
                {
                    var toUpdate = dataContext.Repositions
                        .Single(m => m.Date == currentDate
                        && m.Cost == reposition.Cost
                        && m.Name == reposition.Name);

                    if (reposition.CorrectAmount == 0)
                    {//elimina de BD
                        dataContext.Repositions.Remove(toUpdate);

                        amount = -reposition.AmountInDb;
                        if (reposition.OldType)//si es pagado
                            forMoneyOperations = -reposition.AmountInDb;
                        else
                            forStorage = -reposition.AmountInDb;
                    }
                    else
                    { //update en BD
                        if (reposition.OldType == reposition.NewType)
                        {
                            amount = reposition.CorrectAmount - reposition.AmountInDb;
                        }
                        else
                        {
                            if (reposition.OldType)//si es pagado
                            {
                                forMoneyOperations = -reposition.AmountInDb;
                                forStorage = reposition.CorrectAmount;
                            }
                            else
                            {
                                forStorage = -reposition.AmountInDb;
                                forMoneyOperations = reposition.CorrectAmount;
                            }

                        }
                        toUpdate.Amount = reposition.CorrectAmount;
                        toUpdate.Paid = reposition.NewType;
                    }
                }

                dataContext.SaveChanges();
                UpdateAmounts(reposition.Name, reposition.Cost, currentDate, amount, forStorage, forMoneyOperations, reposition.OldType, reposition.NewType, employeeId);
            }
        }

        public void SaveClosingRows(DateTime currentDate, DateTime lastClosingDate, IList<DailyClosing> todaysClosings)
        {
            foreach (var item in todaysClosings)
            {
                var dailyClosingRow = dataContext.DailyClosings
                    .SingleOrDefault(dc => dc.Date == currentDate && dc.Name == item.Name && dc.Cost == item.Cost);
                var previousDailyClosingRow = dataContext.DailyClosings
                    .SingleOrDefault(dc => dc.Date == lastClosingDate && dc.Name == item.Name && dc.Cost == item.Cost);

                if (dailyClosingRow == null)
                {
                    if (item.TotalAmount != 0 || previousDailyClosingRow.TotalAmount != 0)
                    {
                        //var newDailyClosing = new DailyClosing
                        //{
                        //    Date = currentDate,//esto se lo acabo de anadir
                        //    Cost = item.Cost,
                        //    Name = item.Name,//todo lo demas esta en 0, excepto total y repuesto que se actualiza ahora
                        //    AmountInStorage = item.AmountInStorage,
                        //    Room1 = item.Room1,
                        //    Room2 = item.Room2,
                        //    Room3 = item.Room3,
                        //    TotalAmount = item.TotalAmount,
                        //    ConsuptionSincePreviuosClosing = item.ConsuptionSincePreviuosClosing,
                        //    Replenished = item.Replenished
                        //};
                        //dataContext.DailyClosings.Add(newDailyClosing);

                        item.Date = currentDate;
                        dataContext.DailyClosings.Add(item);

                        dataContext.SaveChanges();
                    }
                }
                else
                {
                    if (item.TotalAmount == 0 && previousDailyClosingRow.TotalAmount == 0)
                        dataContext.DailyClosings.Remove(dailyClosingRow);
                    else
                    {
                        dailyClosingRow.AmountInStorage = item.AmountInStorage;
                        dailyClosingRow.TotalAmount = item.TotalAmount;
                        dailyClosingRow.Replenished = item.Replenished;
                    }
                    dataContext.SaveChanges();
                }

            }
        }

        public IActionResult SaveClosing(DailyClosingViewModel dcvm)
        {
            if(dcvm.TodaysClosings == null)
                return RedirectToAction("Index", "DailyClosing");

            string employeeId = userManager.GetUserId(User);
            var employee = dataContext.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
            var currentDate = new DateTime(dcvm.DateTicks);
            var previousClosingDate = new DateTime(dcvm.PreviousClosingTicks);

            if (dcvm.MissingProductsToReport == null)//new
                dcvm.MissingProductsToReport = new List<MissingRecord>();
            if (dcvm.Repositions == null)
                dcvm.Repositions = new List<RepositionRecord>();

            SaveDateOfClosing(currentDate, previousClosingDate, employeeId);
            SaveReports(currentDate, dcvm.MissingProductsToReport);
            //dcvm.TodaysClosings = SaveRepositions(currentDate, dcvm.Repositions, employeeId, dcvm.TodaysClosings);
            SaveRepositions(currentDate, dcvm.Repositions, employeeId);
            SaveClosingRows(currentDate, previousClosingDate, dcvm.TodaysClosings);

            return RedirectToAction("Index", "DailyClosing");
        }

        public List<GastronomicProduct> GetProductsInStorage(long dateOfClosingTicks = -1)
        {
            var storageWithProducts = dataContext.Storage.Include(s => s.GastronomicProduct).ToList();
            var productsInStorage = new List<GastronomicProduct>();
            foreach (var item in storageWithProducts)
            {
                productsInStorage.Add(item.GastronomicProduct);
            }

            if (dateOfClosingTicks != -1)
            {
                var date = new DateTime(dateOfClosingTicks);
                var closing = dataContext.DailyClosings.Include(dc => dc.Product).Where(dc => dc.Date == date).ToList();
                foreach (var item in closing)
                {
                    if (!productsInStorage.Contains(item.Product))
                        productsInStorage.Add(item.Product);
                }
            }
            return productsInStorage;
        }
        //public List<MissRep> GetTodaysNumbers(DateTime currentClosingDate, DateTime lastClosingDate)
        //{
        //    var TodaysReplenish = dataContext.ReplenishInfoGProds
        //      .Where(ri => ri.Date > lastClosingDate.Date && ri.Date < currentClosingDate);

        //    var replenishedProducts = (from ri in TodaysReplenish
        //                               group ri by new { ri.Cost, ri.Name }
        //                               into grp
        //                               select new
        //                               {
        //                                   grp.Key.Cost,
        //                                   grp.Key.Name,
        //                                   Amount = grp.Sum(ri => ri.ToAddAmount)
        //                               }).ToList();

        //    var productsInStorage = dataContext.Storage.ToList();
        //    var todaysNumbers = new List<MissRep>();
        //    foreach (var product in productsInStorage)
        //    {
        //        var reposition = dataContext.Repositions.SingleOrDefault(r => r.Name == product.Name && r.Cost == product.Cost && r.Date == currentClosingDate);
        //        var repositionAmount = reposition == null ? 0 : reposition.Amount;
        //        var replenish = replenishedProducts.SingleOrDefault(r => r.Name == product.Name && r.Cost == product.Cost);
        //        var replenishAmount = replenish == null ? 0 : replenish.Amount;

        //        var newRow = new MissRep
        //        {
        //            Cost = product.Cost,
        //            Name = product.Name,
        //            AmountInStorage = product.AmountAvailable,
        //            Replenished = repositionAmount + replenishAmount
        //        };

        //        todaysNumbers.Add(newRow);
        //    }

        //    return todaysNumbers;
        //}

        public IActionResult Edit(long dateOfClosingTicks)
        {
            var date = new DateTime(dateOfClosingTicks);
            var dateOfClosing = dataContext.DatesOfClosings.Include(dc => dc.Employee).Single(dc => dc.Date == date);
            var closing = dataContext.DailyClosings.Where(dc => dc.Date == date).ToList();
            var missingOfTheDay = dataContext.Missings.Where(m => m.Date == date).ToList();
            var repositionsOfTheDay = dataContext.Repositions.Where(r => r.Date == date).ToList();
            var missingProducts = GetPendingMissings(date);
            var productsInStorage = GetProductsInStorage(dateOfClosingTicks);//add missing products from yesterday
            var previousClosingNumbers = new List<float>();

            for (int i = 0; i < closing.Count(); i++)
            {
                var previousClosingRow = dataContext.DailyClosings
                    .SingleOrDefault(dc => dc.Date == dateOfClosing.PreviousClousingDate
                    && dc.Name == closing[i].Name
                    && dc.Cost == closing[i].Cost);

                var amountToAdd = previousClosingRow == null ? 0 : previousClosingRow.TotalAmount;

                previousClosingNumbers.Add(amountToAdd);
            }

            var dcViewModel = new DailyClosingViewModel
            {
                Date = date,
                TodaysClosings = closing,
                MissingProductsToReport = missingOfTheDay,
                Repositions = repositionsOfTheDay,
                MissingProducts = missingProducts,
                PreviousClosingDate = dateOfClosing.PreviousClousingDate,
                PreviousClosingNumbers = previousClosingNumbers,
                MissingProduct = new MissingRecord(),
                RepositionRecord = new RepositionRecord(),
                Products = productsInStorage,
                Employee = dateOfClosing.Employee
            };
            return View(dcViewModel);
        }

        public IActionResult SaveEdition(DailyClosingViewModel dcvm)
        {
            var currentDate = new DateTime(dcvm.DateTicks);
            var dateOfClosing = dataContext.DatesOfClosings.Include(dc => dc.Employee).Single(dc => dc.Date == currentDate);
            dateOfClosing.Approved = true;
            dataContext.SaveChanges();

            if (dcvm.MissingProductsToReport == null)
                dcvm.MissingProductsToReport = new List<MissingRecord>();
            if (dcvm.Repositions == null)
                dcvm.Repositions = new List<RepositionRecord>();
            //if (dcvm.TodaysClosings == null)
            //    dcvm.TodaysClosings= new List<DailyClosing>();Nunca va a ser null a no ser que no haya nada en el almacen

            SaveReports(currentDate, dcvm.MissingProductsToReport);
            SaveRepositions(currentDate, dcvm.Repositions, dateOfClosing.Employee.EmployeeId);
            SaveClosingRows(currentDate, dateOfClosing.PreviousClousingDate, dcvm.TodaysClosings);

            return RedirectToAction("Index", "DailyClosing");
        }


        public IActionResult Missings()
        {
            var currentDate = DateTime.Now;

            var missingProducts = GetPendingMissings(currentDate);
            return View(missingProducts);
        }
    }
}