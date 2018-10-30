﻿using ManagerIS.Common;
using ManagerIS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIS.Operation {
    public abstract partial class DataOperation {
        public static void DataChecked(string file) {
            List<Data> datas = MySQLViewRead();
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("批次名");
            dt.Columns.Add("批次面积");
            dt.Columns.Add("地块名称");
            dt.Columns.Add("地块面积");
            dt.Columns.Add("地块剩余面积");
            foreach (Data data in datas) {
                foreach (NZYDK nzydk in data.Dk) {
                    if (nzydk.SYMJ()>0) {
                        DataRow dr = dt.NewRow();
                        dr[0] = 0;
                        dr[1] = data.Nzy;
                        dr[2] = data.GetArea()*15;
                        dr[3] = nzydk.Dkmc;
                        dr[4] = nzydk.Dkmj * 15;
                        dr[5] = nzydk.SYMJ()* 15;
                        dt.Rows.Add(dr);
                    }
                }



            }
            if (File.Exists(file + "未供完.xlsx")) {
                File.Delete(file + "未供完.xlsx");
            }

            ExcelHelper excel = new ExcelHelper(file + "未供完.xlsx");
            int count = excel.DataTableToExcel(dt, "检查", true);

        }
    }
}
