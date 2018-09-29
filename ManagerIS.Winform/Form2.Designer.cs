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
            this.SuspendLayout();
            // 
            // tbxDZJGH
            // 
            this.tbxDZJGH.Location = new System.Drawing.Point(54, 39);
            this.tbxDZJGH.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDZJGH.Name = "tbxDZJGH";
            this.tbxDZJGH.Size = new System.Drawing.Size(132, 21);
            this.tbxDZJGH.TabIndex = 0;
            // 
            // tbxXMMC
            // 
            this.tbxXMMC.Location = new System.Drawing.Point(190, 38);
            this.tbxXMMC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxXMMC.Name = "tbxXMMC";
            this.tbxXMMC.Size = new System.Drawing.Size(148, 21);
            this.tbxXMMC.TabIndex = 1;
            // 
            // tbxGDMJ
            // 
            this.tbxGDMJ.Location = new System.Drawing.Point(54, 123);
            this.tbxGDMJ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxGDMJ.Name = "tbxGDMJ";
            this.tbxGDMJ.Size = new System.Drawing.Size(52, 21);
            this.tbxGDMJ.TabIndex = 2;
            this.tbxGDMJ.Text = "0";
            // 
            // tbxDGMJ
            // 
            this.tbxDGMJ.Location = new System.Drawing.Point(198, 123);
            this.tbxDGMJ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDGMJ.Name = "tbxDGMJ";
            this.tbxDGMJ.Size = new System.Drawing.Size(52, 21);
            this.tbxDGMJ.TabIndex = 3;
            this.tbxDGMJ.Text = "0";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(276, 183);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbxBZ
            // 
            this.tbxBZ.Location = new System.Drawing.Point(54, 162);
            this.tbxBZ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxBZ.Multiline = true;
            this.tbxBZ.Name = "tbxBZ";
            this.tbxBZ.Size = new System.Drawing.Size(195, 53);
            this.tbxBZ.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "电子监管号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "用地单位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "出让面积";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "带供面积";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "备注";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 225);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
    }
}