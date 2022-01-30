using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model
{
    [Table("KTsmn_VertriebKosten")]
    public class CostVariables
    {
        [Key]
     public int VK_ID { get; set; }
    public string User_ArtikelGruppe {get;set;}
    public decimal SeeFracht { get; set; }
    public decimal Demurage { get; set; }
    public decimal USDEUR { get; set; }
    public decimal Nachlauf { get; set; }
    public int Container16 { get; set; }
    public int Container17 { get; set; }
    public int Container18 { get; set; }
    public int Container19 { get; set; }
    public int Container20 { get; set; }
    public int Container21 { get; set; }
    public int Container22 { get; set; }
    public decimal VKLagerFT13 { get; set; }
    public decimal VKLagerFT14 { get; set; }
    public decimal VKLagerFT15 { get; set; }
    public decimal VKLagerFT16 { get; set; }
    public decimal VKLagerFT17 { get; set; }
    public decimal VKLagerFT18 { get; set; }
    public decimal VKLagerFT19 { get; set; }
    public decimal VKLagerFT20 { get; set; }
    public decimal VKLagerFT21 { get; set; }
    public decimal VKLagerFT22 { get; set; }
    public decimal LagerGoInOut13 { get; set; }
    public decimal LagerGoInOut14 { get; set; }
    public decimal LagerGoInOut15 { get; set; }
    public decimal LagerGoInOut16 { get; set; }
    public decimal LagerGoInOut17 { get; set; }
    public decimal LagerGoInOut18 { get; set; }
    public decimal LagerGoInOut19 { get; set; }
    public decimal LagerGoInOut20 { get; set; }
    public decimal LagerGoInOut21 { get; set; }
    public decimal LagerGoInOut22 { get; set; }
    public decimal MouldInvestment { get; set; }
    public decimal TuvAbeZer { get; set; }
    public decimal Deckel { get; set; }
    public decimal ZubehorPaket { get; set; }
    public decimal VertriebKosten { get; set; }
    public decimal Finanzierung { get; set; }
    public decimal Wrapping { get; set; }
    public decimal VerVerpMat { get; set; }
    public decimal Reklamation { get; set; }
    public decimal LCBankKosten { get; set; }
    public decimal PaketFracht { get; set; }
    public decimal PortalGebuhrPer { get; set; }
    public decimal PortalGebuhrExtra { get; set; }

    public decimal? Profit12 { get; set; }
        public decimal? Profit13 { get; set; }
        public decimal? Profit14 { get; set; }
        public decimal? Profit15 { get; set; }
        public decimal? Profit16 { get; set; }
        public decimal? Profit17 { get; set; }
        public decimal? Profit18 { get; set; }
        public decimal? Profit19 { get; set; }
        public decimal? Profit20 { get; set; }
        public decimal? Profit21 { get; set; }
        public decimal? Profit22 { get; set; }
        public decimal? ProfitPer12 { get; set; }
        public decimal? ProfitPer13 { get; set; }
        public decimal? ProfitPer14 { get; set; }
        public decimal? ProfitPer15 { get; set; }
        public decimal? ProfitPer16 { get; set; }
        public decimal? ProfitPer17 { get; set; }
        public decimal? ProfitPer18 { get; set; }
        public decimal? ProfitPer19 { get; set; }
        public decimal? ProfitPer20 { get; set; }
        public decimal? ProfitPer21 { get; set; }
        public decimal? ProfitPer22 { get; set; }


    }
}
