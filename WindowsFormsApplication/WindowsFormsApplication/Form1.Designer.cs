namespace WindowsFormsApplication
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBoxCategory = new System.Windows.Forms.GroupBox();
            this.checkBoxMech = new System.Windows.Forms.CheckBox();
            this.checkBoxElec = new System.Windows.Forms.CheckBox();
            this.txtBoxSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.pictBoxImageView = new System.Windows.Forms.PictureBox();
            this.richTextBoxDesc = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictBoxImport = new System.Windows.Forms.PictureBox();
            this.pictBoxExport = new System.Windows.Forms.PictureBox();
            this.pictBoxImsertImage = new System.Windows.Forms.PictureBox();
            this.pictBoxEdit = new System.Windows.Forms.PictureBox();
            this.pictBoxReload = new System.Windows.Forms.PictureBox();
            this.pictBoxRemove = new System.Windows.Forms.PictureBox();
            this.pictBoxAdd = new System.Windows.Forms.PictureBox();
            this.pictBoxDetailView = new System.Windows.Forms.PictureBox();
            this.pictSearch = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImageView)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImsertImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxReload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxDetailView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxCategory
            // 
            this.groupBoxCategory.Controls.Add(this.checkBoxMech);
            this.groupBoxCategory.Controls.Add(this.checkBoxElec);
            this.groupBoxCategory.Location = new System.Drawing.Point(3, 144);
            this.groupBoxCategory.Name = "groupBoxCategory";
            this.groupBoxCategory.Size = new System.Drawing.Size(169, 101);
            this.groupBoxCategory.TabIndex = 3;
            this.groupBoxCategory.TabStop = false;
            this.groupBoxCategory.Text = "Категории";
            // 
            // checkBoxMech
            // 
            this.checkBoxMech.AutoSize = true;
            this.checkBoxMech.Location = new System.Drawing.Point(7, 59);
            this.checkBoxMech.Name = "checkBoxMech";
            this.checkBoxMech.Size = new System.Drawing.Size(56, 25);
            this.checkBoxMech.TabIndex = 1;
            this.checkBoxMech.Text = "№2";
            this.checkBoxMech.UseVisualStyleBackColor = true;
            // 
            // checkBoxElec
            // 
            this.checkBoxElec.AutoSize = true;
            this.checkBoxElec.Location = new System.Drawing.Point(7, 28);
            this.checkBoxElec.Name = "checkBoxElec";
            this.checkBoxElec.Size = new System.Drawing.Size(56, 25);
            this.checkBoxElec.TabIndex = 0;
            this.checkBoxElec.Text = "№1";
            this.checkBoxElec.UseVisualStyleBackColor = true;
            // 
            // txtBoxSearch
            // 
            this.txtBoxSearch.Location = new System.Drawing.Point(23, 15);
            this.txtBoxSearch.Name = "txtBoxSearch";
            this.txtBoxSearch.Size = new System.Drawing.Size(205, 27);
            this.txtBoxSearch.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(151)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(234, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 27);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Поиск";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 56;
            this.dataGridView1.Size = new System.Drawing.Size(619, 365);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(63, 52);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(10, 10);
            this.dataGridView2.TabIndex = 6;
            this.dataGridView2.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1068, 457);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelSearch);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1060, 423);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.pictBoxImageView);
            this.panelSearch.Controls.Add(this.groupBoxCategory);
            this.panelSearch.Controls.Add(this.richTextBoxDesc);
            this.panelSearch.Controls.Add(this.txtBoxSearch);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSearch.Location = new System.Drawing.Point(628, 55);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(429, 365);
            this.panelSearch.TabIndex = 9;
            // 
            // pictBoxImageView
            // 
            this.pictBoxImageView.Location = new System.Drawing.Point(116, 0);
            this.pictBoxImageView.Name = "pictBoxImageView";
            this.pictBoxImageView.Size = new System.Drawing.Size(190, 160);
            this.pictBoxImageView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxImageView.TabIndex = 4;
            this.pictBoxImageView.TabStop = false;
            this.pictBoxImageView.Visible = false;
            // 
            // richTextBoxDesc
            // 
            this.richTextBoxDesc.Location = new System.Drawing.Point(10, 184);
            this.richTextBoxDesc.Name = "richTextBoxDesc";
            this.richTextBoxDesc.ReadOnly = true;
            this.richTextBoxDesc.Size = new System.Drawing.Size(404, 130);
            this.richTextBoxDesc.TabIndex = 1;
            this.richTextBoxDesc.Text = "";
            this.richTextBoxDesc.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Controls.Add(this.dataGridView2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(3, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(619, 365);
            this.panel3.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 294);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictBoxImport);
            this.panel2.Controls.Add(this.pictBoxExport);
            this.panel2.Controls.Add(this.pictBoxImsertImage);
            this.panel2.Controls.Add(this.pictBoxEdit);
            this.panel2.Controls.Add(this.pictBoxReload);
            this.panel2.Controls.Add(this.pictBoxRemove);
            this.panel2.Controls.Add(this.pictBoxAdd);
            this.panel2.Controls.Add(this.pictBoxDetailView);
            this.panel2.Controls.Add(this.pictSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1054, 52);
            this.panel2.TabIndex = 0;
            // 
            // pictBoxImport
            // 
            this.pictBoxImport.Image = ((System.Drawing.Image)(resources.GetObject("pictBoxImport.Image")));
            this.pictBoxImport.Location = new System.Drawing.Point(413, 3);
            this.pictBoxImport.Name = "pictBoxImport";
            this.pictBoxImport.Size = new System.Drawing.Size(45, 45);
            this.pictBoxImport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxImport.TabIndex = 8;
            this.pictBoxImport.TabStop = false;
            this.pictBoxImport.Click += new System.EventHandler(this.pictBoxImport_Click);
            this.pictBoxImport.MouseLeave += new System.EventHandler(this.pictBoxImport_MouseLeave);
            this.pictBoxImport.MouseHover += new System.EventHandler(this.pictBoxImport_MouseHover);
            // 
            // pictBoxExport
            // 
            this.pictBoxExport.Image = ((System.Drawing.Image)(resources.GetObject("pictBoxExport.Image")));
            this.pictBoxExport.Location = new System.Drawing.Point(362, 4);
            this.pictBoxExport.Name = "pictBoxExport";
            this.pictBoxExport.Size = new System.Drawing.Size(45, 45);
            this.pictBoxExport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxExport.TabIndex = 7;
            this.pictBoxExport.TabStop = false;
            this.pictBoxExport.Click += new System.EventHandler(this.pictBoxExport_Click);
            this.pictBoxExport.MouseLeave += new System.EventHandler(this.pictBoxExport_MouseLeave);
            this.pictBoxExport.MouseHover += new System.EventHandler(this.pictBoxExport_MouseHover);
            // 
            // pictBoxImsertImage
            // 
            this.pictBoxImsertImage.Image = ((System.Drawing.Image)(resources.GetObject("pictBoxImsertImage.Image")));
            this.pictBoxImsertImage.Location = new System.Drawing.Point(158, 4);
            this.pictBoxImsertImage.Name = "pictBoxImsertImage";
            this.pictBoxImsertImage.Size = new System.Drawing.Size(45, 45);
            this.pictBoxImsertImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxImsertImage.TabIndex = 6;
            this.pictBoxImsertImage.TabStop = false;
            this.pictBoxImsertImage.Click += new System.EventHandler(this.pictBoxImsertImage_Click);
            this.pictBoxImsertImage.MouseLeave += new System.EventHandler(this.pictBoxImsertImage_MouseLeave);
            this.pictBoxImsertImage.MouseHover += new System.EventHandler(this.pictBoxImsertImage_MouseHover);
            // 
            // pictBoxEdit
            // 
            this.pictBoxEdit.Image = ((System.Drawing.Image)(resources.GetObject("pictBoxEdit.Image")));
            this.pictBoxEdit.Location = new System.Drawing.Point(107, 4);
            this.pictBoxEdit.Name = "pictBoxEdit";
            this.pictBoxEdit.Size = new System.Drawing.Size(45, 45);
            this.pictBoxEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxEdit.TabIndex = 5;
            this.pictBoxEdit.TabStop = false;
            this.pictBoxEdit.Click += new System.EventHandler(this.pictBoxEdit_Click);
            this.pictBoxEdit.MouseLeave += new System.EventHandler(this.pictBoxEdit_MouseLeave);
            this.pictBoxEdit.MouseHover += new System.EventHandler(this.pictBoxEdit_MouseHover);
            // 
            // pictBoxReload
            // 
            this.pictBoxReload.Image = ((System.Drawing.Image)(resources.GetObject("pictBoxReload.Image")));
            this.pictBoxReload.Location = new System.Drawing.Point(209, 3);
            this.pictBoxReload.Name = "pictBoxReload";
            this.pictBoxReload.Size = new System.Drawing.Size(45, 45);
            this.pictBoxReload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxReload.TabIndex = 4;
            this.pictBoxReload.TabStop = false;
            this.pictBoxReload.Click += new System.EventHandler(this.pictBoxReload_Click);
            this.pictBoxReload.MouseLeave += new System.EventHandler(this.pictBoxReload_MouseLeave);
            this.pictBoxReload.MouseHover += new System.EventHandler(this.pictBoxReload_MouseHover);
            // 
            // pictBoxRemove
            // 
            this.pictBoxRemove.Image = ((System.Drawing.Image)(resources.GetObject("pictBoxRemove.Image")));
            this.pictBoxRemove.Location = new System.Drawing.Point(56, 3);
            this.pictBoxRemove.Name = "pictBoxRemove";
            this.pictBoxRemove.Size = new System.Drawing.Size(45, 45);
            this.pictBoxRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxRemove.TabIndex = 3;
            this.pictBoxRemove.TabStop = false;
            this.pictBoxRemove.Click += new System.EventHandler(this.pictBoxRemove_Click);
            this.pictBoxRemove.MouseLeave += new System.EventHandler(this.pictBoxRemove_MouseLeave);
            this.pictBoxRemove.MouseHover += new System.EventHandler(this.pictBoxRemove_MouseHover);
            // 
            // pictBoxAdd
            // 
            this.pictBoxAdd.Image = ((System.Drawing.Image)(resources.GetObject("pictBoxAdd.Image")));
            this.pictBoxAdd.Location = new System.Drawing.Point(5, 3);
            this.pictBoxAdd.Name = "pictBoxAdd";
            this.pictBoxAdd.Size = new System.Drawing.Size(45, 45);
            this.pictBoxAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxAdd.TabIndex = 2;
            this.pictBoxAdd.TabStop = false;
            this.pictBoxAdd.Click += new System.EventHandler(this.pictBoxAdd_Click);
            this.pictBoxAdd.MouseLeave += new System.EventHandler(this.pictBoxAdd_MouseLeave);
            this.pictBoxAdd.MouseHover += new System.EventHandler(this.pictBoxAdd_MouseHover);
            // 
            // pictBoxDetailView
            // 
            this.pictBoxDetailView.Image = ((System.Drawing.Image)(resources.GetObject("pictBoxDetailView.Image")));
            this.pictBoxDetailView.Location = new System.Drawing.Point(311, 3);
            this.pictBoxDetailView.Name = "pictBoxDetailView";
            this.pictBoxDetailView.Size = new System.Drawing.Size(45, 45);
            this.pictBoxDetailView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxDetailView.TabIndex = 1;
            this.pictBoxDetailView.TabStop = false;
            this.pictBoxDetailView.Click += new System.EventHandler(this.pictBoxDetailView_Click);
            this.pictBoxDetailView.MouseLeave += new System.EventHandler(this.pictBoxDetailView_MouseLeave);
            this.pictBoxDetailView.MouseHover += new System.EventHandler(this.pictBoxDetailView_MouseHover);
            // 
            // pictSearch
            // 
            this.pictSearch.BackColor = System.Drawing.Color.White;
            this.pictSearch.Image = ((System.Drawing.Image)(resources.GetObject("pictSearch.Image")));
            this.pictSearch.Location = new System.Drawing.Point(260, 4);
            this.pictSearch.Name = "pictSearch";
            this.pictSearch.Size = new System.Drawing.Size(45, 45);
            this.pictSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictSearch.TabIndex = 0;
            this.pictSearch.TabStop = false;
            this.pictSearch.Click += new System.EventHandler(this.pictSearch_Click);
            this.pictSearch.MouseLeave += new System.EventHandler(this.pictSearch_MouseLeave);
            this.pictSearch.MouseHover += new System.EventHandler(this.pictSearch_MouseHover);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1060, 431);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1068, 457);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Form1";
            this.Text = "Главая";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxCategory.ResumeLayout(false);
            this.groupBoxCategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImageView)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImsertImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxReload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxDetailView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtBoxSearch;
        private System.Windows.Forms.GroupBox groupBoxCategory;
        private System.Windows.Forms.CheckBox checkBoxMech;
        private System.Windows.Forms.CheckBox checkBoxElec;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictSearch;
        private System.Windows.Forms.PictureBox pictBoxImsertImage;
        private System.Windows.Forms.PictureBox pictBoxEdit;
        private System.Windows.Forms.PictureBox pictBoxReload;
        private System.Windows.Forms.PictureBox pictBoxRemove;
        private System.Windows.Forms.PictureBox pictBoxAdd;
        private System.Windows.Forms.PictureBox pictBoxDetailView;
        private System.Windows.Forms.PictureBox pictBoxExport;
        private System.Windows.Forms.PictureBox pictBoxImport;
        private System.Windows.Forms.RichTextBox richTextBoxDesc;
        private System.Windows.Forms.PictureBox pictBoxImageView;
    }
}

