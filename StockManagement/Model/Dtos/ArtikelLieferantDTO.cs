using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model.Dtos
{
    [Keyless]
    public class ArtikelLieferantDTO
    {
        public string Artikelnummer { get; set; }
        public string USER_Produktionsgruppe { get; set; }
        public string Lieferant { get; set; }
        public decimal? Einzelpreis { get; set; }
    }
}
