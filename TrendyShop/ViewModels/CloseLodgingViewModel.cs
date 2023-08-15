using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class CloseLodgingViewModel
    {
        public Lodging Lodging { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public IList<PurchasePerLodging> Purchase { get; set; }//now ilist

        public Incidence SelectedIncidence { get; set; }//fuera
        public bool OldIncidence { get; set; }//fuera

        [Display(Name = "Incidencias")]
        public IList<Incidence> Incidences { get; set; }

        public IList<Incidence> IncidencesToBind { get; set; }//cambio a ILSist

        public float? TotalPrice { get; set; }//2:9
        public int GreatestId { get; set; }

        public string DateFormat { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
