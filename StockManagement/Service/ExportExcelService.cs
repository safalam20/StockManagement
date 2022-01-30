using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using StockManagement.Model;
using StockManagement.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Service
{
    public class ExportExcelService
    {

        public void exportToExcel(List<ArtikelData> Artikels, List<WarenEingangData> Gangs,string Mode, bool isEKAuthenticated)
        {
            
            var newFile = @"wwwroot\files\newbook.core.xlsx";
            if (isEKAuthenticated)
            {
                using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet1 = workbook.CreateSheet("Artikel Data");

                    IRow HeaderRow = sheet1.CreateRow(0);

                    CreateCellString(HeaderRow, 0, "Artikelnummer");
                    CreateCellString(HeaderRow, 1, "USER_Produktionsgruppe");
                    CreateCellString(HeaderRow, 2, "Last8MonthTendency");
                    CreateCellString(HeaderRow, 3, "Last8MonthTendencytoShow");
                    CreateCellString(HeaderRow, 4, "ExportWarenEingangRate");
                    CreateCellString(HeaderRow, 5, "Sum0");
                    CreateCellString(HeaderRow, 6, "Sum1");
                    CreateCellString(HeaderRow, 7, "Sum2");
                    CreateCellString(HeaderRow, 8, "ProjectedSaleUntilContainer");
                    CreateCellString(HeaderRow, 9, "ProjectedSaleUntilTarget");
                    CreateCellString(HeaderRow, 10, "Bestand");
                    CreateCellString(HeaderRow, 11, "TransitStock");
                    CreateCellString(HeaderRow, 12, "BackOrder");
                    CreateCellString(HeaderRow, 13, "FactoryStock");
                    CreateCellString(HeaderRow, 14, "T24Price");
                    CreateCellString(HeaderRow, 15, "MaxEK");
                    CreateCellString(HeaderRow, 16, "Lieferant");
                    CreateCellString(HeaderRow, 17, "USER_ContainerStueck");
                    CreateCellString(HeaderRow, 18, "USER_Radius");
                    CreateCellString(HeaderRow, 19, "USER_NB");
                    CreateCellString(HeaderRow, 20, "USER_Breite");
                    CreateCellString(HeaderRow, 21, "USER_LK1");
                    CreateCellString(HeaderRow, 22, "USER_LK2");
                    CreateCellString(HeaderRow, 23, "USER_ET");
                    CreateCellString(HeaderRow, 24, "USER_Farbekurz");
                    CreateCellString(HeaderRow, 25, "USER_Farbelang");
                    CreateCellString(HeaderRow, 26, "USER_Application");
                    CreateCellString(HeaderRow, 27, "USER_ArtikelName");
                    CreateCellString(HeaderRow, 28, "Matchcode");
                    CreateCellString(HeaderRow, 29, "USER_ArtikelTyp");
                    CreateCellString(HeaderRow, 30, "Gewicht");
                    CreateCellString(HeaderRow, 31, "_ArtikelTyp");
                    CreateCellString(HeaderRow, 32, "Betrag0");
                    CreateCellString(HeaderRow, 33, "Betrag1");
                    CreateCellString(HeaderRow, 34, "Betrag2");
                    CreateCellString(HeaderRow, 35, "ConWar0");
                    CreateCellString(HeaderRow, 36, "ConWarBetrag0");
                    CreateCellString(HeaderRow, 37, "ConWarTS0");
                    CreateCellString(HeaderRow, 38, "ConWarTSBetrag0");
                    CreateCellString(HeaderRow, 39, "ConWar1");
                    CreateCellString(HeaderRow, 40, "ConWarBetrag1");
                    CreateCellString(HeaderRow, 41, "ConWarTS1");
                    CreateCellString(HeaderRow, 42, "ConWarTSBetrag1");
                    CreateCellString(HeaderRow, 43, "ConWar2");
                    CreateCellString(HeaderRow, 44, "ConWarBetrag2");
                    CreateCellString(HeaderRow, 45, "ConWarTS2");
                    CreateCellString(HeaderRow, 46, "ConWarTSBetrag2");
                    CreateCellString(HeaderRow, 47, "Month1_0");
                    CreateCellString(HeaderRow, 48, "Month2_0");
                    CreateCellString(HeaderRow, 49, "Month3_0");
                    CreateCellString(HeaderRow, 50, "Month4_0");
                    CreateCellString(HeaderRow, 51, "Month5_0");
                    CreateCellString(HeaderRow, 52, "Month6_0");
                    CreateCellString(HeaderRow, 53, "Month7_0");
                    CreateCellString(HeaderRow, 54, "Month8_0");
                    CreateCellString(HeaderRow, 55, "Month9_0");
                    CreateCellString(HeaderRow, 56, "Month10_0");
                    CreateCellString(HeaderRow, 57, "Month11_0");
                    CreateCellString(HeaderRow, 58, "Month12_0");
                    CreateCellString(HeaderRow, 59, "Month1_1");
                    CreateCellString(HeaderRow, 60, "Month2_1");
                    CreateCellString(HeaderRow, 61, "Month3_1");
                    CreateCellString(HeaderRow, 62, "Month4_1");
                    CreateCellString(HeaderRow, 63, "Month5_1");
                    CreateCellString(HeaderRow, 64, "Month6_1");
                    CreateCellString(HeaderRow, 65, "Month7_1");
                    CreateCellString(HeaderRow, 66, "Month8_1");
                    CreateCellString(HeaderRow, 67, "Month9_1");
                    CreateCellString(HeaderRow, 68, "Month10_1");
                    CreateCellString(HeaderRow, 69, "Month11_1");
                    CreateCellString(HeaderRow, 70, "Month12_1");
                    CreateCellString(HeaderRow, 71, "Month1_2");
                    CreateCellString(HeaderRow, 72, "Month2_2");
                    CreateCellString(HeaderRow, 73, "Month3_2");
                    CreateCellString(HeaderRow, 74, "Month4_2");
                    CreateCellString(HeaderRow, 75, "Month5_2");
                    CreateCellString(HeaderRow, 76, "Month6_2");
                    CreateCellString(HeaderRow, 77, "Month7_2");
                    CreateCellString(HeaderRow, 78, "Month8_2");
                    CreateCellString(HeaderRow, 79, "Month9_2");
                    CreateCellString(HeaderRow, 80, "Month10_2");
                    CreateCellString(HeaderRow, 81, "Month11_2");
                    CreateCellString(HeaderRow, 82, "Month12_2");
                    CreateCellString(HeaderRow, 83, "Production Offer");
                    CreateCellString(HeaderRow, 84, "New Order");
                    CreateCellString(HeaderRow, 85, "Cost");
                    CreateCellString(HeaderRow, 86, "Profit");
                    CreateCellString(HeaderRow, 87, "Container DE");
                    CreateCellString(HeaderRow, 88, "Fracht");



                    int RowIndex = 1;

                    foreach (ArtikelData p in Artikels)
                    {
                        IRow CurrentRow = sheet1.CreateRow(RowIndex);

                        CreateCellString(CurrentRow, 0, p.Artikelnummer);
                        CreateCellString(CurrentRow, 1, p.USER_Produktionsgruppe);
                        CreateCellNumberPercent(workbook, CurrentRow, 2, (double)(p.Last8MonthTendency));
                        CreateCellNumberPercent(workbook, CurrentRow, 3, (double)(p.Last8MonthTendencytoShow));
                        CreateCellNumberPercent(workbook, CurrentRow, 4, (double)(p.ExportWarenEingangRate));
                        CreateCellNumber(CurrentRow, 5, (double)(p.Sum0 ?? 0));
                        CreateCellNumber(CurrentRow, 6, (double)(p.Sum1 ?? 0));
                        CreateCellNumber(CurrentRow, 7, (double)(p.Sum2 ?? 0));
                        CreateCellNumber(CurrentRow, 8, (double)(p.ProjectedSaleUntilContainer ?? 0));
                        CreateCellNumber(CurrentRow, 9, (double)(p.ProjectedSaleUntilTarget ?? 0));
                        CreateCellNumber(CurrentRow, 10, (double)(p.Bestand ?? 0));
                        CreateCellNumber(CurrentRow, 11, (double)(p.TransitStock ?? 0));
                        CreateCellNumber(CurrentRow, 12, (double)(p.BackOrder ?? 0));
                        CreateCellNumber(CurrentRow, 13, (double)(p.FactoryStock ?? 0));
                        CreateCellNumber(CurrentRow, 14, (double)(p.T24Price ?? 0));
                        CreateCellNumber(CurrentRow, 15, (double)(p.MaxEK ?? 0));
                        CreateCellString(CurrentRow, 16, p.Lieferant);
                        CreateCellNumber(CurrentRow, 17, (double)(p.USER_ContainerStueck ?? 0));
                        CreateCellString(CurrentRow, 18, p.USER_Radius);
                        CreateCellString(CurrentRow, 19, p.USER_NB);
                        CreateCellString(CurrentRow, 20, p.USER_Breite);
                        CreateCellString(CurrentRow, 21, p.USER_LK1);
                        CreateCellString(CurrentRow, 22, p.USER_LK2);
                        CreateCellString(CurrentRow, 23, p.USER_ET);
                        CreateCellString(CurrentRow, 24, p.USER_Farbekurz);
                        CreateCellString(CurrentRow, 25, p.USER_Farbelang);
                        CreateCellString(CurrentRow, 26, p.USER_Application);
                        CreateCellString(CurrentRow, 27, p.USER_ArtikelName);
                        CreateCellString(CurrentRow, 28, p.Matchcode);
                        CreateCellString(CurrentRow, 29, p.USER_ArtikelTyp);
                        CreateCellNumber(CurrentRow, 30, (double)(p.Gewicht ?? 0));
                        CreateCellString(CurrentRow, 31, p._ArtikelTyp);
                        CreateCellNumber(CurrentRow, 32, (double)(p.Betrag0 ?? 0));
                        CreateCellNumber(CurrentRow, 33, (double)(p.Betrag1 ?? 0));
                        CreateCellNumber(CurrentRow, 34, (double)(p.Betrag2 ?? 0));
                        CreateCellNumber(CurrentRow, 35, (double)(p.ConWar0 ?? 0));
                        CreateCellNumber(CurrentRow, 36, (double)(p.ConWarBetrag0 ?? 0));
                        CreateCellNumber(CurrentRow, 37, (double)(p.ConWarTS0 ?? 0));
                        CreateCellNumber(CurrentRow, 38, (double)(p.ConWarTSBetrag0 ?? 0));
                        CreateCellNumber(CurrentRow, 39, (double)(p.ConWar1 ?? 0));
                        CreateCellNumber(CurrentRow, 40, (double)(p.ConWarBetrag1 ?? 0));
                        CreateCellNumber(CurrentRow, 41, (double)(p.ConWarTS1 ?? 0));
                        CreateCellNumber(CurrentRow, 42, (double)(p.ConWarTSBetrag1 ?? 0));
                        CreateCellNumber(CurrentRow, 43, (double)(p.ConWar2 ?? 0));
                        CreateCellNumber(CurrentRow, 44, (double)(p.ConWarBetrag2 ?? 0));
                        CreateCellNumber(CurrentRow, 45, (double)(p.ConWarTS2 ?? 0));
                        CreateCellNumber(CurrentRow, 46, (double)(p.ConWarTSBetrag2 ?? 0));
                        CreateCellNumber(CurrentRow, 47, (double)(p.Month1_0 ?? 0));
                        CreateCellNumber(CurrentRow, 48, (double)(p.Month2_0 ?? 0));
                        CreateCellNumber(CurrentRow, 49, (double)(p.Month3_0 ?? 0));
                        CreateCellNumber(CurrentRow, 50, (double)(p.Month4_0 ?? 0));
                        CreateCellNumber(CurrentRow, 51, (double)(p.Month5_0 ?? 0));
                        CreateCellNumber(CurrentRow, 52, (double)(p.Month6_0 ?? 0));
                        CreateCellNumber(CurrentRow, 53, (double)(p.Month7_0 ?? 0));
                        CreateCellNumber(CurrentRow, 54, (double)(p.Month8_0 ?? 0));
                        CreateCellNumber(CurrentRow, 55, (double)(p.Month9_0 ?? 0));
                        CreateCellNumber(CurrentRow, 56, (double)(p.Month10_0 ?? 0));
                        CreateCellNumber(CurrentRow, 57, (double)(p.Month11_0 ?? 0));
                        CreateCellNumber(CurrentRow, 58, (double)(p.Month12_0 ?? 0));
                        CreateCellNumber(CurrentRow, 59, (double)(p.Month1_1 ?? 0));
                        CreateCellNumber(CurrentRow, 60, (double)(p.Month2_1 ?? 0));
                        CreateCellNumber(CurrentRow, 61, (double)(p.Month3_1 ?? 0));
                        CreateCellNumber(CurrentRow, 62, (double)(p.Month4_1 ?? 0));
                        CreateCellNumber(CurrentRow, 63, (double)(p.Month5_1 ?? 0));
                        CreateCellNumber(CurrentRow, 64, (double)(p.Month6_1 ?? 0));
                        CreateCellNumber(CurrentRow, 65, (double)(p.Month7_1 ?? 0));
                        CreateCellNumber(CurrentRow, 66, (double)(p.Month8_1 ?? 0));
                        CreateCellNumber(CurrentRow, 67, (double)(p.Month9_1 ?? 0));
                        CreateCellNumber(CurrentRow, 68, (double)(p.Month10_1 ?? 0));
                        CreateCellNumber(CurrentRow, 69, (double)(p.Month11_1 ?? 0));
                        CreateCellNumber(CurrentRow, 70, (double)(p.Month12_1 ?? 0));
                        CreateCellNumber(CurrentRow, 71, (double)(p.Month1_2 ?? 0));
                        CreateCellNumber(CurrentRow, 72, (double)(p.Month2_2 ?? 0));
                        CreateCellNumber(CurrentRow, 73, (double)(p.Month3_2 ?? 0));
                        CreateCellNumber(CurrentRow, 74, (double)(p.Month4_2 ?? 0));
                        CreateCellNumber(CurrentRow, 75, (double)(p.Month5_2 ?? 0));
                        CreateCellNumber(CurrentRow, 76, (double)(p.Month6_2 ?? 0));
                        CreateCellNumber(CurrentRow, 77, (double)(p.Month7_2 ?? 0));
                        CreateCellNumber(CurrentRow, 78, (double)(p.Month8_2 ?? 0));
                        CreateCellNumber(CurrentRow, 79, (double)(p.Month9_2 ?? 0));
                        CreateCellNumber(CurrentRow, 80, (double)(p.Month10_2 ?? 0));
                        CreateCellNumber(CurrentRow, 81, (double)(p.Month11_2 ?? 0));
                        CreateCellNumber(CurrentRow, 82, (double)(p.Month12_2 ?? 0));
                        CreateCellNumber(CurrentRow, 83, (double)(p.NewProductionOffer ?? 0));
                        CreateCellNumber(CurrentRow, 84, (double)(p.TempNewOrder ?? 0));
                        CreateCellNumber(CurrentRow, 85, (double)(p.ArtikelCost ?? 0));
                        CreateCellNumber(CurrentRow, 86, (double)(p.ArtikelProfit ?? 0));
                        CreateCellNumber(CurrentRow, 87, (double)(p.ContainerDE ?? 0));
                        CreateCellNumber(CurrentRow, 88, (double)(p.ArtikelFracht ?? 0));


                        RowIndex++;

                    }



                    ISheet sheet2 = workbook.CreateSheet("Wareneingang");
                    IRow HeaderRow2 = sheet2.CreateRow(0);


                    CreateCellString(HeaderRow2, 0, "Referenznummer");
                    CreateCellString(HeaderRow2, 1, "Artikelnummer");
                    CreateCellString(HeaderRow2, 2, "Artikelgruppe");
                    CreateCellString(HeaderRow2, 3, "USER_Produktionsgruppe");
                    CreateCellString(HeaderRow2, 4, "Belegdatum");
                    CreateCellString(HeaderRow2, 5, "Menge");
                    CreateCellString(HeaderRow2, 6, "Einzelpreis");
                    CreateCellString(HeaderRow2, 7, "USER_ETA");
                    CreateCellString(HeaderRow2, 8, "USER_ETD");
                    CreateCellString(HeaderRow2, 9, "Lieferant");
                    CreateCellString(HeaderRow2, 10, "USER_Rechnungsnummer");
                    CreateCellString(HeaderRow2, 11, "USER_Referenznummer");
                    CreateCellString(HeaderRow2, 12, "USER_Spedition");
                    CreateCellString(HeaderRow2, 13, "BLNummer");
                    CreateCellString(HeaderRow2, 14, "TransItems");
                    CreateCellString(HeaderRow2, 15, "A0Name1");

                    int RowIndex2 = 1;
                    foreach (WarenEingangData wd in Gangs)
                    {
                        IRow CurrentRow = sheet2.CreateRow(RowIndex2);

                        CreateCellString(CurrentRow, 0, wd.Referenznummer);
                        CreateCellString(CurrentRow, 1, wd.Artikelnummer);
                        CreateCellString(CurrentRow, 2, wd.Artikelgruppe);
                        CreateCellString(CurrentRow, 3, wd.USER_Produktionsgruppe);
                        CreateCellDate(CurrentRow, 4, wd.Belegdatum);
                        CreateCellNumber(CurrentRow, 5, (double)(wd.Menge ?? 0));
                        CreateCellNumber(CurrentRow, 6, (double)(wd.Einzelpreis ?? 0));
                        CreateCellDate(CurrentRow, 7, wd.USER_ETA);
                        CreateCellDate(CurrentRow, 8, wd.USER_ETD);
                        CreateCellString(CurrentRow, 9, wd.Lieferant);
                        CreateCellString(CurrentRow, 10, wd.USER_Rechnungsnummer);
                        CreateCellString(CurrentRow, 11, wd.USER_Referenznummer);
                        CreateCellString(CurrentRow, 12, wd.USER_Spedition);
                        CreateCellString(CurrentRow, 13, wd.BLNummer);
                        CreateCellNumber(CurrentRow, 14, (double)(wd.TransItems ?? 0));
                        CreateCellString(CurrentRow, 15, wd.A0Name1);


                        RowIndex2++;
                    }

                    if (Mode == "REIFEN")
                    {
                        ISheet sheet3 = workbook.CreateSheet("Sizes");
                        IRow HeaderRow3 = sheet3.CreateRow(0);
                        List<string> DistinctPatterns = Artikels.Select(x => x.USER_ArtikelName).Distinct().ToList();
                        List<string> DistinctSizes = Artikels.Select(x => x._Size).Distinct().ToList();

                        int cellCount = 0;
                        CreateCellString(HeaderRow3, cellCount, "Size");
                        foreach (string pattern in DistinctPatterns)
                        {
                            cellCount++;
                            CreateCellString(HeaderRow3, cellCount, pattern);
                            cellCount++;
                            CreateCellString(HeaderRow3, cellCount, "Sale0");
                            cellCount++;
                            CreateCellString(HeaderRow3, cellCount, "Sale1");
                        }



                        int RowIndex3 = 1;

                        foreach (string size in DistinctSizes)
                        {
                            cellCount = 0;
                            IRow CurrentRow = sheet3.CreateRow(RowIndex3);

                            CreateCellString(CurrentRow, cellCount, size);

                            foreach (string pattern in DistinctPatterns)
                            {


                                ArtikelData artikel = Artikels.FirstOrDefault(x => x.USER_ArtikelName == pattern && x._Size == size);
                                if (artikel != null)
                                {
                                    cellCount++;
                                    CreateCellString(CurrentRow, cellCount, artikel.USER_txLZLK);
                                    cellCount++;
                                    CreateCellNumber(CurrentRow, cellCount, (double?)artikel.Sum0 ?? 0);
                                    cellCount++;
                                    CreateCellNumber(CurrentRow, cellCount, (double?)artikel.Sum1 ?? 0);

                                }
                                else
                                {
                                    cellCount++;
                                    cellCount++;
                                    cellCount++;
                                }

                            }


                            RowIndex3++;
                        }
                    }
                    workbook.Write(fs);
                }
            }
            else
            {
                using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet1 = workbook.CreateSheet("Artikel Data");

                    IRow HeaderRow = sheet1.CreateRow(0);

                    CreateCellString(HeaderRow, 0, "Artikelnummer");
                    CreateCellString(HeaderRow, 1, "USER_Produktionsgruppe");
                   
                    CreateCellString(HeaderRow, 2, "CustomerContainerRate");
                    CreateCellString(HeaderRow, 3, "Sum0");
                    CreateCellString(HeaderRow, 4, "Sum1");
                    CreateCellString(HeaderRow, 5, "Sum2");
                    CreateCellString(HeaderRow, 6, "Bestand");
                    CreateCellString(HeaderRow, 7, "TransitStock");
                    CreateCellString(HeaderRow, 8, "BackOrder");
                    CreateCellString(HeaderRow, 9, "FactoryStock");
                    
                    CreateCellString(HeaderRow, 10, "USER_ContainerStueck");
                    CreateCellString(HeaderRow, 11, "USER_Radius");
                    CreateCellString(HeaderRow, 12, "USER_NB");
                    CreateCellString(HeaderRow, 13, "USER_Breite");
                    CreateCellString(HeaderRow, 14, "USER_LK1");
                    CreateCellString(HeaderRow, 15, "USER_LK2");
                    CreateCellString(HeaderRow, 16, "USER_ET");
                    CreateCellString(HeaderRow, 17, "USER_Farbekurz");
                    CreateCellString(HeaderRow, 18, "USER_Farbelang");
                    CreateCellString(HeaderRow, 19, "USER_Application");
                    CreateCellString(HeaderRow, 20, "USER_ArtikelName");
                    CreateCellString(HeaderRow, 21, "Matchcode");
                    CreateCellString(HeaderRow, 22, "USER_ArtikelTyp");
                    CreateCellString(HeaderRow, 23, "Gewicht");
                    CreateCellString(HeaderRow, 24, "_ArtikelTyp");
                    CreateCellString(HeaderRow, 25, "Betrag0");
                    CreateCellString(HeaderRow, 26, "Betrag1");
                    CreateCellString(HeaderRow, 27, "Betrag2");

                    CreateCellString(HeaderRow, 28, "Cost");
                    CreateCellString(HeaderRow, 29, "Container DE");
                    CreateCellString(HeaderRow, 30, "Fracht");
                    CreateCellString(HeaderRow, 31, "T24Price");
                    CreateCellString(HeaderRow, 32, "Month1_0");
                    CreateCellString(HeaderRow, 33, "Month2_0");
                    CreateCellString(HeaderRow, 34, "Month3_0");
                    CreateCellString(HeaderRow, 35, "Month4_0");
                    CreateCellString(HeaderRow, 36, "Month5_0");
                    CreateCellString(HeaderRow, 37, "Month6_0");
                    CreateCellString(HeaderRow, 38, "Month7_0");
                    CreateCellString(HeaderRow, 39, "Month8_0");
                    CreateCellString(HeaderRow, 40, "Month9_0");
                    CreateCellString(HeaderRow, 41, "Month10_0");
                    CreateCellString(HeaderRow, 42, "Month11_0");
                    CreateCellString(HeaderRow, 43, "Month12_0");
                    CreateCellString(HeaderRow, 44, "Month1_1");
                    CreateCellString(HeaderRow, 45, "Month2_1");
                    CreateCellString(HeaderRow, 46, "Month3_1");
                    CreateCellString(HeaderRow, 47, "Month4_1");
                    CreateCellString(HeaderRow, 48, "Month5_1");
                    CreateCellString(HeaderRow, 49, "Month6_1");
                    CreateCellString(HeaderRow, 50, "Month7_1");
                    CreateCellString(HeaderRow, 51, "Month8_1");
                    CreateCellString(HeaderRow, 52, "Month9_1");
                    CreateCellString(HeaderRow, 53, "Month10_1");
                    CreateCellString(HeaderRow, 54, "Month11_1");
                    CreateCellString(HeaderRow, 55, "Month12_1");
                    CreateCellString(HeaderRow, 56, "Month1_2");
                    CreateCellString(HeaderRow, 57, "Month2_2");
                    CreateCellString(HeaderRow, 58, "Month3_2");
                    CreateCellString(HeaderRow, 59, "Month4_2");
                    CreateCellString(HeaderRow, 60, "Month5_2");
                    CreateCellString(HeaderRow, 61, "Month6_2");
                    CreateCellString(HeaderRow, 62, "Month7_2");
                    CreateCellString(HeaderRow, 63, "Month8_2");
                    CreateCellString(HeaderRow, 64, "Month9_2");
                    CreateCellString(HeaderRow, 65, "Month10_2");
                    CreateCellString(HeaderRow, 66, "Month11_2");
                    CreateCellString(HeaderRow, 67, "Month12_2");
                   
                   



                    int RowIndex = 1;

                    foreach (ArtikelData p in Artikels)
                    {
                        IRow CurrentRow = sheet1.CreateRow(RowIndex);

                        CreateCellString(CurrentRow, 0, p.Artikelnummer);
                        CreateCellString(CurrentRow, 1, p.USER_Produktionsgruppe);
                        
                        CreateCellNumberPercent(workbook, CurrentRow, 2, (double)(p.ExportWarenEingangRate));
                        CreateCellNumber(CurrentRow, 3, (double)(p.Sum0 ?? 0));
                        CreateCellNumber(CurrentRow, 4, (double)(p.Sum1 ?? 0));
                        CreateCellNumber(CurrentRow, 5, (double)(p.Sum2 ?? 0));
                      
                        CreateCellNumber(CurrentRow, 6, (double)(p.Bestand ?? 0));
                        CreateCellNumber(CurrentRow, 7, (double)(p.TransitStock ?? 0));
                        CreateCellNumber(CurrentRow, 8, (double)(p.BackOrder ?? 0));
                        CreateCellNumber(CurrentRow, 9, (double)(p.FactoryStock ?? 0));
                        
   
                        
                        CreateCellNumber(CurrentRow, 10, (double)(p.USER_ContainerStueck ?? 0));
                        CreateCellString(CurrentRow, 11, p.USER_Radius);
                        CreateCellString(CurrentRow, 12, p.USER_NB);
                        CreateCellString(CurrentRow, 13, p.USER_Breite);
                        CreateCellString(CurrentRow, 14, p.USER_LK1);
                        CreateCellString(CurrentRow, 15, p.USER_LK2);
                        CreateCellString(CurrentRow, 16, p.USER_ET);
                        CreateCellString(CurrentRow, 17, p.USER_Farbekurz);
                        CreateCellString(CurrentRow, 18, p.USER_Farbelang);
                        CreateCellString(CurrentRow, 19, p.USER_Application);
                        CreateCellString(CurrentRow, 20, p.USER_ArtikelName);
                        CreateCellString(CurrentRow, 21, p.Matchcode);
                        CreateCellString(CurrentRow, 22, p.USER_ArtikelTyp);
                        CreateCellNumber(CurrentRow, 23, (double)(p.Gewicht ?? 0));
                        CreateCellString(CurrentRow, 24, p._ArtikelTyp);
                        CreateCellNumber(CurrentRow, 25, (double)(p.Betrag0 ?? 0));
                        CreateCellNumber(CurrentRow, 26, (double)(p.Betrag1 ?? 0));
                        CreateCellNumber(CurrentRow, 27, (double)(p.Betrag2 ?? 0));
                        CreateCellNumber(CurrentRow, 28, (double)(p.ArtikelCost ?? 0));
                        CreateCellNumber(CurrentRow, 29, (double)(p.ContainerDE ?? 0));
                        CreateCellNumber(CurrentRow, 30, (double)(p.ArtikelFracht ?? 0));
                        CreateCellNumber(CurrentRow, 31, (double)(p.T24Price ?? 0));
                        CreateCellNumber(CurrentRow, 32, (double)(p.Month1_0 ?? 0));
                        CreateCellNumber(CurrentRow, 33, (double)(p.Month2_0 ?? 0));
                        CreateCellNumber(CurrentRow, 34, (double)(p.Month3_0 ?? 0));
                        CreateCellNumber(CurrentRow, 35, (double)(p.Month4_0 ?? 0));
                        CreateCellNumber(CurrentRow, 36, (double)(p.Month5_0 ?? 0));
                        CreateCellNumber(CurrentRow, 37, (double)(p.Month6_0 ?? 0));
                        CreateCellNumber(CurrentRow, 38, (double)(p.Month7_0 ?? 0));
                        CreateCellNumber(CurrentRow, 39, (double)(p.Month8_0 ?? 0));
                        CreateCellNumber(CurrentRow, 40, (double)(p.Month9_0 ?? 0));
                        CreateCellNumber(CurrentRow, 41, (double)(p.Month10_0 ?? 0));
                        CreateCellNumber(CurrentRow, 42, (double)(p.Month11_0 ?? 0));
                        CreateCellNumber(CurrentRow, 43, (double)(p.Month12_0 ?? 0));
                        CreateCellNumber(CurrentRow, 44, (double)(p.Month1_1 ?? 0));
                        CreateCellNumber(CurrentRow, 45, (double)(p.Month2_1 ?? 0));
                        CreateCellNumber(CurrentRow, 46, (double)(p.Month3_1 ?? 0));
                        CreateCellNumber(CurrentRow, 47, (double)(p.Month4_1 ?? 0));
                        CreateCellNumber(CurrentRow, 48, (double)(p.Month5_1 ?? 0));
                        CreateCellNumber(CurrentRow, 49, (double)(p.Month6_1 ?? 0));
                        CreateCellNumber(CurrentRow, 50, (double)(p.Month7_1 ?? 0));
                        CreateCellNumber(CurrentRow, 51, (double)(p.Month8_1 ?? 0));
                        CreateCellNumber(CurrentRow, 52, (double)(p.Month9_1 ?? 0));
                        CreateCellNumber(CurrentRow, 53, (double)(p.Month10_1 ?? 0));
                        CreateCellNumber(CurrentRow, 54, (double)(p.Month11_1 ?? 0));
                        CreateCellNumber(CurrentRow, 55, (double)(p.Month12_1 ?? 0));
                        CreateCellNumber(CurrentRow, 56, (double)(p.Month1_2 ?? 0));
                        CreateCellNumber(CurrentRow, 57, (double)(p.Month2_2 ?? 0));
                        CreateCellNumber(CurrentRow, 58, (double)(p.Month3_2 ?? 0));
                        CreateCellNumber(CurrentRow, 59, (double)(p.Month4_2 ?? 0));
                        CreateCellNumber(CurrentRow, 60, (double)(p.Month5_2 ?? 0));
                        CreateCellNumber(CurrentRow, 61, (double)(p.Month6_2 ?? 0));
                        CreateCellNumber(CurrentRow, 62, (double)(p.Month7_2 ?? 0));
                        CreateCellNumber(CurrentRow, 63, (double)(p.Month8_2 ?? 0));
                        CreateCellNumber(CurrentRow, 64, (double)(p.Month9_2 ?? 0));
                        CreateCellNumber(CurrentRow, 65, (double)(p.Month10_2 ?? 0));
                        CreateCellNumber(CurrentRow, 66, (double)(p.Month11_2 ?? 0));
                        CreateCellNumber(CurrentRow, 67, (double)(p.Month12_2 ?? 0));        
                   


                        RowIndex++;

                    }

                                                     
                    workbook.Write(fs);
                }
            }
            
        }
        private void CreateCellString(IRow CurrentRow, int CellIndex, string Value)
        {
            ICell Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);
        }
        private void CreateCellNumber(IRow CurrentRow, int CellIndex, double Value)
        {
            ICell Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);
        }
        private void CreateCellNumberPercent(IWorkbook book,IRow CurrentRow, int CellIndex, double Value)
        {
            ICell Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);

            ICellStyle cellStyle = book.CreateCellStyle();
            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
            Cell.CellStyle = cellStyle;
        }        
        private void CreateCellDate(IRow CurrentRow, int CellIndex, DateTime? Value)
        {
            
            ICell Cell = CurrentRow.CreateCell(CellIndex);         
            Cell.SetCellValue(Value ?? DateTime.MinValue);

            
        }
        public void exportInvoiceCheck(List<BestellungPositionPrice> BestellungList,List<InvoiceFormDTO> InvoiceEntries,InvoiceAngaben angaben)
        {
            var newFile = @"wwwroot\files\Invoice.xlsx";

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1 = workbook.CreateSheet("Bestellung");

                IRow HeaderRow = sheet1.CreateRow(0);

                CreateCellString(HeaderRow, 0, "Matchcode");
                CreateCellString(HeaderRow, 1, "Lieferant");
                CreateCellString(HeaderRow, 2, "Belegnummer");
                CreateCellString(HeaderRow, 3, "BestellDatum");
                CreateCellString(HeaderRow, 4, "VorID");
                CreateCellString(HeaderRow, 5, "Artikelnummer");
                CreateCellString(HeaderRow, 6, "Total Order");
                CreateCellString(HeaderRow, 7, "Einzelpreis");
                CreateCellString(HeaderRow, 8, "Back Order");

                int RowIndex = 1;

                foreach (BestellungPositionPrice bes in BestellungList)
                {
                    IRow CurrentRow = sheet1.CreateRow(RowIndex);

                    CreateCellString(CurrentRow, 0, bes.Matchcode);
                    CreateCellString(CurrentRow, 1, bes.Lieferant);
                    CreateCellNumber(CurrentRow, 2, bes.Belegnummer);
                    CreateCellString(CurrentRow, 3, bes.BestellDatum);
                    CreateCellNumber(CurrentRow, 4, bes.VorID);
                    CreateCellString(CurrentRow, 5, bes.Artikelnummer);
                    CreateCellNumber(CurrentRow, 6, (int)bes.TotalOrder);
                    CreateCellNumber(CurrentRow, 7, (double)bes.Einzelpreis);
                    CreateCellNumber(CurrentRow, 8, bes.BackOrder);

                    RowIndex++;

                }

                ISheet sheet2 = workbook.CreateSheet("Invoice");
                IRow NumberRow = sheet2.CreateRow(0);
                CreateCellString(NumberRow, 0, "Invoice No");
                CreateCellString(NumberRow, 1, angaben.InvoiceNo);

                NumberRow = sheet2.CreateRow(1);
                CreateCellString(NumberRow, 0, "Reference No");
                CreateCellString(NumberRow, 1, angaben.ReferenceNo);

                NumberRow = sheet2.CreateRow(2);
                CreateCellString(NumberRow, 0, "BL No");
                CreateCellString(NumberRow, 1, angaben.BLno);

                NumberRow = sheet2.CreateRow(3);
                CreateCellString(NumberRow, 0, "Lieferant");
                CreateCellString(NumberRow, 1, angaben.Lieferant);

                NumberRow = sheet2.CreateRow(4);
                CreateCellString(NumberRow, 0, "Vorgang ID");
                CreateCellString(NumberRow, 1, angaben.VorgangID);
              
                IRow HeaderRow2 = sheet2.CreateRow(6);


                CreateCellString(HeaderRow2, 0, "Artikelnummer");
                CreateCellString(HeaderRow2, 1, "Menge");
                CreateCellString(HeaderRow2, 2, "Price");
                CreateCellString(HeaderRow2, 3, "Amount");
                CreateCellString(HeaderRow2, 4, "Status");


                int RowIndex2 = 7;
                foreach (InvoiceFormDTO iv in InvoiceEntries)
                {
                    if (iv.Artikelnummer != null)
                    {
                        IRow CurrentRow = sheet2.CreateRow(RowIndex2);

                        CreateCellString(CurrentRow, 0, iv.Artikelnummer);
                        CreateCellNumber(CurrentRow, 1, iv.Menge ?? 0);
                        CreateCellNumber(CurrentRow, 2, (double?)iv.Price ?? 0);
                        CreateCellNumber(CurrentRow, 3, (double?)iv.Amount ?? 0);
                        CreateCellString(CurrentRow, 4, iv.Status);

                        RowIndex2++;
                    }
                   
                }

                workbook.Write(fs);
            }
        }
    }
}
