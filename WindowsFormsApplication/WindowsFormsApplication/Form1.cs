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

namespace WindowsFormsApplication
{
    public partial class Form1 : Form
    {
        // =============================================== БЛОК ОБЯЪЯВЛЕНИЯ ГЛОБАЛЬНЫХ ПЕРЕМЕННЫХ  ===============================================
        private SQLiteConnection db;
        private SQLiteCommand com;
        private DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            
           connectDB();
           outputData();
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
            //dataGridView1.Columns.RemoveAt(1);
            /*com = new SQLiteCommand(query, db);
            
            SQLiteDataReader reader = com.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while(reader.Read()){
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }
            reader.Close();

            foreach(string[] el in data){
                dataGridView1.Rows.Add(el);
            }*/
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

        private void btnExport_Click(object sender, EventArgs e)
        {
             //Объект документа пдф
            iTextSharp.text.Document doc =new iTextSharp.text.Document();
 
            //Создаем объект записи пдф-документа в файл
            PdfWriter.GetInstance(doc, new FileStream("pdfTables.pdf", FileMode.Create));
 
            //Открываем документ
            doc.Open();
 
            //Определение шрифта необходимо для сохранения кириллического текста
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
 
            //Обход по всем таблицам датасета 
            for(int i = 0;i< ds.Tables.Count;i++)
            {
                //Создаем объект таблицы и передаем в нее число столбцов таблицы из нашего датасета
                PdfPTable table =new PdfPTable(ds.Tables[i].Columns.Count);
 
                //Добавим в таблицу общий заголовок
                PdfPCell cell = new PdfPCell(new Phrase("БД Tasks", font));
 
                cell.Colspan = ds.Tables[i].Columns.Count;
                cell.HorizontalAlignment = 1;
                //Убираем границу первой ячейки, чтобы балы как заголовок
                cell.Border = 0;
                table.AddCell(cell);
 
                //Сначала добавляем заголовки таблицы
                for(int j = 0; j< ds.Tables[i].Columns.Count;j++)
                {
                    cell = new PdfPCell(new Phrase(new Phrase(ds.Tables[i].Columns[j].ColumnName, font)));
                    //Фоновый цвет (необязательно, просто сделаем по красивее)
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }
 
                //Добавляем все остальные ячейки
                for(int j = 0; j< ds.Tables[i].Rows.Count;j++)
                {
                    for(int k = 0; k< ds.Tables[i].Columns.Count;k++)
                    {
                        table.AddCell(new Phrase(ds.Tables[i].Rows[j][k].ToString(), font));
                    }
                }
                //Добавляем таблицу в документ
                doc.Add(table);
            }
            //Закрываем документ
            doc.Close();
 
            MessageBox.Show("Pdf-документ сохранен");
        }

        private void method() {
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
        
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // Вызываем окн проводник
            OpenFileDialog opfd = new OpenFileDialog();

            // Если выбрали, то загружаем
            if (opfd.ShowDialog(this) == DialogResult.OK) {
             
                if (opfd.FileName.EndsWith(".xlsx")) {
                    
                    OleDbConnection Excel = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=test.xlsx;
                    Extended Properties=""Excel 12.0 Xml;HDR=YES"";");

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Лист1$]", Excel); 
                    Excel.Open();
                    DataTable tableau = new DataTable();
                    OleDbDataReader Reader = cmd.ExecuteReader();
                    tableau.Load(Reader);
                    dataGridView1.DataSource = tableau;
                }
            }
        }
       
    }
}
