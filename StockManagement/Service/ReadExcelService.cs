using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using StockManagement.Model;
using StockManagement.Model.Dtos;

namespace StockManagement.Service
{
    public class ReadExcelService
    {
        public List<FactoryStockDTO> procExcel(string Mode)
        {
            List<FactoryStockDTO> returnStock = new List<FactoryStockDTO>();
            string path;
            if (Mode == "REIFEN")
            {
                path = @"\\DEFTKTSAGEWEB\_NetApp\104_StockManagement\StockManagement\REIFEN.xlsx";
            }
            else
            {
                path = @"\\DEFTKTSAGEWEB\_NetApp\104_StockManagement\StockManagement\FELGEN.xlsx";
            }
            

             IWorkbook book;

            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                // Try to read workbook as XLSX:
                try
                {
                    book = new XSSFWorkbook(fs);

                    ISheet sheet = book.GetSheetAt(0);

                    if (sheet != null)
                    {
                        int rowCount = sheet.LastRowNum; 
                                                         
                        for (int i = 0; i <= rowCount; i++)
                        {
                            IRow curRow = sheet.GetRow(i);
                            
                            if (curRow == null)
                            {
                                
                                rowCount = i - 1;
                                break;
                            }                            
                            var codeValue = curRow.GetCell(0).StringCellValue.Trim();
                            var mengeValue = 0.0;
                            if(curRow.GetCell(1) != null)
                            {
                                mengeValue = curRow.GetCell(1).NumericCellValue;
                            }
                            

                            returnStock.Add(new FactoryStockDTO { Code = codeValue, Menge = mengeValue });

                        }
                    }
                }
                catch
                {
                    book = null;
                }

                // If reading fails, try to read workbook as XLS:
                if (book == null)
                {
                    book = new HSSFWorkbook(fs);
                }
            }
            catch (Exception ex)
            {
               
                
            }

            return returnStock;
        }



    }
}
