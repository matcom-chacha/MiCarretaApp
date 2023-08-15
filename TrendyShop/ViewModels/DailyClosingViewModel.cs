using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class DailyClosingViewModel
    {
        public DateTime Date { get; set; }
        public DateTime PreviousClosingDate { get; set; }
        public long DateTicks { get; set; }
        public long PreviousClosingTicks { get; set; }
        public IList<DailyClosing> TodaysClosings { get; set; }
        public IList<float> PreviousClosingNumbers { get; set; }
        public IList<MissingRecord> MissingProducts { get; set; }
        public IList<MissingRecord> MissingProductsToReport { get; set; }
        public IList<RepositionRecord> Repositions { get; set; }
        public MissingRecord MissingProduct { get; set; }
        public RepositionRecord RepositionRecord { get; set; }
        public IList<GastronomicProduct> Products { get; set; }
        public Employee Employee { get; set; }
    }
}
