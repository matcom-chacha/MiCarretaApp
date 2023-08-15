using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class MainPageViewModel
    {
        public List<Shift> Shifts { get; set; }
        public List<Lodging> CurrentLodgins { get; set; }
        public List<Tuple<Shift, List<Reservation>, List<Lodging>>> ShiftAndReservationsRoom1{ get; set; }
        public List<Tuple<Shift, List<Reservation>, List<Lodging>>> ShiftAndReservationsRoom2{ get; set; }
        public List<Tuple<Shift, List<Reservation>, List<Lodging>>> ShiftAndReservationsRoom3{ get; set; }
    }
}
