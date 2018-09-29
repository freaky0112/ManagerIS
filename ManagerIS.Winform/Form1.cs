using ManagerIS.Common;
using ManagerIS.Operation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagerIS.Winform {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            lswNZY.Columns.Add("lstPCMC", "批次名称");
            List<Data> datas = DataOperation.MySQLRead();
            foreach (Data data in datas) {
                ListViewItem item = new ListViewItem();
                item.Text = data.Nzy;
                item.Tag = data.Guid;
                lswNZY.Items.Add(item);
            }

        }
    }
}
