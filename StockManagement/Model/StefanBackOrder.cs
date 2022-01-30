using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model
{
    [Keyless]
    public class StefanBackOrder
    {
        public string Matchcode { get; set; }
        public string Lieferant { get; set; }
        //public int? Jahr { get; set; }
        public int? Belegnummer { get; set; }
        public string BestellDatum { get; set; }
        public int VorID { get; set; }
        public string BestellungReferenz { get; set; }
        public string Artikelnummer { get; set; }
        public string USER_Produktionsgruppe { get; set; }

        public decimal? BackOrder { get; set; }
        public decimal? TotalOrder { get; set; }
        public decimal? TotalDelivered { get; set; }
        public decimal? MaxPriceInBestellung { get; set; }

    }
}
