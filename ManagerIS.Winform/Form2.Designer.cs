namespace ManagerIS.Winform {
    partial class Form2 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tbxDZJGH = new System.Windows.Forms.TextBox();
            this.tbxXMMC = new System.Windows.Forms.TextBox();
            this.tbxGDMJ = new System.Windows.Forms.TextBox();
            this.tbxDGMJ = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbxBZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxTDYT = new System.Windows.Forms.TextBox();
            this.tbxTDZL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxDZJGH
            // 
            this.tbxDZJGH.Location = new System.Drawing.Point(108, 78);
            this.tbxDZJGH.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxDZJGH.Name = "tbxDZJGH";
            this.tbxDZJGH.Size = new System.Drawing.Size(260, 35);
            this.tbxDZJGH.TabIndex = 0;
            // 
            // tbxXMMC
            // 
            this.tbxXMMC.Location = new System.Drawing.Point(380, 76);
            this.tbxXMMC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxXMMC.Name = "tbxXMMC";
            this.tbxXMMC.Size = new System.Drawing.Size(292, 35);
            this.tbxXMMC.TabIndex = 1;
            // 
            // tbxGDMJ
            // 
            this.tbxGDMJ.Location = new System.Drawing.Point(108, 162);
            this.tbxGDMJ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxGDMJ.Name = "tbxGDMJ";
            this.tbxGDMJ.Size = new System.Drawing.Size(260, 35);
            this.tbxGDMJ.TabIndex = 2;
            this.tbxGDMJ.Text = "0";
            // 
            // tbxDGMJ
            // 
            this.tbxDGMJ.Location = new System.Drawing.Point(396, 162);
            this.tbxDGMJ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxDGMJ.Name = "tbxDGMJ";
            this.tbxDGMJ.Size = new System.Drawing.Size(276, 35);
            this.tbxDGMJ.TabIndex = 3;
            this.tbxDGMJ.Text = "0";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(552, 366);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(124, 48);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbxBZ
            // 
            this.tbxBZ.Location = new System.Drawing.Point(108, 324);
            this.tbxBZ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxBZ.Multiline = true;
            this.tbxBZ.Name = "tbxBZ";
            this.tbxBZ.Size = new System.Drawing.Size(386, 102);
            this.tbxBZ.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "电子监管号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(380, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "用地单位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "出让面积";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(396, 128);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "带供面积";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 324);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 24);
            this.label5.TabIndex = 10;
            this.label5.Text = "备注";
            // 
            // tbxTDYT
            // 
            this.tbxTDYT.Location = new System.Drawing.Point(112, 253);
            this.tbxTDYT.Name = "tbxTDYT";
            this.tbxTDYT.Size = new System.Drawing.Size(256, 35);
            this.tbxTDYT.TabIndex = 11;
            // 
            // tbxTDZL
            // 
            this.tbxTDZL.Location = new System.Drawing.Point(396, 253);
            this.tbxTDZL.Name = "tbxTDZL";
            this.tbxTDZL.Size = new System.Drawing.Size(276, 35);
            this.tbxTDZL.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(108, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "土地用途";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(400, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 24);
            this.label7.TabIndex = 14;
            this.label7.Text = "土地坐落";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxTDZL);
            this.Controls.Add(this.tbxTDYT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxBZ);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbxDGMJ);
            this.Controls.Add(this.tbxGDMJ);
            this.Controls.Add(this.tbxXMMC);
            this.Controls.Add(this.tbxDZJGH);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxDZJGH;
        private System.Windows.Forms.TextBox tbxXMMC;
        private System.Windows.Forms.TextBox tbxGDMJ;
        private System.Windows.Forms.TextBox tbxDGMJ;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbxBZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxTDYT;
        private System.Windows.Forms.TextBox tbxTDZL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}