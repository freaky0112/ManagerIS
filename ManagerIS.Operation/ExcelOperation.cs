using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
namespace ManagerIS.Operation {
    public abstract class ExcelOperation {
        public static void MergeExcel(string file) {

            Application app = new Application();
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;
            app.Visible = false;
            app.UserControl = true;
            Workbooks workbooks = app.Workbooks;
            _Workbook workbook = workbooks.Add(file+"2009.xlsx");
            Sheets sheets = workbook.Sheets;
            Worksheet worksheet = (Worksheet)sheets.get_Item(1);
            Workbooks result_book = app.Workbooks;
            string start = worksheet.Cells[2,11].TEXT;
            _Workbook result = result_book.Add(file+"导出.xlsx");
            result.Sheets.Copy(Type.Missing, sheets[1]);
            //result_sheet.Name = 2009.ToString();
            //result.Sheets.Add(worksheet);
            result.SaveAs(file + "导出.xlsx");
            app.Quit();
        }
    }
}
