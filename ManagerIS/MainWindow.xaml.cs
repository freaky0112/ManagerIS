using System;
using System.Windows;
using System.Windows.Input;
using ManagerIS.Operation;

namespace ManagerIS {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            //初始化程序
            //Method.Initialize();
            //SQLiteOperation.Initialize();

            tbxImportUrl.Text = @"\\Mac\Home\Desktop\补充.xlsx";
        }

        private void tbxImportUrl_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

        }

        private void btnImport_Click(object sender, RoutedEventArgs e) {
            int year = int.Parse(tbxYear.Text);
            //for (int i = 2009; i <= 2018; i++) {
            //DataOperation.ExcelToMysql(tbxImportUrl.Text, year);
            //}
            DataOperation.ImportDZJGH(@"\\Mac\Home\Desktop\海宁20181010.xls");

            MessageBox.Show("导入成功");
        }

        private void btnExport_Click(object sender, RoutedEventArgs e) {
            DateTime dt = DateTime.Now;

            //try {
            //DataOperation.DataCheck(@"\\Mac\Home\Desktop\");
            DataOperation.DataChecked(@"\\Mac\Home\Desktop\");
            //ExcelOperation.FormatWGWExcel(@"\\Mac\Home\Desktop\未供完.xlsx"); ;
            //DataOperation.DataExport(@"\\Mac\Home\Desktop\");
            //ExcelOperation.MergeExcel(@"\\Mac\Home\Desktop\");
            ////ExcelOperation.FormatePHExcel(@"\\Mac\Home\Desktop\导出盘活.xlsx");


            MessageBox.Show(string.Format("导出成功,用时{0}秒",((Int32)((DateTime.Now-dt)).TotalSeconds)).ToString());
            // } catch (Exception ex) {

            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
