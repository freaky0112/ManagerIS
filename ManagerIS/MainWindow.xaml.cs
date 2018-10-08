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

            tbxImportUrl.Text = @"C:\Users\freak\Desktop\补充.xlsx";
        }

        private void tbxImportUrl_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

        }

        private void btnImport_Click(object sender, RoutedEventArgs e) {
            int year = int.Parse(tbxYear.Text);
            //for (int i = 2009; i <= 2018; i++) {
            //DataOperation.ExcelToMysql(tbxImportUrl.Text, year);
            //}
            DataOperation.ImportDZJGH(@"C:\Users\freak\Desktop\海宁.xls");
            MessageBox.Show("导入成功");
        }

        private void btnExport_Click(object sender, RoutedEventArgs e) {
            DateTime dt = DateTime.Now;
            
            //try {
            //DataOperation.DataCheck(@"C:\Users\freak\Desktop\");
            DataOperation.DataExport(@"C:\Users\freak\Desktop\");
            ExcelOperation.MergeExcel(@"C:\Users\freak\Desktop\");
            //ExcelOperation.FormateExcel(@"C:\Users\freak\Desktop\导出.xlsx");

            MessageBox.Show(string.Format("导出成功,用时{0}秒",((Int32)((DateTime.Now-dt)).TotalSeconds)).ToString());
            // } catch (Exception ex) {

            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
