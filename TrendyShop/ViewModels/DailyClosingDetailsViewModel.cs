using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class DailyClosingDetailsViewModel
    {
        public DateTime Date { get; set; }
        public IList<DailyClosing> DailyClosingRows { get; set; }
        public IList<RepositionRecord> Repositions { get; set; }
        public IList<MissingRecord> Missings { get; set; }
        public IList<MissingRecord> MissingsOfTheDay { get; set; }
        public Employee Employee { get; set; }
        public bool Approved { get; set; }
    }
}
