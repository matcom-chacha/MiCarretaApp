using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrendyShop.Data;
using TrendyShop.Models;
using TrendyShop.Utility;
using TrendyShop.ViewModels;

namespace TrendyShop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private DataContext dataContext;

        public HomeController(DataContext ctx)
        {
            dataContext = ctx;
        }

        public IActionResult Index()
        {
            List<Shift> Shifts = Statics.GetShifts();
            int roomCount = dataContext.Rooms.Count();

            DateTime h9_pm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0);
            DateTime h10_pm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 0, 0);
            DateTime h8_am = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            DateTime h9_am = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);

            var shiftsAndReservations = new List<Tuple<Shift, List<Reservation>, List<Lodging>>>[roomCount];
            for (int i = 0, roomId; i < roomCount; i++)
            {
                roomId = i + 1;
                shiftsAndReservations[i] = new List<Tuple<Shift, List<Reservation>, List<Lodging>>>();

                for (int j = 0; j < Shifts.Count; j++)
                {
                    shiftsAndReservations[i].Add(
                        new Tuple<Shift, List<Reservation>, List<Lodging>>(
                            Shifts[j],
                            dataContext.Reservations.Where(r => r.RoomId == roomId && r.Date >= Shifts[j].InitialDate && r.Date < Shifts[j].FinalDate).ToList(),
                            dataContext.Lodgings.Include(l => l.Customer).Where(l => l.RoomId == roomId && l.ActualIDate >= Shifts[j].InitialDate && l.ActualIDate < Shifts[j].FinalDate && !l.Active).OrderBy(l => l.ActualIDate).ToList()
                            )
                        );
                }
                //De 8am a 9am y de 9pm a 10pm hay espacios entre los turnos que el for anterior no tiene en cuenta, por eso se chequean directamente aquí

                var r8_9 = dataContext.Reservations.Where(r => r.RoomId == roomId && r.Date >= h8_am && r.Date < h9_am).ToList();
                if (r8_9 != null)
                    shiftsAndReservations[i][0] = new Tuple<Shift, List<Reservation>, List<Lodging>>(shiftsAndReservations[i][0].Item1, shiftsAndReservations[i][0].Item2.Concat(r8_9).ToList(), shiftsAndReservations[i][0].Item3);

                var l8_9 = dataContext.Lodgings.Include(l => l.Customer).Where(l => l.RoomId == roomId && l.ActualIDate >= h8_am && l.ActualIDate < h9_am && !l.Active).OrderBy(l => l.ActualIDate).ToList();
                if (l8_9 != null)
                    shiftsAndReservations[i][0] = new Tuple<Shift, List<Reservation>, List<Lodging>>(shiftsAndReservations[i][0].Item1, shiftsAndReservations[i][0].Item2, shiftsAndReservations[i][0].Item3.Concat(l8_9).ToList());

                var r9_10 = dataContext.Reservations.Where(r => r.RoomId == roomId && r.Date >= h9_pm && r.Date < h10_pm).ToList();
                if (r9_10 != null)
                    shiftsAndReservations[i][3] = new Tuple<Shift, List<Reservation>, List<Lodging>>(shiftsAndReservations[i][3].Item1, shiftsAndReservations[i][3].Item2.Concat(r9_10).ToList(), shiftsAndReservations[i][3].Item3);

                var l9_10 = dataContext.Lodgings.Include(l => l.Customer).Where(l => l.RoomId == roomId && l.ActualIDate >= h9_pm && l.ActualIDate < h10_pm && !l.Active).OrderBy(l => l.ActualIDate).ToList();
                if (l9_10 != null)
                    shiftsAndReservations[i][3] = new Tuple<Shift, List<Reservation>, List<Lodging>>(shiftsAndReservations[i][3].Item1, shiftsAndReservations[i][3].Item2,shiftsAndReservations[i][3].Item3.Concat(l9_10).ToList());
            }

            var mainPageViewModel = new MainPageViewModel
            {
                ShiftAndReservationsRoom1 = shiftsAndReservations[0],
                ShiftAndReservationsRoom2 = shiftsAndReservations[1],
                ShiftAndReservationsRoom3 = shiftsAndReservations[2],
            };

            return View(mainPageViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public IActionResult Presentation()
        {
            return View();
        }
    }
}

//    ShiftAndReservationsRoom1 = new List<Tuple<Shift, List<Reservation>>>
//                {
//                    new Tuple<Shift, List<Reservation>>(Shifts[0], dataContext.Reservations.Where(r => r.RoomId == 1  && r.Date >= Shifts[0].InitialDate && r.Date<Shifts[0].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[1], dataContext.Reservations.Where(r => r.RoomId == 1  && r.Date >= Shifts[1].InitialDate && r.Date<Shifts[1].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[2], dataContext.Reservations.Where(r => r.RoomId == 1  && r.Date >= Shifts[2].InitialDate && r.Date<Shifts[2].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[3], dataContext.Reservations.Where(r => r.RoomId == 1  && r.Date >= Shifts[3].InitialDate && r.Date<Shifts[3].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[4], dataContext.Reservations.Where(r => r.RoomId == 1  && r.Date >= Shifts[4].InitialDate && r.Date<Shifts[4].FinalDate).ToList()),
//                },
//                 ShiftAndReservationsRoom2 = new List<Tuple<Shift, List<Reservation>>>
//                {
//                    new Tuple<Shift, List<Reservation>>(Shifts[0], dataContext.Reservations.Where(r => r.RoomId == 2 && r.Date >= Shifts[0].InitialDate && r.Date<Shifts[0].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[1], dataContext.Reservations.Where(r => r.RoomId == 2 && r.Date >= Shifts[1].InitialDate && r.Date<Shifts[1].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[2], dataContext.Reservations.Where(r => r.RoomId == 2 && r.Date >= Shifts[2].InitialDate && r.Date<Shifts[2].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[3], dataContext.Reservations.Where(r => r.RoomId == 2 && r.Date >= Shifts[3].InitialDate && r.Date<Shifts[3].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[4], dataContext.Reservations.Where(r => r.RoomId == 2 && r.Date >= Shifts[4].InitialDate && r.Date<Shifts[4].FinalDate).ToList()),
//                },
//                ShiftAndReservationsRoom3 = new List<Tuple<Shift, List<Reservation>>>
//                {
//                    new Tuple<Shift, List<Reservation>>(Shifts[0], dataContext.Reservations.Where(r => r.RoomId == 3 && r.Date >= Shifts[0].InitialDate && r.Date<Shifts[0].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[1], dataContext.Reservations.Where(r => r.RoomId == 3 && r.Date >= Shifts[1].InitialDate && r.Date<Shifts[1].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[2], dataContext.Reservations.Where(r => r.RoomId == 3 && r.Date >= Shifts[2].InitialDate && r.Date<Shifts[2].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[3], dataContext.Reservations.Where(r => r.RoomId == 3 && r.Date >= Shifts[3].InitialDate && r.Date<Shifts[3].FinalDate).ToList()),
//                    new Tuple<Shift, List<Reservation>>(Shifts[4], dataContext.Reservations.Where(r => r.RoomId == 3 && r.Date >= Shifts[4].InitialDate && r.Date<Shifts[4].FinalDate).ToList()),
//                }
//}
