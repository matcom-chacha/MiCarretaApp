using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class NewReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public bool RoomOccupied { get;  set; }
        public string InitialDateFormat { get; set; }
        public string FinalDateFormat { get; set; }
    }
}
