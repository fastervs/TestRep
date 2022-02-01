using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGeogrInfaCom
{
    public static class ExportToExcel 
    {

        /// <summary>
        /// Создаёт файл Excel c расширением .xlsx или .xls
        /// и заполняет названия колонок таблицы 
        /// </summary>
        public static void CreateTestExcelFile(string filepath, IEnumerable<Result> resultData)
        {
            //Рабочая книга Excel
            XSSFWorkbook wb;
            //Лист в книге Excel
            XSSFSheet sh;

            //Создаем рабочую книгу
            wb = new XSSFWorkbook();
            //Создаём лист в книге
            sh = (XSSFSheet)wb.CreateSheet("Результат тестового рассчёта");


            //Создаем заголвоок
            var HeaderRow = sh.CreateRow(0);
          
            HeaderRow.CreateCell(0).SetCellValue("Var");

            sh.AutoSizeColumn(0);

          
            HeaderRow.CreateCell(1).SetCellValue("Val");

            sh.AutoSizeColumn(1);

     
            for (int i = 1; i < resultData.Count()+1; i++)
            {
                //Создаем строку
                var currentRow = sh.CreateRow(i);
                    //в строке создаём ячеёку с указанием столбца
                currentRow.CreateCell(0).SetCellValue(resultData.ElementAt(i-1).var);//-1 because of header

                sh.AutoSizeColumn(0);


                currentRow.CreateCell(1).SetCellValue(resultData.ElementAt(i-1).val);

                sh.AutoSizeColumn(1);

            }

            using (var fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                wb.Write(fs);
            }

            Process.Start(filepath);
        }

        
    }
}
