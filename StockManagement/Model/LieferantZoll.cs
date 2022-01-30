using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model
{

    [Table("KTsmn_LieferantZollAntiDKosten")]
    public class LieferantZoll
    {

        [Key]
        public int LZ_ID { get; set; }
        public string Lieferant { get; set; }
        public decimal Zoll { get; set; }
        public decimal AntiDumping { get; set; }
        public decimal? SeeFracht { get; set; }
        public string Currency { get; set; }
    }
}
