using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model
{
    [Table("KTsmn_TempProduktionPlan")]
    public class ProductionOffer
    {
        [Key]
        public int OfferID { get; set; }
        public string Artikelnummer { get; set; }
        public int? Menge { get; set; }
        public string Artikelgruppe { get; set; }
    }
}
