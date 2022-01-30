using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model.Dtos
{
    public class FilterDTO
    {
        public string Pattern { get; set; }
        public string Size { get; set; }
        public string MonthNumber { get; set; }
        public string BeginMonth { get; set; }
        public bool isChart { get; set; }

        public string Zoll { get; set; }
        public string Breite { get; set; }
        public string LK1 { get; set; }
        public string LK2 { get; set; }
        public string ET { get; set; }
        public string Farbekurz { get; set; }
        public string Lieferant { get; set; }

    }
}
