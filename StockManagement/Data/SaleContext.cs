using Microsoft.EntityFrameworkCore;
using StockManagement.Model;
using StockManagement.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Data
{
    public class SaleContext :DbContext
    {
        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
        {

        }

        public virtual DbSet<ArtikelData> ArtikelDatas { get; set; }

        public virtual DbSet<CostVariables> _VariablesCost { get; set; }

        public virtual DbSet<LieferantZoll> _LieferantZoll { get; set; }
        
        public virtual DbSet<WarenEingangData> _WarenEingangDatas { get; set; }
        public virtual DbSet<KTEnumerator> KTEnumerators { get; set; }
        public virtual DbSet<StefanBackOrder> _BackOrders { get; set; }

        public virtual DbSet<ArtikelLieferantDTO> _ArtikelLieferants { get; set; }
        public virtual DbSet<ProductionOffer> _ProductionOffer { get; set; }
        public virtual DbSet<BestellungPositionPrice> _EinkaufReportBelegPosition { get; set; }
        public virtual DbSet<KHKOpUbersicht> _KHKOpUbersichts { get; set; }
       
        }
}
