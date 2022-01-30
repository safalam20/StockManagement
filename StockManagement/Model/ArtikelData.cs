using Microsoft.EntityFrameworkCore;
using StockManagement.Model.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Model
{
    [Keyless]
    public class ArtikelData
    {
        public string Artikelnummer { get; set; }
        public string Artikelgruppe { get; set; }
        public string USER_LZ { get; set; }
        public string USER_LK1 { get; set; }
        public string USER_LK2 { get; set; }
        public string USER_ET { get; set; }
        public string USER_Farbekurz { get; set; }
        public string USER_Farbelang { get; set; }
        public string USER_EPRENr { get; set; }

        public string Lieferant { get; set; }
        public string USER_Produktionsgruppe { get; set; }
        public int? USER_ContainerStueck { get; set; }
        public string USER_Radius { get; set; }
        public string USER_NB { get; set; }
        public string USER_Breite { get; set; }
        public string USER_Application { get; set; }
        public string USER_txLZLK { get; set; }
        public string USER_ArtikelName { get; set; }
        public string Matchcode { get; set; }
        public string USER_ArtikelTyp { get; set; }
        public decimal? T24Price { get; set; }
        public decimal? ContainerDE { get; set; }
        public decimal? Bestand { get; set; }
        public decimal? MaxEK { get; set; }
        public decimal? ConWar0 { get; set; }
        public decimal? ConWarBetrag0 { get; set; }
        public decimal? ConWarTS0 { get; set; }
        public decimal? ConWarTSBetrag0 { get; set; }
        public decimal? ConWar1 { get; set; }
        public decimal? ConWarBetrag1 { get; set; }
        public decimal? ConWarTS1 { get; set; }
        public decimal? ConWarTSBetrag1 { get; set; }
        public decimal? ConWar2 { get; set; }
        public decimal? ConWarBetrag2 { get; set; }
        public decimal? ConWarTS2 { get; set; }
        public decimal? ConWarTSBetrag2 { get; set; }



        [NotMapped]
        public double ExportWarenEingangRate
        {
            get
            {
                double SaleSum = (double)((Sum0 ?? 0) + (Sum1 ?? 0));
                double TsSum = (double)((ConWarTS0 ?? 0) + (ConWarTS1 ?? 0));

                if(SaleSum != 0)
                {
                    return TsSum / SaleSum;
                }
                else { return 0; }

            }
            set { }
        }

        public decimal? Betrag0 { get; set; }
        public decimal? Betrag1 { get; set; }
        public decimal? Betrag2 { get; set; }
        public decimal? Gewicht { get; set; }       
        public decimal? Month1_0 { get; set; }
        public decimal? Month2_0 { get; set; }
        public decimal? Month3_0 { get; set; }
        public decimal? Month4_0 { get; set; }
        public decimal? Month5_0 { get; set; }
        public decimal? Month6_0 { get; set; }
        public decimal? Month7_0 { get; set; }
        public decimal? Month8_0 { get; set; }
        public decimal? Month9_0 { get; set; }
        public decimal? Month10_0 { get; set; }
        public decimal? Month11_0 { get; set; }
        public decimal? Month12_0 { get; set; }
        public decimal? Month1_1 { get; set; }
        public decimal? Month2_1 { get; set; }
        public decimal? Month3_1 { get; set; }
        public decimal? Month4_1 { get; set; }
        public decimal? Month5_1 { get; set; }
        public decimal? Month6_1 { get; set; }
        public decimal? Month7_1 { get; set; }
        public decimal? Month8_1 { get; set; }
        public decimal? Month9_1 { get; set; }
        public decimal? Month10_1 { get; set; }
        public decimal? Month11_1 { get; set; }
        public decimal? Month12_1 { get; set; }
        public decimal? Month1_2 { get; set; }
        public decimal? Month2_2 { get; set; }
        public decimal? Month3_2 { get; set; }
        public decimal? Month4_2 { get; set; }
        public decimal? Month5_2 { get; set; }
        public decimal? Month6_2 { get; set; }
        public decimal? Month7_2 { get; set; }
        public decimal? Month8_2 { get; set; }
        public decimal? Month9_2 { get; set; }
        public decimal? Month10_2 { get; set; }
        public decimal? Month11_2 { get; set; }
        public decimal? Month12_2 { get; set; }




        [NotMapped]
        public decimal? Sum0
        {
            get
            {
                return Month1_0
    + Month2_0
    + Month3_0
    + Month4_0
    + Month5_0
    + Month6_0
    + Month7_0
    + Month8_0
    + Month9_0
    + Month10_0
    + Month11_0
    + Month12_0;
            }
            set { }
        }

        [NotMapped]
        public decimal? Sum1
        {
            get
            {
                return Month1_1
                 + Month2_1
                 + Month3_1
                 + Month4_1
                 + Month5_1
                 + Month6_1
                 + Month7_1
                 + Month8_1
                 + Month9_1
                 + Month10_1
                 + Month11_1
                 + Month12_1;

            }
            set { }
        }

        [NotMapped]
        public decimal? Sum2
        {
            get
            {
                return Month1_2
 + Month2_2
 + Month3_2
 + Month4_2
 + Month5_2
 + Month6_2
 + Month7_2
 + Month8_2
 + Month9_2
 + Month10_2
 + Month11_2
 + Month12_2;

            }
            set { }
        }


        [NotMapped]
        public decimal StockPercent
        {
            get
            {
               decimal maxSum= Math.Max((Sum0 ?? 0),(Sum1 ?? 0));
                if (maxSum != 0)
                {
                    return (Bestand ?? 0) / maxSum * 100;
                }
                else
                {
                    return 0;
                }
            }
            set { }
        }

        [NotMapped]
        public string _Size
        {
            get
            {
                return USER_Breite + " " + USER_NB + " " + USER_Radius;
            }
            set { }
        }
        
       

        [NotMapped]
        public decimal[] List3Months
        {
            get
            {
                return new decimal[]
                {
                    Month1_2 ?? 0,
Month2_2 ?? 0,
Month3_2 ?? 0,
Month4_2 ?? 0,
Month5_2 ?? 0,
Month6_2 ?? 0,
Month7_2 ?? 0,
Month8_2 ?? 0,
Month9_2 ?? 0,
Month10_2 ?? 0,
Month11_2 ?? 0,
Month12_2 ?? 0,
Month1_1 ?? 0,
Month2_1 ?? 0,
Month3_1 ?? 0,
Month4_1 ?? 0,
Month5_1 ?? 0,
Month6_1 ?? 0,
Month7_1 ?? 0,
Month8_1 ?? 0,
Month9_1 ?? 0,
Month10_1 ?? 0,
Month11_1 ?? 0,
Month12_1 ?? 0,
Month1_0 ?? 0,
Month2_0 ?? 0,
Month3_0 ?? 0,
Month4_0 ?? 0,
Month5_0 ?? 0,
Month6_0 ?? 0,
Month7_0 ?? 0,
Month8_0 ?? 0,
Month9_0 ?? 0,
Month10_0 ?? 0,
Month11_0 ?? 0,
Month12_0 ?? 0

                };
            }
            set { }
        }

        [NotMapped]
        public int[] SaleProjected{ get; set; }
        

        [NotMapped]
        public int[] Max3YearsSales { get; set; }    
        
        [NotMapped]
        public double Last8MonthTendency
        {
            get
            {
                double total0 = 0;
                double total1 = 0;
                int currentMonth = Int32.Parse(DateTime.Now.ToString("MM"));

                for (int i = 0; i < 8; i++)
                {
                    double month0 = (double)List3Months[22 + currentMonth - i];
                    double month1 = (double)List3Months[10 + currentMonth - i];

                    if (month0 > 0 && 
                        month1 > 0 &&
                       ((month0/month1) > 0.2 && (month0 / month1)<5))
                    {

                        total0 = total0 + month0;
                        total1 = total1 + month1;
                    }
                }

                if (total1 != 0)
                {                    
                    return (total0 / total1);
                }
                else
                {
                    return 0;
                }
            }
            set { }
        }

        [NotMapped]
        public double Last8MonthTendencytoShow
        {
            get
            {
                double total0 = 0;
                double total1 = 0;
                int currentMonth = Int32.Parse(DateTime.Now.ToString("MM"));

                for (int i = 0; i < 8; i++)
                {
                    double month0 = (double)List3Months[22 + currentMonth - i];
                    double month1 = (double)List3Months[10 + currentMonth - i];

                   
                    total0 = total0 + month0;
                    total1 = total1 + month1;
                    
                }

                if (total1 != 0)
                {
                    return (total0 / total1);
                }
                else
                {
                    return 0;
                }
            }
            set { }
        }


        [NotMapped]
        public string _ArtikelTyp
        {
            get
            {
                string result = USER_ArtikelTyp.Substring(USER_ArtikelTyp.Length - 2) ?? null;
                return result;
            }
            set { }
        }

        [NotMapped]
        public int? TransitStock { get; set; }


        [NotMapped]
        public int? BackOrder { get; set; }


        [NotMapped]
        public int? NewProductionOffer { get; set; }

        [NotMapped]
        public int? ProjectedSaleUntilContainer { get; set; }

        [NotMapped]
        public int? ProjectedSaleUntilTarget { get; set; }

        [NotMapped]
        public decimal? RealEK { get; set; }

        [NotMapped]
        public int? FactoryStock { get; set; }


        [NotMapped]
        public int? TempNewOrder { get; set; }

        [NotMapped]
        public List<ArtikelLieferantDTO> Lieferants { get; set; }

        [NotMapped]
        public int SelectedMonthSale { get; set; }

        [NotMapped]
        public decimal? ArtikelCost { get; set; }

        [NotMapped]
        public  decimal? ArtikelProfit { get; set; }

        [NotMapped]
        public decimal? ArtikelFracht { get; set; }


        [NotMapped]
        public double ContainerDeCostRate
        {
            get
            {
               if(ArtikelCost!=null && ArtikelCost!=0 && ContainerDE!=null && ContainerDE != 0)
                {
                    return (double)ArtikelCost-(double)ContainerDE;
                }

                return 0;

            }
            set { }
        }

        [NotMapped]
        public decimal? AverageSeaFracht { get; set; }
        [NotMapped]
        public decimal? AverageZoll { get; set; }
        [NotMapped]
        public decimal? AverageAntiDumping { get; set; }

        [NotMapped]
        public decimal? RealEKAdvanced { get; set; }
    }
}
