using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerIS.Common;
using Microsoft.Office.Interop.Excel;
namespace ManagerIS.Operation {
    public abstract class ExcelOperation {
        public static void MergeExcel(string file) {
            Application excel = new Application();

            try {


                Workbook workbook_export = excel.Workbooks.Open(file + "导出.xlsx");
                Worksheet export = workbook_export.Sheets[1];
                for (int i = 2017; i >= 2009; i--) {
                    Workbook workbook_source = excel.Workbooks.Open(file + i.ToString() + ".xlsx");
                    Worksheet source = workbook_source.Sheets[1];
                    source.Copy(Type.Missing, export);
                    workbook_source.Close();
                }
                //export.Delete();
                workbook_export.Save();
                workbook_export.Close();
            } catch (Exception) {

                throw;
            } finally {

                excel.Quit();
            }

            for (int i = 2009; i <= 2017; i++) {
                File.Delete(file + i.ToString() + ".xlsx");
            }
            FormateExcel(file + "导出.xlsx");
        }
        /// <summary>
        /// 格式化表格
        /// </summary>
        /// <param name="file"></param>
        public static void FormateExcel(string file) {
            Application app = new Application();
            app.DisplayAlerts = false;
            app.Visible = false;
            app.UserControl = true;
            Workbooks workbooks = app.Workbooks;
            _Workbook workbook = workbooks.Add(file);
            Sheets sheets = workbook.Sheets;
            for (int i = 1; i <= 1; i++) {//读取2009-2017表格，首张为汇总表
                Worksheet worksheet = (Worksheet)sheets.get_Item(i);
                int recordCount = 2;//从第二行开始判断
                //查找数据行数
                while (true) {
                    if (Method.IsNumber(worksheet.Cells[recordCount, 1].TEXT)) {
                        recordCount++;
                    } else {
                        break;
                    }
                }
                MergeCell(ref worksheet, 2, recordCount, 3);
                MergeCell(ref worksheet, 2, recordCount, 7);
            }
            workbook.SaveAs(file);
            app.Quit();
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="startline"></param>
        /// <param name="recCount"></param>
        /// <param name="col"></param>
        private static void MergeCell(ref Worksheet sheet, int startline, int recCount, int col) {
            string start = sheet.Cells[startline, col].TEXT;
            int index = 1;
            for (int i = startline; i < recCount;) {
                string temp;
                int j = i;
                while (true) {
                    temp = sheet.Cells[j, col].TEXT;
                    if (temp.Equals(start)) {
                        j++;
                    } else {
                        start = temp;
                        sheet.Range[sheet.Cells[i, col], sheet.Cells[j - 1, col]].Merge(Type.Missing);
                        if (col == 3) {// 通过第三列批次名称判断合并批次单元格
                            sheet.Cells[i, 1] = index;
                            index++;
                            //合并序号列
                            sheet.Range[sheet.Cells[i, 1], sheet.Cells[j - 1, 1]].Merge(Type.Missing);
                            //合并第2列
                            sheet.Range[sheet.Cells[i, col - 1], sheet.Cells[j - 1, col - 1]].Merge(Type.Missing);
                            //合并箱9列
                            sheet.Range[sheet.Cells[i, 9], sheet.Cells[j - 1, 9]].Merge(Type.Missing);
                            //合并第14列
                            sheet.Range[sheet.Cells[i, 14], sheet.Cells[j - 1, 14]].Merge(Type.Missing);
                            for (int k = 1; k <= 3; k++) {//合并4、5、6列
                                sheet.Range[sheet.Cells[i, col + k], sheet.Cells[j - 1, col + k]].Merge(Type.Missing);
                            }

                        }
                        if (col == 7) {
                            //合并6、7列
                            sheet.Range[sheet.Cells[i, col], sheet.Cells[j - 1, col]].Merge(Type.Missing);
                            sheet.Range[sheet.Cells[i, col + 1], sheet.Cells[j - 1, col + 1]].Merge(Type.Missing);
                            //合并第15列
                            sheet.Range[sheet.Cells[i, 15], sheet.Cells[j - 1, 15]].Merge(Type.Missing);
                            //合并20-36列
                            for (int k = 20; k < 37; k++) {
                                sheet.Range[sheet.Cells[i, k], sheet.Cells[j - 1, k]].Merge(Type.Missing);

                            }
                        }
                        i = j;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(start)) {
                    break;
                }
            }
        }

        /// <summary>
        /// 格式化盘活表格
        /// </summary>
        /// <param name="file"></param>
        public static void FormatePHExcel(string file) {
            Application app = new Application();
            app.DisplayAlerts = false;
            app.Visible = false;
            app.UserControl = true;
            Workbooks workbooks = app.Workbooks;
            _Workbook workbook = workbooks.Add(file);
            Sheets sheets = workbook.Sheets;
            Worksheet worksheet = (Worksheet)sheets.get_Item(1);
            int recordCount = 2;//从第二行开始判断
                                //查找数据行数
            while (true) {
                if (Method.IsNumber(worksheet.Cells[recordCount, 1].TEXT)) {
                    recordCount++;
                } else {
                    break;
                }
            }
            MergePHCell(ref worksheet, 2, recordCount, 5);
            MergePHCell(ref worksheet, 2, recordCount, 6);
            workbook.SaveAs(file);
            app.Quit();
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="startline"></param>
        /// <param name="recCount"></param>
        /// <param name="col"></param>
        private static void MergePHCell(ref Worksheet sheet, int startline, int recCount, int col) {
            string start = sheet.Cells[startline, col].TEXT;
            int index = 1;
            for (int i = startline; i < recCount;) {
                string temp;
                int j = i;
                while (true) {
                    temp = sheet.Cells[j, col].TEXT;
                    if (temp.Equals(start)) {
                        j++;
                    } else {
                        start = temp;
                        sheet.Range[sheet.Cells[i, col], sheet.Cells[j - 1, col]].Merge(Type.Missing);
                        if (col == 5) {// 通过第三列批次名称判断合并批次单元格
                            sheet.Cells[i, 1] = index;
                            index++;
                            //合并1-4列
                            for (int k = 1; k <= 5; k++) {
                                sheet.Range[sheet.Cells[i, k], sheet.Cells[j - 1, k]].Merge(Type.Missing);

                            }
                        }
                        if (col == 6) {
                            //合并6、7列
                            //sheet.Range[sheet.Cells[i, col], sheet.Cells[j - 1, col]].Merge(Type.Missing);
                            //合并5-10列
                            for (int k = 6; k < 12; k++) {
                                sheet.Range[sheet.Cells[i, k], sheet.Cells[j - 1, k]].Merge(Type.Missing);

                            }
                        }
                        i = j;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(start)) {
                    break;
                }
            }
        }


        /// <summary>
        /// 格式化未供完表格
        /// </summary>
        /// <param name="file"></param>
        public static void FormatWGWExcel(string file) {
            Application app = new Application();
            app.DisplayAlerts = false;
            app.Visible = false;
            app.UserControl = true;
            Workbooks workbooks = app.Workbooks;
            _Workbook workbook = workbooks.Add(file);
            Sheets sheets = workbook.Sheets;
            Worksheet worksheet = (Worksheet)sheets.get_Item(1);
            int recordCount = 2;//从第二行开始判断
                                //查找数据行数
            while (true) {
                if (Method.IsNumber(worksheet.Cells[recordCount, 1].TEXT)) {
                    recordCount++;
                } else {
                    break;
                }
            }
            MergeWGWCell(ref worksheet, 2, recordCount, 2);
            //MergePHCell(ref worksheet, 2, recordCount, 6);
            workbook.SaveAs(file);
            app.Quit();
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="startline"></param>
        /// <param name="recCount"></param>
        /// <param name="col"></param>
        private static void MergeWGWCell(ref Worksheet sheet, int startline, int recCount, int col) {
            string start = sheet.Cells[startline, col].TEXT;
            int index = 1;
            for (int i = startline; i < recCount;) {
                string temp;
                int j = i;
                while (true) {
                    temp = sheet.Cells[j, col].TEXT;
                    if (temp.Equals(start)) {
                        j++;
                    } else {
                        start = temp;
                        sheet.Range[sheet.Cells[i, col], sheet.Cells[j - 1, col]].Merge(Type.Missing);
                        if (col == 2) {// 通过第二列批次名称判断合并批次单元格
                            sheet.Cells[i, 1] = index;
                            index++;
                            //合并1-3列
                            for (int k = 1; k <= 3; k++) {
                                sheet.Range[sheet.Cells[i, k], sheet.Cells[j - 1, k]].Merge(Type.Missing);

                            }
                        }
                        
                        i = j;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(start)) {
                    break;
                }
            }
        }
    }
}
