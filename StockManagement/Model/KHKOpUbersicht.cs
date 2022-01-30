using Microsoft.EntityFrameworkCore;

namespace StockManagement.Model
{
    [Keyless]
    public class KHKOpUbersicht
    {
        public string Lieferant { get; set; }
        public string Referenznummer { get; set; }
        public string LF_Rechnungnummer { get; set; }
        public string Container_nr { get; set; }
        public decimal? Betrag { get; set; }
        public decimal? BezahltBetrag { get; set; }
    }
}
