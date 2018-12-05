using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

//using iTextSharp;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.IO;
using System.Data.OleDb;
using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;

namespace WindowsFormsApplication
{
    public partial class Form1 : Form
    {
        // =============================================== БЛОК ОБЯЪЯВЛЕНИЯ ГЛОБАЛЬНЫХ ПЕРЕМЕННЫХ  ===============================================
        private SQLiteConnection db;
        private SQLiteCommand com;
        private DataSet ds;
        private List<int> list = new List<int>();


        private DataGridView dataGridViewComments;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            //DataGridViewCell.Style.WrapMode = DataGridViewTriState.True;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            
           connectDB();
           outputData();
           loadToolTip();
           dataGridView1.Columns.RemoveAt(3);
          //dataGridView1.Columns.RemoveAt(4);
        }

        // =============================================== МЕТОД ПОДКЛЮЧЕНИЯ К БД  ===============================================
        private void connectDB() {
            db = new SQLiteConnection("Data Source=TestDB.db; Version=3;");
            db.Open();
        }

        // =============================================== ВЫВОД ДАННЫХ ИЗ БД  ===============================================
        private void outputData() {
            string query = "SELECT * FROM Tasks";
            
            ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, db);
            adapter.Fill(ds, "Tasks");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Tasks";
        }

        private void loadToolTip() {
            com = new SQLiteCommand("SELECT tags FROM Tasks", db);
            SQLiteDataReader reader = com.ExecuteReader();

            int i = 0;
            while (reader.Read()) {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].ToolTipText = reader[0].ToString();
                i++;
            }
        }

        // =============================================== ПРИ ЗАКРЫТИИ ФОРМЫ ОТКЛЮЧАЕМСЯ ОТ БД  ===============================================
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAdd add = new FormAdd();
            add.Show();
        }

       

       /* private void method() {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                //cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdfTable.AddCell(cell.Value.ToString());
                }
            }
        
        }*/

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //Задаем расширение имени файла по умолчанию.
            ofd.DefaultExt = "*.xls;*.xlsx";
            //Задаем строку фильтра имен файлов, которая определяет
            //варианты, доступные в поле "Файлы типа" диалогового
            //окна.
            ofd.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
            //Задаем заголовок диалогового окна.
            ofd.Title = "Выберите документ для загрузки данных";
            ExcelObj.Application app = new ExcelObj.Application();
            ExcelObj.Workbook workbook;
            ExcelObj.Worksheet NwSheet;
            ExcelObj.Range ShtRange;
            DataTable dt = new DataTable();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //textBox1.Text = ofd.FileName;

                workbook = app.Workbooks.Open(ofd.FileName, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value);

                //Устанавливаем номер листа из котрого будут извлекаться данные
                //Листы нумеруются от 1
                NwSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);
                ShtRange = NwSheet.UsedRange;
                for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
                {
                    dt.Columns.Add(
                       new DataColumn((ShtRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString()));
                }
                dt.AcceptChanges();

                string[] columnNames = new String[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    columnNames[0] = dt.Columns[i].ColumnName;
                }

                for (int Rnum = 2; Rnum <= ShtRange.Rows.Count; Rnum++)
                {
                    DataRow dr = dt.NewRow();
                    for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
                    {
                        if ((ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2 != null)
                        {
                            dr[Cnum - 1] =
                (ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }

                dataGridView1.DataSource = dt;
                app.Quit();
            }
            
            // Ищем оставшиеся файлы
            string path = Path.GetDirectoryName(ofd.FileName);
            DirSearch(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormExport export = new FormExport(dataGridView1, ds, db, list);
            export.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());

            if (e.ColumnIndex == 2)
            {
                string que = "SELECT text FROM Comments WHERE code_tasks=" + id;
                SQLiteCommand com = new SQLiteCommand(que, db);

                IDataReader read = com.ExecuteReader();

                string res = "";

                while (read.Read()) {
                    res += read.GetValue(0).ToString() + "\n";
                }

                read.Close();

             
                MessageBox.Show(dataGridView1.CurrentCell.Value.ToString() + "\n\n" + res);
            }
            else if (e.ColumnIndex == 0)
            {
                list.Add(Int32.Parse(dataGridView1.CurrentCell.Value.ToString()));
            }
            //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

            

            string query = "SELECT photo FROM Tasks WHERE id=" + id;
            
           
            SQLiteCommand cmd = new SQLiteCommand(query, db);

            try
            {
                IDataReader rdr = cmd.ExecuteReader();
                try
                {
                    while (rdr.Read())
                    {
                        byte[] a = (System.Byte[])rdr[0];
                        pictBoxViewImage.Image = ByteToImage(a);
                    }
                }
                catch (Exception exc) { /*MessageBox.Show(exc.Message);*/ }
            }
            catch (Exception ex) { /*MessageBox.Show(ex.Message);*/ }

        }

        private void btnDownloadImage_Click(object sender, EventArgs e)
        {
            // Получаем id 
            int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());


            if (id != null || id != 0)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] buf = ms.ToArray();

                    //com = new SQLiteCommand("UPDATE Tasks SET photo = " + buf + " WHERE id = " + id, db);
                    //com.ExecuteNonQuery();

                   
                    SQLiteCommand cmd = new SQLiteCommand("UPDATE Tasks SET photo = @photo  WHERE id = " + id, db);
                    cmd.Parameters.Add("@photo", DbType.Binary, 8000).Value = buf;
                    cmd.ExecuteNonQuery();

                    /*
                     command.CommandText =
                    "update Example set Info = :info, Text = :text where ID=:id";
                command.Parameters.Add("info", DbType.String).Value = textBox2.Text; 
                command.Parameters.Add("text", DbType.String).Value = textBox3.Text; 
                command.Parameters.Add("id", DbType.String).Value = textBox1.Text; 
                command.ExecuteNonQuery();
                     */
                }
            }
            else {
                MessageBox.Show("Выберите ячейку !");
            }
        }

        public Image ByteToImage(byte[] imageBytes) {
            // Convert byte[] to Image
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = new Bitmap(ms);
            return image;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());
                var comment = Interaction.InputBox("Message", "Title", "", -1, -1);


                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Comments(code_tasks, text) VALUES(@code, @txt)", db);
                cmd.Parameters.Add("@code", DbType.UInt32).Value = id;
                cmd.Parameters.Add("@txt", DbType.String).Value = comment.ToString();
                cmd.ExecuteNonQuery();
            }
        }


        void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {

                   // MessageBox.Show(d);

                    DirectoryInfo directory = new DirectoryInfo(d);
                    FileInfo[] files = directory.GetFiles();

                    var filtered = files.Select(f => f)
                                        .Where(f => (f.Attributes & FileAttributes.Hidden) == 0);

                    foreach (var f in filtered)
                    {
                        string name = Path.GetFileNameWithoutExtension(f.FullName);
                        if (name.Equals("comments")) {
                            findItherFiles(f.FullName);
                            
                        }
                        
                    }
                    DirSearch(d);
                }

            }
            catch (System.Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


            if (dataGridViewComments == null)
            {
                FormComments comments = new FormComments(db);
                comments.Show();
            }
            else {
                FormComments comments = new FormComments(db, dataGridViewComments);
                comments.Show();
            }

        }


        private void findItherFiles(string ofd) {
            
            ExcelObj.Application app = new ExcelObj.Application();
            ExcelObj.Workbook workbook;
            ExcelObj.Worksheet NwSheet;
            ExcelObj.Range ShtRange;
            DataTable dt = new DataTable();
           
                //textBox1.Text = ofd.FileName;

                workbook = app.Workbooks.Open(ofd, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value);

                //Устанавливаем номер листа из котрого будут извлекаться данные
                //Листы нумеруются от 1
                NwSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);
                ShtRange = NwSheet.UsedRange;
                for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
                {
                    dt.Columns.Add(
                       new DataColumn((ShtRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString()));
                }
                dt.AcceptChanges();

                string[] columnNames = new String[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    columnNames[0] = dt.Columns[i].ColumnName;
                }

                for (int Rnum = 2; Rnum <= ShtRange.Rows.Count; Rnum++)
                {
                    DataRow dr = dt.NewRow();
                    for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
                    {
                        if ((ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2 != null)
                        {
                            dr[Cnum - 1] =
                (ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
                dataGridViewComments = new DataGridView();
                dataGridView2.DataSource = dt;
                dataGridViewComments.DataSource = dataGridView2.DataSource;
                app.Quit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string elec = "";
            string mech = "";

            if (checkBoxElec.Checked) {
                elec = checkBoxElec.Text;
            }
            if (checkBoxMech.Checked) {
                mech = checkBoxMech.Text;
            }

            string selectString = "";

            // string selectString = "address Like '%" + txtBoxFind.Text.Trim() + "%' and description_object Like '%" + txtBoxFindDesc.Text.Trim() + "%' and floor Like '%" + txtBoxFindFloor.Text.Trim() + "%' and price  Like '%" + txtBoxFindPrice.Text.Trim() + "%'";

            if (elec.Equals("") && mech.Equals(""))
            {
                selectString = "SELECT * FROM Tasks WHERE desc_short LIKE '" + txtBoxSearch.Text + "%';";
            }
            else {
                selectString = "SELECT * FROM Tasks WHERE category LIKE '" + mech + "' OR category LIKE '" + elec + "' AND desc_short LIKE '" + txtBoxSearch.Text + "%'";
            }

            ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectString, db);
            adapter.Fill(ds, "Tasks");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Tasks";
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            outputData();
        }
    }
}
