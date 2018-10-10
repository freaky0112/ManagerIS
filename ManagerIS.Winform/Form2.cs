using ManagerIS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerIS.Winform {
    public partial class Form2 : Form {
        public Form2(GDDK gddk,bool isNew) {
            InitializeComponent();
            this.gddk = gddk;
            if (!isNew) {
                tbxDZJGH.Text = gddk.Dzjgh.ToString();
                tbxXMMC.Text = gddk.Xmmc.ToString();
                tbxGDMJ.Text = gddk.Gdmj.ToString();
                tbxDGMJ.Text = gddk.Dgmj.ToString();
                tbxBZ.Text = gddk.Bz.ToString();
                tbxTDYT.Text = gddk.Tdyt.ToString();
                tbxTDZL.Text = gddk.Tdzl.ToString();
            }
        }
        GDDK gddk;
        private void btnOK_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(tbxXMMC.Text)) {
                MessageBox.Show("数据录入有误");
            } else {

                gddk.Dzjgh = tbxDZJGH.Text;
                gddk.Xmmc = tbxXMMC.Text;
                gddk.Gdmj = decimal.Parse(tbxGDMJ.Text);
                gddk.Dgmj = decimal.Parse(tbxDGMJ.Text);
                gddk.Bz = tbxBZ.Text;
                gddk.Tdzl = tbxTDZL.Text;
                gddk.Tdyt = tbxTDYT.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
