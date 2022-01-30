using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model
{
    [Keyless]
    public class WarenEingangData
    {
        public string Belegkennzeichen { get; set; }
        public string Belegart { get; set; }
        public string Referenznummer { get; set; }
        public string Artikelnummer { get; set; }
        public string Artikelgruppe { get; set; }
        public string USER_Produktionsgruppe { get; set; }
        public DateTime? Belegdatum { get; set; }
        public decimal? Menge { get; set; }
        public decimal? Einzelpreis { get; set; }
        public DateTime? USER_ETA { get; set; }
        public DateTime? USER_ETD { get; set; }
        public string Lieferant { get; set; }
        public string USER_Rechnungsnummer { get; set; }
        public string USER_Referenznummer { get; set; }
        public string USER_Spedition { get; set; }
        public string BLNummer { get; set; }
        public int? TransItems {get;set;}

        public string A0Name1 { get; set; }
        public string A0Empfaenger { get; set; }

        [NotMapped]
        public int TotalMenge { get; set; }

        [NotMapped]
        public KHKOpUbersicht KHKOpUbersicht { get; set; }

    }
}
