using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class LodgingIncidence
    {
        [Key, Column(Order = 0)]
        public int RoomId { get; set; }

        [Key, Column(Order = 1)]
        public DateTime Date { get; set; }

        [ForeignKey("RoomId,Date")]
        public Lodging Lodging { get; set; }


        [Key, Column(Order = 2)]
        public int IncidenceId { get; set; }

        [ForeignKey("IncidenceId")]
        public Incidence Incidence { get; set; }
    }
}
