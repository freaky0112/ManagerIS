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
            this.dgvGDDK = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGDDK)).BeginInit();
            this.SuspendLayout();
            // 
            // lswNZY
            // 
            this.lswNZY.Location = new System.Drawing.Point(11, 11);
            this.lswNZY.Margin = new System.Windows.Forms.Padding(2);
            this.lswNZY.Name = "lswNZY";
            this.lswNZY.Size = new System.Drawing.Size(705, 102);
            this.lswNZY.TabIndex = 4;
            this.lswNZY.UseCompatibleStateImageBehavior = false;
            this.lswNZY.View = System.Windows.Forms.View.Details;
            this.lswNZY.SelectedIndexChanged += new System.EventHandler(this.lswNZY_SelectedIndexChanged);
            // 
            // lswDK
            // 
            this.lswDK.Location = new System.Drawing.Point(11, 117);
            this.lswDK.Margin = new System.Windows.Forms.Padding(2);
            this.lswDK.Name = "lswDK";
            this.lswDK.Size = new System.Drawing.Size(185, 356);
            this.lswDK.TabIndex = 5;
            this.lswDK.UseCompatibleStateImageBehavior = false;
            this.lswDK.View = System.Windows.Forms.View.Details;
            this.lswDK.SelectedIndexChanged += new System.EventHandler(this.lswDK_SelectedIndexChanged);
            // 
            // lswGDDK
            // 
            this.lswGDDK.Location = new System.Drawing.Point(200, 117);
            this.lswGDDK.Margin = new System.Windows.Forms.Padding(2);
            this.lswGDDK.Name = "lswGDDK";
            this.lswGDDK.Size = new System.Drawing.Size(516, 122);
            this.lswGDDK.TabIndex = 6;
            this.lswGDDK.UseCompatibleStateImageBehavior = false;
            this.lswGDDK.View = System.Windows.Forms.View.Details;
            // 
            // dgvGDDK
            // 
            this.dgvGDDK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGDDK.Location = new System.Drawing.Point(201, 244);
            this.dgvGDDK.Name = "dgvGDDK";
            this.dgvGDDK.RowTemplate.Height = 23;
            this.dgvGDDK.Size = new System.Drawing.Size(515, 146);
            this.dgvGDDK.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 484);
            this.Controls.Add(this.dgvGDDK);
            this.Controls.Add(this.lswGDDK);
            this.Controls.Add(this.lswDK);
            this.Controls.Add(this.lswNZY);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGDDK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lswNZY;
        private System.Windows.Forms.ListView lswDK;
        private System.Windows.Forms.ListView lswGDDK;
        private System.Windows.Forms.DataGridView dgvGDDK;
    }
}

