using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class EditReservationViewModel
    {
        public Reservation Reservation { get; set; }

        public IEnumerable<Room> Rooms { get; set; }

        public int OldRoomId { get; set; }
        public DateTime OldDate { get; set; }
    }
}
