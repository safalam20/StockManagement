using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model
{
    [Table("KTEnumerator")]
    public class KTEnumerator
    {
        [Key]
        public int ID { get; set; }
        public int? Aktiv { get; set; }
        public string Type1 { get; set; }
        public string Enumerator { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
    }
}
