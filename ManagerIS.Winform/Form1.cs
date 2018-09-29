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

        NZYDK nzydk = new NZYDK();
        List<Data> datas;
        private void Form1_Load(object sender, EventArgs e) {
            lswNZY.Columns.Add("lstPCMC", "批次名称");
            lswNZY.Columns[0].Width = lswNZY.Width;
            lswDK.Columns.Add("lstDKMC", "地块名称");
            lswDK.Columns.Add("lstDKMJ", "地块面积");
            lswDK.Columns[0].Width = lswDK.Width/2;
            lswGDDK.Columns.Add("供地文号");
            lswGDDK.Columns.Add("用地单位");
            lswGDDK.Columns.Add("出让面积");
            lswGDDK.Columns.Add("带供面积");
            foreach (ColumnHeader column in lswGDDK.Columns) {
                column.Width = lswGDDK.Width / 4;
            }
            //dgvGDDK.Columns.Add("dzjgh", "供地文号");
            //dgvGDDK.Columns.Add("xmmc", "用地单位");
            //dgvGDDK.Columns.Add("gdmj", "出让面积");
            //dgvGDDK.Columns.Add("dgmj", "带供面积");
            //foreach (DataGridViewColumn column in dgvGDDK.Columns) {
            //    column.Width = dgvGDDK.Width / 4;
            //}



            datas = DataOperation.MySQLRead();
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
                    string[] str = new string[] { nzydk.Dkmc,nzydk.Dkmj.ToString() };
                    ListViewItem item = new ListViewItem(str);
                    item.Tag = nzydk;
                    lswDK.Items.Add(item);
                }
            }

        }

        private void lswDK_SelectedIndexChanged(object sender, EventArgs e) {
            lswGDDK.Items.Clear();
            //dgvGDDK.DataSource = null;
            if (lswDK.SelectedItems.Count>0) {
                nzydk = (NZYDK)lswDK.SelectedItems[0].Tag;
                DataOperation.MySQLGDRead(nzydk);

                //dgvGDDK.DataSource = nzydk.Gddk;
                //dgvGDDK.Columns[0].DataPropertyName = "dzjgh";
                //dgvGDDK.Columns[1].DataPropertyName = "xmmc";
                //dgvGDDK.Columns[2].DataPropertyName = "gdmj";
                //dgvGDDK.Columns[3].DataPropertyName = "dgmj";
                foreach (GDDK gddk in nzydk.Gddk) {

                    string[] str = new string[] { gddk.Dzjgh, gddk.Xmmc, gddk.Gdmj.ToString(), gddk.Dgmj.ToString() };
                    ListViewItem item = new ListViewItem(str);
                    item.Tag = gddk;

                    //DataGridViewRow row = new DataGridViewRow();


                    lswGDDK.Items.Add(item);
                }
                cbxCZFS.SelectedIndex = 1;
                cbxCZFS.SelectedIndex = 0;
                comboBox1.SelectedIndex = nzydk.Sx;
                tbxBZ.Text = nzydk.Bz;
                lblCurrentDk.Text = nzydk.Dkmc;
            }
        }



        private void lswGDDK_DoubleClick(object sender, EventArgs e) {
            if (lswNZY.SelectedItems.Count > 0) {
                GDDK gddk = (GDDK)lswGDDK.SelectedItems[0].Tag;
                Form2 fm = new Form2(gddk, false);
                if (fm.ShowDialog() == DialogResult.OK) {
                    DataOperation.UpdateGDDK(gddk);
                }
            }
        }

        private void btnADD_Click(object sender, EventArgs e) {

            GDDK gddk = new GDDK();
            Form2 fm = new Form2(gddk,true);
            if (fm.ShowDialog()==DialogResult.OK) {
                DataOperation.GddkToMySQL(nzydk.Guid, gddk);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e) {
            if (lswNZY.SelectedItems.Count > 0) {
                GDDK gddk = (GDDK)lswGDDK.SelectedItems[0].Tag;
                DataOperation.DeleteGDDK(gddk);
                MessageBox.Show("删除成功");
            }
        }

        private void cbxCZFS_SelectedIndexChanged(object sender, EventArgs e) {
            tbxCZFS.Text = nzydk.Czfs[cbxCZFS.SelectedIndex].ToString();
        }

        private void tbxCZFS_TextChanged(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(tbxCZFS.Text)) {
                tbxCZFS.Text = "0";
            }
            if (!Method.IsNumber(tbxCZFS.Text)) {
                MessageBox.Show("请输入数字");
                tbxCZFS.Text = "0";
            }
            nzydk.Czfs[cbxCZFS.SelectedIndex] = decimal.Parse(tbxCZFS.Text);
        }

        private void btnSubmit_Click(object sender, EventArgs e) {
            try {
                nzydk.Check();
                    DataOperation.UpdateCZFS(nzydk);
                DataOperation.UpdateNzydk(nzydk);
                    MessageBox.Show("数据更新成功");
                tbxBZ.Text = "";

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) {
            nzydk.Sx = comboBox1.SelectedIndex;
        }

        private void lswGDDK_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            lswDK.Items.Clear();
            if (lswNZY.SelectedItems.Count > 0) {
                Data data = (Data)lswNZY.SelectedItems[0].Tag;
                DataOperation.MySQLDKRead(data);
                foreach (NZYDK nzydk in data.Dk) {
                    string[] str = new string[] { nzydk.Dkmc, nzydk.Dkmj.ToString() };
                    ListViewItem item = new ListViewItem(str);
                    item.Tag = nzydk;
                    if (nzydk.Dkmc.Contains(tbxFilter.Text)) {
                        lswDK.Items.Add(item);
                    }
                    
                }
            }
        }

        private void tbxFilterPC_TextChanged(object sender, EventArgs e) {
            lswNZY.Items.Clear();
            foreach (Data data in datas) {
                ListViewItem item = new ListViewItem();
                item.Text = data.Nzy;
                item.Tag = data;
                if (data.Nzy.Contains(tbxFilterPC.Text)) {
                    lswNZY.Items.Add(item);
                }
                
            }
        }

        private void tbxFilterPC_Enter(object sender, EventArgs e) {
            TextBox txt = sender as TextBox;
            txt.SelectAll();
        }

        private void tbxFilterPC_MouseClick(object sender, MouseEventArgs e) {
            TextBox txt = sender as TextBox;
            txt.Tag = 1;
            txt.SelectAll();
        }

        private void tbxFilterPC_Leave(object sender, EventArgs e) {
            TextBox txt = sender as TextBox;
            txt.Tag = 1;
            txt.SelectAll();
        }

        private void tbxBZ_TextChanged(object sender, EventArgs e) {
           nzydk.Bz= tbxBZ.Text;
        }
    }
}
