using ManagerIS.Common;
using ManagerIS.Operation;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ManagerIS.Winform {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            lswNZY.Columns.Add("lstPCMC", "批次名称");
            lswNZY.Columns[0].Width = lswNZY.Width;
            lswDK.Columns.Add("lstDKMC", "地块名称");
            lswDK.Columns[0].Width = lswDK.Width;
            lswGDDK.Columns.Add("供地文号");
            lswGDDK.Columns.Add("用地单位");
            lswGDDK.Columns.Add("出让面积");
            lswGDDK.Columns.Add("带供面积");
            foreach (ColumnHeader column in lswGDDK.Columns) {
                column.Width = lswGDDK.Width / 4;
            }
            dgvGDDK.Columns.Add("dzjgh", "供地文号");
            dgvGDDK.Columns.Add("xmmc", "用地单位");
            dgvGDDK.Columns.Add("gdmj", "出让面积");
            dgvGDDK.Columns.Add("dgmj", "带供面积");
            foreach (DataGridViewColumn column in dgvGDDK.Columns) {
                column.Width = dgvGDDK.Width / 4;
            }



            List<Data> datas = DataOperation.MySQLRead();
            foreach (Data data in datas) {
                ListViewItem item = new ListViewItem();
                item.Text = data.Nzy;
                item.Tag = data;
                lswNZY.Items.Add(item);
            }

        }

        private void lswNZY_SelectedIndexChanged(object sender, EventArgs e) {
            lswDK.Items.Clear();
            if (lswNZY.SelectedItems.Count>0) {
                Data data = (Data)lswNZY.SelectedItems[0].Tag;
                DataOperation.MySQLDKRead(data);
                foreach (NZYDK nzydk in data.Dk) {
                    ListViewItem item = new ListViewItem();
                    item.Text = nzydk.Dkmc;
                    item.Tag = nzydk;
                    lswDK.Items.Add(item);
                }
            }

        }

        private void lswDK_SelectedIndexChanged(object sender, EventArgs e) {
            lswGDDK.Items.Clear();
            dgvGDDK.DataSource = null;
            if (lswDK.SelectedItems.Count>0) {
                NZYDK nzydk = (NZYDK)lswDK.SelectedItems[0].Tag;
                DataOperation.MySQLGDRead(nzydk);

                dgvGDDK.DataSource = nzydk.Gddk;
                //dgvGDDK.Columns[0].DataPropertyName = "dzjgh";
                //dgvGDDK.Columns[1].DataPropertyName = "xmmc";
                //dgvGDDK.Columns[2].DataPropertyName = "gdmj";
                //dgvGDDK.Columns[3].DataPropertyName = "dgmj";
                //foreach (GDDK gddk in nzydk.Gddk) {
                    
                //    string[] str = new string[]{ gddk.Dzjgh, gddk.Xmmc, gddk.Gdmj.ToString(), gddk.Dgmj.ToString() };
                //    ListViewItem item = new ListViewItem(str);
                //    item.Tag = gddk;

                //    DataGridViewRow row = new DataGridViewRow();
                    

                //    lswGDDK.Items.Add(item);
                //}
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
