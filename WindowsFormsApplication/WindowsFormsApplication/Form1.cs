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

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.OleDb;
using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApplication
{
    public partial class Form1 : Form
    {
        // =============================================== БЛОК ОБЯЪЯВЛЕНИЯ ГЛОБАЛЬНЫХ ПЕРЕМЕННЫХ  ===============================================
        private SQLiteConnection db;
        private SQLiteCommand com;
        private DataSet ds;
        private List<int> list = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            //DataGridViewCell.Style.WrapMode = DataGridViewTriState.True;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            
           connectDB();
           outputData();
           loadToolTip();
           dataGridView1.Columns.RemoveAt(3);
           
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
            else
                Application.Exit();
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
            if (e.ColumnIndex == 2)
            {
                MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
            }
            else if (e.ColumnIndex == 0)
            {
                list.Add(Int32.Parse(dataGridView1.CurrentCell.Value.ToString()));
            }
            //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
           
        }

       
       
    }
}
