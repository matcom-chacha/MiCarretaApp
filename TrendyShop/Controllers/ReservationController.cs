using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrendyShop.Models;
using TrendyShop.ViewModels;
using TrendyShop.Data;
using Microsoft.AspNetCore.Authorization;

namespace TrendyShop.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private DataContext dataContext;
        public ReservationController(DataContext ctx)
        {
            dataContext = ctx;
        }
        
        public IActionResult Index()
        {
            //var reservations = dataContext.Reservations.
            //    Where(r => r.Date.Year >= DateTime.Today.Year && r.Date.Month >= DateTime.Today.Month)
            //    .ToList();
            //reservations.Sort((r1, r2) => r1.Date.CompareTo(r2.Date));
            var reservations = dataContext.Reservations.
               Where(r => r.Date.Year >= DateTime.Today.Year && r.Date.Month >= DateTime.Today.Month)
               .ToList();
            reservations.Sort((r1, r2) => r1.Date.CompareTo(r2.Date));
            return View(reservations);

        }

        public IActionResult Details(int roomId, DateTime _date)
        {
            Reservation reservation = dataContext.Reservations.Find(roomId, _date);

            return View(reservation);
        }

        public IActionResult Confirm(int roomId, DateTime _date)
        {
            Reservation reservation = dataContext.Reservations.Find(roomId, _date);
            reservation.IsConfirmed = true;
           
            dataContext.SaveChanges();

            return View("Details", reservation);
        }
        public IActionResult CancelConfirm(int roomId, DateTime _date)
        {
            Reservation reservation = dataContext.Reservations.Find(roomId, _date);
            reservation.IsConfirmed = false;
            
            dataContext.SaveChanges();

            return View("Details", reservation);
        }
        public IActionResult NewReservation(DateTime initialDate, DateTime finalDate, int roomId = 1)
        {
            if (initialDate == default)
                initialDate = DateTime.Now;
            if (finalDate == default)
                finalDate = DateTime.Now.AddHours(3);

            NewReservationViewModel nrvm = new NewReservationViewModel
            {
                Reservation = new Reservation
                {
                    Date = initialDate,
                    FinalDate = finalDate,
                    RoomId = roomId
                },
                InitialDate = initialDate,
                FinalDate = finalDate,
                Rooms = dataContext.Rooms.ToList(),
                InitialDateFormat = initialDate.ToString("yyyy-MM-dd") + "T" + initialDate.ToString("HH:mm"),
                FinalDateFormat = finalDate.ToString("yyyy-MM-dd") + "T" + finalDate.ToString("HH:mm")
            };
            return View(nrvm);
        }

        [HttpPost]
        public IActionResult InsertNewReservation(NewReservationViewModel nrvm)
        {
            var occupied = RoomOcuppied(nrvm.Reservation.RoomId, nrvm.InitialDate, nrvm.FinalDate);
            if (!ModelState.IsValid || occupied)
            {
                NewReservationViewModel newrvm = new NewReservationViewModel
                {
                    Reservation = new Reservation
                    {
                        Date = nrvm.Reservation.Date,
                        FinalDate = nrvm.Reservation.FinalDate
                    },
                    Rooms = dataContext.Rooms.ToList(),
                    InitialDateFormat = nrvm.InitialDate.ToString("yyyy-MM-dd") + "T" + nrvm.InitialDate.ToString("HH:mm"),
                    FinalDateFormat = nrvm.FinalDate.ToString("yyyy-MM-dd") + "T" + nrvm.FinalDate.ToString("HH:mm")
                };

                if (occupied)
                    newrvm.RoomOccupied = true;

                return View("NewReservation", newrvm);
            }

            nrvm.Reservation.Date = nrvm.InitialDate;
            nrvm.Reservation.FinalDate = nrvm.FinalDate;
            dataContext.Reservations.Add(nrvm.Reservation);
            dataContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private bool RoomOcuppied(int roomId, DateTime date, DateTime finalDate )
        {
            var reservations = dataContext.Reservations.Where(r => r.RoomId == roomId).ToList();

            foreach (var item in reservations)
            {
                if ((date < item.FinalDate && item.FinalDate <= finalDate) ||
                    (item.Date < finalDate && finalDate <= item.FinalDate))
                {
                    return true;
                }
            }

            return false;
        }


        public IActionResult Delete(int roomId, DateTime _date)
        {
            Reservation reservation = dataContext.Reservations.Find(roomId, _date);
            dataContext.Reservations.Remove(reservation);
            dataContext.SaveChanges();

            return RedirectToAction("Index", "Reservation");
        }

        public IActionResult Edit(int roomId, DateTime _date)
        {
            EditReservationViewModel ervm = new EditReservationViewModel
            {
                Reservation = dataContext.Reservations.Find(roomId, _date),
                Rooms = dataContext.Rooms.ToList(),
                OldRoomId = roomId,
                OldDate = _date
            };
            return View(ervm);
        }

        [HttpPost]
        public IActionResult SaveEdition(EditReservationViewModel ervm)
        {
            if (!ModelState.IsValid)
            {
                EditReservationViewModel newEditAddViewModel = new EditReservationViewModel
                {
                    Reservation = ervm.Reservation,
                    Rooms = dataContext.Rooms.ToList(),
                    OldDate = ervm.OldDate,
                    OldRoomId = ervm.OldRoomId
                };
                return View("Edit", ervm);
            }

            //.Include?
            var reservationInDataContext = dataContext.Reservations.Find(ervm.OldRoomId, ervm.OldDate);

            if (ervm.OldRoomId == ervm.Reservation.RoomId && ervm.OldDate == ervm.Reservation.Date)
            {
                reservationInDataContext.Customer = ervm.Reservation.Customer;
                reservationInDataContext.FinalDate = ervm.Reservation.FinalDate;
                reservationInDataContext.Phone = ervm.Reservation.Phone;
                //reservationInDataContext.Companion = ervm.Reservation.Companion;//ahora en hospedaje
                reservationInDataContext.Romantic = ervm.Reservation.Romantic;
                //reservationInDataContext.StandardPrice = ervm.Reservation.StandardPrice;//ahora en Lodging
            }
            else
            {
                dataContext.Remove(reservationInDataContext);
                var newReservation = new Reservation
                {
                    RoomId = ervm.Reservation.RoomId,
                    Date = ervm.Reservation.Date,
                    Customer = ervm.Reservation.Customer,
                    Phone = ervm.Reservation.Phone,
                    Romantic = ervm.Reservation.Romantic,
                };
                dataContext.Reservations.Add(newReservation);
            }
            dataContext.SaveChanges();

            return RedirectToAction("Index", "Reservation");//pordriamos redireccionarlo a los detalles de la propia reservacion
        }
    }
}

