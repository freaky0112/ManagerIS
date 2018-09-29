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

            tbxImportUrl.Text = @"C:\Users\freak\Desktop\终.xlsx";
        }

        private void tbxImportUrl_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

        }

        private void btnImport_Click(object sender, RoutedEventArgs e) {
            DataOperation.ExcelToMysql(tbxImportUrl.Text, 2017);
        }
    }
}
