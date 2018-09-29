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
            this.SuspendLayout();
            // 
            // tbxDZJGH
            // 
            this.tbxDZJGH.Location = new System.Drawing.Point(109, 78);
            this.tbxDZJGH.Name = "tbxDZJGH";
            this.tbxDZJGH.Size = new System.Drawing.Size(100, 35);
            this.tbxDZJGH.TabIndex = 0;
            // 
            // tbxXMMC
            // 
            this.tbxXMMC.Location = new System.Drawing.Point(381, 77);
            this.tbxXMMC.Name = "tbxXMMC";
            this.tbxXMMC.Size = new System.Drawing.Size(100, 35);
            this.tbxXMMC.TabIndex = 1;
            // 
            // tbxGDMJ
            // 
            this.tbxGDMJ.Location = new System.Drawing.Point(109, 246);
            this.tbxGDMJ.Name = "tbxGDMJ";
            this.tbxGDMJ.Size = new System.Drawing.Size(100, 35);
            this.tbxGDMJ.TabIndex = 2;
            this.tbxGDMJ.Text = "0";
            // 
            // tbxDGMJ
            // 
            this.tbxDGMJ.Location = new System.Drawing.Point(395, 246);
            this.tbxDGMJ.Name = "tbxDGMJ";
            this.tbxDGMJ.Size = new System.Drawing.Size(100, 35);
            this.tbxDGMJ.TabIndex = 3;
            this.tbxDGMJ.Text = "0";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(551, 366);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(125, 47);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "button1";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbxBZ
            // 
            this.tbxBZ.Location = new System.Drawing.Point(109, 324);
            this.tbxBZ.Multiline = true;
            this.tbxBZ.Name = "tbxBZ";
            this.tbxBZ.Size = new System.Drawing.Size(386, 102);
            this.tbxBZ.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbxBZ);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbxDGMJ);
            this.Controls.Add(this.tbxGDMJ);
            this.Controls.Add(this.tbxXMMC);
            this.Controls.Add(this.tbxDZJGH);
            this.Name = "Form2";
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
    }
}