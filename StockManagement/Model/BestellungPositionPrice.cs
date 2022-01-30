using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model
{
    [Keyless]
    public class BestellungPositionPrice
    {
        public string Matchcode { get; set; }
        public string Lieferant { get; set; }

        public int Belegnummer { get; set; }       
        public string BestellDatum { get; set; }
        public int VorID { get; set; }
        public string Artikelnummer { get; set; }
        public decimal TotalOrder { get; set; }
        public decimal Einzelpreis { get; set; }

        [NotMapped]
        public int BackOrder { get; set; }
    }
}
