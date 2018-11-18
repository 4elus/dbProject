namespace WindowsFormsApplication
{
    partial class FormExport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExport = new System.Windows.Forms.Button();
            this.radioBtnPDF = new System.Windows.Forms.RadioButton();
            this.radioBtnWord = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(240)))), ((int)(((byte)(36)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(97, 218);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(103, 35);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Выгрузка";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // radioBtnPDF
            // 
            this.radioBtnPDF.AutoSize = true;
            this.radioBtnPDF.Location = new System.Drawing.Point(35, 81);
            this.radioBtnPDF.Name = "radioBtnPDF";
            this.radioBtnPDF.Size = new System.Drawing.Size(57, 25);
            this.radioBtnPDF.TabIndex = 1;
            this.radioBtnPDF.TabStop = true;
            this.radioBtnPDF.Text = "PDF";
            this.radioBtnPDF.UseVisualStyleBackColor = true;
            // 
            // radioBtnWord
            // 
            this.radioBtnWord.AutoSize = true;
            this.radioBtnWord.Location = new System.Drawing.Point(35, 131);
            this.radioBtnWord.Name = "radioBtnWord";
            this.radioBtnWord.Size = new System.Drawing.Size(69, 25);
            this.radioBtnWord.TabIndex = 2;
            this.radioBtnWord.TabStop = true;
            this.radioBtnWord.Text = "Word";
            this.radioBtnWord.UseVisualStyleBackColor = true;
            // 
            // FormExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(295, 288);
            this.Controls.Add(this.radioBtnWord);
            this.Controls.Add(this.radioBtnPDF);
            this.Controls.Add(this.btnExport);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FormExport";
            this.Text = "Экспорт";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.RadioButton radioBtnPDF;
        private System.Windows.Forms.RadioButton radioBtnWord;
    }
}