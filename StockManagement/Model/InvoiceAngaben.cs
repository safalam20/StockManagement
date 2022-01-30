using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model
{
    public class InvoiceAngaben
    {
        public string InvoiceNo { get; set; }
        public string ReferenceNo { get; set; }
        public string BLno { get; set; }
        public string Lieferant { get; set; }
        public string VorgangID { get; set; }
    }
}
