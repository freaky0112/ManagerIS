namespace ManagerIS.Winform {
    partial class Form1 {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.lswNZY = new System.Windows.Forms.ListView();
            this.lswDK = new System.Windows.Forms.ListView();
            this.lswGDDK = new System.Windows.Forms.ListView();
            this.btnADD = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cbxCZFS = new System.Windows.Forms.ComboBox();
            this.tbxCZFS = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lswNZY
            // 
            this.lswNZY.Location = new System.Drawing.Point(22, 22);
            this.lswNZY.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lswNZY.Name = "lswNZY";
            this.lswNZY.Size = new System.Drawing.Size(1899, 457);
            this.lswNZY.TabIndex = 4;
            this.lswNZY.UseCompatibleStateImageBehavior = false;
            this.lswNZY.View = System.Windows.Forms.View.Details;
            this.lswNZY.SelectedIndexChanged += new System.EventHandler(this.lswNZY_SelectedIndexChanged);
            // 
            // lswDK
            // 
            this.lswDK.Location = new System.Drawing.Point(22, 487);
            this.lswDK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lswDK.Name = "lswDK";
            this.lswDK.Size = new System.Drawing.Size(367, 974);
            this.lswDK.TabIndex = 5;
            this.lswDK.UseCompatibleStateImageBehavior = false;
            this.lswDK.View = System.Windows.Forms.View.Details;
            this.lswDK.SelectedIndexChanged += new System.EventHandler(this.lswDK_SelectedIndexChanged);
            // 
            // lswGDDK
            // 
            this.lswGDDK.Location = new System.Drawing.Point(400, 487);
            this.lswGDDK.Margin = new System.Windows.Forms.Padding(4);
            this.lswGDDK.Name = "lswGDDK";
            this.lswGDDK.Size = new System.Drawing.Size(1028, 240);
            this.lswGDDK.TabIndex = 6;
            this.lswGDDK.UseCompatibleStateImageBehavior = false;
            this.lswGDDK.View = System.Windows.Forms.View.Details;
            this.lswGDDK.SelectedIndexChanged += new System.EventHandler(this.lswGDDK_SelectedIndexChanged);
            this.lswGDDK.DoubleClick += new System.EventHandler(this.lswGDDK_DoubleClick);
            // 
            // btnADD
            // 
            this.btnADD.Location = new System.Drawing.Point(1611, 537);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(51, 33);
            this.btnADD.TabIndex = 7;
            this.btnADD.Text = "+";
            this.btnADD.UseVisualStyleBackColor = true;
            this.btnADD.Click += new System.EventHandler(this.btnADD_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(1611, 604);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(51, 40);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cbxCZFS
            // 
            this.cbxCZFS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCZFS.FormattingEnabled = true;
            this.cbxCZFS.Items.AddRange(new object[] {
            "征地、拆迁未落实",
            "城市规划未确定或调整",
            "项目未落实",
            "已用未办理供地手续",
            "正在组织供地前期工作",
            "政府储备土地",
            "道路、安全间距、绿化等代征地以及无法利用的边角地、山地等",
            "其他（需明确具体原因）",
            "加快土地供应",
            "完善供地手续",
            "清理失效或撤回批文",
            "实施批文部分调整",
            "核减重复统计面积",
            "其他（需明确具体方式）"});
            this.cbxCZFS.Location = new System.Drawing.Point(396, 1056);
            this.cbxCZFS.Name = "cbxCZFS";
            this.cbxCZFS.Size = new System.Drawing.Size(1009, 32);
            this.cbxCZFS.TabIndex = 9;
            this.cbxCZFS.SelectedIndexChanged += new System.EventHandler(this.cbxCZFS_SelectedIndexChanged);
            // 
            // tbxCZFS
            // 
            this.tbxCZFS.Location = new System.Drawing.Point(396, 1003);
            this.tbxCZFS.Name = "tbxCZFS";
            this.tbxCZFS.Size = new System.Drawing.Size(1009, 35);
            this.tbxCZFS.TabIndex = 10;
            this.tbxCZFS.TextChanged += new System.EventHandler(this.tbxCZFS_TextChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(1235, 1343);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(193, 47);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "2018年12月31日前",
            "2019年6月30日前",
            "需长期推进"});
            this.comboBox1.Location = new System.Drawing.Point(396, 1125);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(351, 32);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1934, 1474);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.tbxCZFS);
            this.Controls.Add(this.cbxCZFS);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnADD);
            this.Controls.Add(this.lswGDDK);
            this.Controls.Add(this.lswDK);
            this.Controls.Add(this.lswNZY);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lswNZY;
        private System.Windows.Forms.ListView lswDK;
        private System.Windows.Forms.ListView lswGDDK;
        private System.Windows.Forms.Button btnADD;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox cbxCZFS;
        private System.Windows.Forms.TextBox tbxCZFS;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

