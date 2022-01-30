using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model.Dtos
{
    public class InvoiceFormDTO
    {
        public string Artikelnummer { get; set; }
        public int? Menge { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }
        public decimal? Amount{
            get
            {
                if(Price!=null && Menge != null)
                {
                    return Price * Menge;
                }
                else
                {
                    return null;
                }
            }
            set { }
        }
    }
}
