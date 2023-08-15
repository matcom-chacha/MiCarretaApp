using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class RoomReplaceViewModel
    {
        [Display(Name ="Desde")]
        public int SourceId { get; set; }

        [Display(Name = "Hacia")]
        public int DestinationId { get; set; }

        public List<NamedRoom> Rooms { get; set; }
    }
}
