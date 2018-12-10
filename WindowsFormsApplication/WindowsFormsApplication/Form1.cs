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
        private DataTable dtFile;
        private List<int> list = new List<int>();

        private Color activeColor = Color.Gray;
        private Color passiveColor = Color.White;
        private string pathToImage, pathToComments;
        private DataGridView dataGridViewComments;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            //DataGridViewCell.Style.WrapMode = DataGridViewTriState.True;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            setToolTip();
            

           connectDB();
           outputData();
           //loadToolTip();
           
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

        //private void loadToolTip() {
        //    com = new SQLiteCommand("SELECT tags FROM Tasks", db);
        //    SQLiteDataReader reader = com.ExecuteReader();

        //    int i = 0;
        //    while (reader.Read()) {
        //        for (int j = 0; j < dataGridView1.ColumnCount; j++)
        //            dataGridView1.Rows[i].Cells[j].ToolTipText = reader[0].ToString();
        //        i++;
        //    }
        //}

        // =============================================== ПРИ ЗАКРЫТИИ ФОРМЫ ОТКЛЮЧАЕМСЯ ОТ БД  ===============================================
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Close();
        }

  
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());

            if (e.ColumnIndex == 1)
            {
                string que = "SELECT text FROM Comments WHERE code_tasks=" + id;
                SQLiteCommand com = new SQLiteCommand(que, db);

                IDataReader read = com.ExecuteReader();

                string res = "";

                while (read.Read())
                {
                    res += read.GetValue(0).ToString() + "\n";
                }

                read.Close();

             
                //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString() + "\n\n" + res);

                //FormDetailView formDetailView = new FormDetailView(db, id, dataGridView1.CurrentCell.Value.ToString(), res);
                //formDetailView.Show();
                getImage();
                richTextBoxDesc.Text = dataGridView1.CurrentCell.Value.ToString();

                

            }
            else if (e.ColumnIndex == 0)
            {
                list.Add(Int32.Parse(dataGridView1.CurrentCell.Value.ToString()));
            }
            //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

            

            string query = "SELECT photo FROM Tasks WHERE id=" + id;
            
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
                        string name = Path.GetFileName(f.FullName);
                        if (name.Contains(".txt") || name.Contains(".xlsx"))
                        {
                            //findItherFiles(f.FullName);
                            pathToComments = f.FullName;
                        }
                        if (name.Contains(".png")) {

                            pathToImage = f.FullName;
                        }
                        
                    }
                    DirSearch(d);
                }

            }
            catch (System.Exception)
            {

            }
        }

        private void importExcel(string file) {
            DirSearch(Path.GetDirectoryName(file));
            Image img = null;

            try
            {
                img = Image.FromFile(pathToImage);
            }
            catch (System.ArgumentNullException) { }

            ExcelObj.Application app = new ExcelObj.Application();
            ExcelObj.Workbook workbook;
            ExcelObj.Worksheet NwSheet;
            ExcelObj.Range ShtRange;
            DataTable dt = new DataTable();

            workbook = app.Workbooks.Open(file, Missing.Value,
               Missing.Value, Missing.Value, Missing.Value, Missing.Value,
               Missing.Value, Missing.Value, Missing.Value, Missing.Value,
               Missing.Value, Missing.Value, Missing.Value, Missing.Value,
               Missing.Value);

            //Устанавливаем номер листа из котрого будут извлекаться данные
            //Листы нумеруются от 1
            NwSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);
            ShtRange = NwSheet.UsedRange;
            bool key = false;

            for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
            {


                try
                {
                    dt.Columns.Add(
                   new DataColumn((ShtRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString()));
                }
                catch (Exception) { break; }
                key = true;
            }
            dt.AcceptChanges();

            if (!(key))
                dt.Columns.Add();

            string[] columnNames = new String[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                // Получам текст задачи
                
                columnNames[0] = dt.Columns[i].ColumnName;

                if ((columnNames[0] != null || columnNames[0] != "") && (pathToImage == null)) {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Tasks(desc_short) VALUES(@desc_short)", db);
                    cmd.Parameters.Add("@desc_short", DbType.String).Value = columnNames[0].ToString();
                    cmd.ExecuteNonQuery();
                }
                else if ((columnNames[0] == "" || columnNames[0] == "Column1") && (pathToImage != null))
                {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Tasks(photo) VALUES(@photo)", db);
                    cmd.Parameters.Add("@photo", DbType.Binary, 8000).Value = imageToByteArray(img);
                    cmd.ExecuteNonQuery();
                }
                else {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Tasks(desc_short, photo) VALUES(@desc_short, @photo)", db);
                    cmd.Parameters.Add("@desc_short", DbType.String).Value = columnNames[0].ToString();
                    cmd.Parameters.Add("@photo", DbType.Binary, 8000).Value = imageToByteArray(img);
                    cmd.ExecuteNonQuery();
                }
             
                
            }
            app.Quit();


            if (pathToComments != null) {
                //________________ Получаем комментарии
                dt = new DataTable();
                workbook = app.Workbooks.Open(pathToComments, Missing.Value,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value);

                //Устанавливаем номер листа из котрого будут извлекаться данные
                //Листы нумеруются от 1
                NwSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);
                ShtRange = NwSheet.UsedRange;
                string str1 = ""; string str2 = "";

                for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
                {
                    dt.Columns.Add(
                       new DataColumn((ShtRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString()));

                    if (Cnum == 1)
                        str1 = (ShtRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString();
                    else
                        str2 = (ShtRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString();
                }

                SQLiteCommand com = new SQLiteCommand("INSERT INTO Comments(code_tasks, text) VALUES(@code_tasks, @text)", db);
                com.Parameters.Add("@code_tasks", DbType.String).Value = str1;
                com.Parameters.Add("@text", DbType.String).Value = str2;
                com.ExecuteNonQuery();


                dt.AcceptChanges();

                string[] columnNames1 = new String[dt.Columns.Count];
                for (int i1 = 0; i1 < dt.Columns.Count; i1++)
                {
                    // Получам текст задачи
                    columnNames1[0] = dt.Columns[i1].ColumnName;
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

                    com = new SQLiteCommand("INSERT INTO Comments(code_tasks, text) VALUES(@code_tasks, @text)", db);
                    com.Parameters.Add("@code_tasks", DbType.String).Value = dr[0].ToString();
                    com.Parameters.Add("@text", DbType.String).Value = dr[1].ToString();
                    com.ExecuteNonQuery();

                    dt.Rows.Add(dr);

                    dt.AcceptChanges();
                }

                app.Quit();
            }

        }

        private void importTxt(string file) {
            //dtFile.Columns.Add("id");
            //dtFile.Columns.Add("text");
            //dtFile.Columns.Add("col1", typeof(byte[]));
            DirSearch(Path.GetDirectoryName(file));
            Image img = Image.FromFile(pathToImage);
            bool key = false;

            foreach (var line in File.ReadLines(file))
            {
                if (pathToImage != null)
                {
                    var array = line.Split('\t');
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Tasks(desc_short, photo) VALUES(@desc_short, @photo)", db);

                    try
                    {
                        cmd.Parameters.Add("@desc_short", DbType.String).Value = array[0].ToString();
                    }
                    catch (System.ArgumentOutOfRangeException) { }

                    cmd.Parameters.Add("@photo", DbType.Binary, 8000).Value = imageToByteArray(img);
                    cmd.ExecuteNonQuery();
                }
                else {
                    var array = line.Split('\t');
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Tasks(desc_short) VALUES(@desc_short)", db);
                    cmd.Parameters.Add("@desc_short", DbType.String).Value = array[1].ToString();
                    cmd.ExecuteNonQuery();
                }
                key = true;
            }


            if (!(key)) {
                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Tasks(photo) VALUES(@photo)", db);
                cmd.Parameters.Add("@photo", DbType.Binary, 8000).Value = imageToByteArray(img);
                cmd.ExecuteNonQuery();
            }

            // Если есть хоть один комментарий считываем
            foreach (var line in File.ReadLines(pathToComments)) {
                if (pathToComments != null)
                {
                    var array = line.Split('\t');
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Comments(code_tasks, text) VALUES(@code_tasks, @text)", db);
                    cmd.Parameters.Add("@code_tasks", DbType.String).Value = array[0].ToString();
                    cmd.Parameters.Add("@text", DbType.String).Value = array[1].ToString();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
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

        private void pictSearch_Click(object sender, EventArgs e)
        {
            txtBoxSearch.Visible = true;
            btnSearch.Visible = true;
            groupBoxCategory.Visible = true;

            pictBoxImageView.Visible = false;
            richTextBoxDesc.Visible = false;
        }

        private void pictBoxReload_Click(object sender, EventArgs e)
        {
            outputData();
        }

        private void pictBoxAdd_Click(object sender, EventArgs e)
        {
             //OpenFileDialog openFileDialog1 = new OpenFileDialog();

            var desc = Interaction.InputBox("Описание задачи", "Описание", "", -1, -1);

            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Tasks(desc_short) VALUES(@desc_short)", db);
            cmd.Parameters.Add("@desc_short", DbType.String).Value = desc.ToString();

            cmd.ExecuteNonQuery();

            outputData();
        }

        private void pictBoxRemove_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());

            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Tasks WHERE id = " + id, db);
            cmd.ExecuteNonQuery();
            outputData();
        }

        private void pictBoxEdit_Click(object sender, EventArgs e)
        {
           
            int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());
           
            var editText = Interaction.InputBox("Введите исправления", "Редактирование", "", -1, -1);
           
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Tasks SET desc_short = @txt  WHERE id = " + id, db);
            cmd.Parameters.Add("@txt", DbType.String).Value = editText;
            cmd.ExecuteNonQuery();

            outputData();
            
        }

        private void pictBoxImsertImage_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            SQLiteCommand cmd = new SQLiteCommand("UPDATE Tasks SET photo = @photo  WHERE id = " + id, db);
          
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] buf = ms.ToArray();

                cmd.Parameters.Add("@photo", DbType.Binary, 8000).Value = buf;

                cmd.ExecuteNonQuery();
            }

            outputData();
        }

        private void pictBoxExport_Click(object sender, EventArgs e)
        {
            FormExport export = new FormExport(dataGridView1, ds, db, list);
            export.Show();
        }

        private void pictBoxImport_Click(object sender, EventArgs e)
        {
            // txt files (*.txt)|*.txt|All files (*.*)|*.*
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel files(*.xlsx)|*.xlsx| Text files(*.txt)|*.txt";

            dtFile = new DataTable();


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.Contains(".txt"))
                {
                    importTxt(ofd.FileName);
                }
                if (ofd.FileName.Contains(".xlsx") || ofd.FileName.Contains(".xls"))
                {
                    importExcel(ofd.FileName);
                }
            }




            //OpenFileDialog ofd = new OpenFileDialog();
            ////Задаем расширение имени файла по умолчанию.
            //ofd.DefaultExt = "*.xls;*.xlsx";
            ////Задаем строку фильтра имен файлов, которая определяет
            ////варианты, доступные в поле "Файлы типа" диалогового
            ////окна.
            //ofd.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
            ////Задаем заголовок диалогового окна.
            //ofd.Title = "Выберите документ для загрузки данных";
            //ExcelObj.Application app = new ExcelObj.Application();
            //ExcelObj.Workbook workbook;
            //ExcelObj.Worksheet NwSheet;
            //ExcelObj.Range ShtRange;
            //DataTable dt = new DataTable();
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{


            //    workbook = app.Workbooks.Open(ofd.FileName, Missing.Value,
            //    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            //    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            //    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            //    Missing.Value);

            //    //Устанавливаем номер листа из котрого будут извлекаться данные
            //    //Листы нумеруются от 1
            //    NwSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);
            //    ShtRange = NwSheet.UsedRange;
            //    for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
            //    {
            //        dt.Columns.Add(
            //           new DataColumn((ShtRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString()));
            //    }
            //    dt.AcceptChanges();

            //    string[] columnNames = new String[dt.Columns.Count];
            //    for (int i = 0; i < dt.Columns.Count; i++)
            //    {
            //        // Получам текст задачи
            //        columnNames[0] = dt.Columns[i].ColumnName;
            //    }

            //    for (int Rnum = 2; Rnum <= ShtRange.Rows.Count; Rnum++)
            //    {
            //        DataRow dr = dt.NewRow();
            //        for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
            //        {
            //            if ((ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2 != null)
            //            {
            //                dr[Cnum - 1] =
            //    (ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString();
            //            }
            //        }
            //        dt.Rows.Add(dr);
            //        dt.AcceptChanges();
            //    }

            //    dataGridView1.DataSource = dt;
            //    app.Quit();
            //}

            //// Ищем оставшиеся файлы
            //string path = Path.GetDirectoryName(ofd.FileName);
            //DirSearch(path);
        }

        private void pictBoxDetailView_Click(object sender, EventArgs e)
        {
            pictBoxImageView.Visible = true;
            richTextBoxDesc.Visible = true;

            txtBoxSearch.Visible = false;
            btnSearch.Visible = false;
            groupBoxCategory.Visible = false;
        }



        private void setToolTip() {
            ToolTip add = new ToolTip();
            add.SetToolTip(pictBoxAdd, "Добавить");

            ToolTip remove = new ToolTip();
            remove.SetToolTip(pictBoxRemove, "Удалить");

            ToolTip edit = new ToolTip();
            edit.SetToolTip(pictBoxEdit, "Редактировать");

            ToolTip insertImage = new ToolTip();
            insertImage.SetToolTip(pictBoxImsertImage, "Вставить картинку");

            ToolTip reload = new ToolTip();
            reload.SetToolTip(pictBoxReload, "Обновить");

            ToolTip search = new ToolTip();
            search.SetToolTip(pictSearch, "Поиск");

            ToolTip detailView = new ToolTip();
            detailView.SetToolTip(pictBoxDetailView, "Полный обзор");

            ToolTip export = new ToolTip();
            export.SetToolTip(pictBoxExport, "Выгрузка");

            ToolTip import = new ToolTip();
            import.SetToolTip(pictBoxImport, "Загрузка");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string elec = "";
            string mech = "";

            if (checkBoxElec.Checked)
            {
                elec = checkBoxElec.Text;
            }
            if (checkBoxMech.Checked)
            {
                mech = checkBoxMech.Text;
            }

            string selectString = "";

            // string selectString = "address Like '%" + txtBoxFind.Text.Trim() + "%' and description_object Like '%" + txtBoxFindDesc.Text.Trim() + "%' and floor Like '%" + txtBoxFindFloor.Text.Trim() + "%' and price  Like '%" + txtBoxFindPrice.Text.Trim() + "%'";

            if (elec.Equals("") && mech.Equals(""))
            {
                selectString = "SELECT * FROM Tasks WHERE desc_short LIKE '%" + txtBoxSearch.Text + "%';";
            }
            else
            {
                selectString = "SELECT * FROM Tasks, Comments WHERE text LIKE '%" + mech + "%' AND text LIKE '%" + elec + "%' AND desc_short LIKE '%" + txtBoxSearch.Text + "%' AND Comments.code_tasks = Tasks.id";
            }

            ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectString, db);
            adapter.Fill(ds, "Tasks");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Tasks";

           
        }

        private void test(DataTable dt)
        {

            string sql1 = "DELETE FROM Tasks;";
            SQLiteCommand delete = new SQLiteCommand(sql1, db);
            delete.ExecuteNonQuery();

            //SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Comments(code_tasks, text) VALUES(@code, @txt)", db);
            //cmd.Parameters.Add("@code", DbType.UInt32).Value = id;
            //cmd.Parameters.Add("@txt", DbType.String).Value = comment.ToString();
            //cmd.ExecuteNonQuery();

            string sql = "INSERT INTO Tasks (category, desc_short) VALUES (@category, @desc_short)";
            SQLiteCommand insertCommand = new SQLiteCommand(sql, db);
            insertCommand.Parameters.Add(new SQLiteParameter("category"));
            insertCommand.Parameters.Add(new SQLiteParameter("desc_short"));
            foreach (DataRow originalRow in dt.Rows)
            {
                MessageBox.Show(originalRow[1].ToString());


                insertCommand.Parameters["category"].Value = originalRow[1].ToString();
                insertCommand.Parameters["desc_short"].Value = originalRow[2].ToString();
                try
                {
                    insertCommand.ExecuteNonQuery();
                }
                catch
                {
                    //...
                }
            }
        }

        private void getImage()
        {
            int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());
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

                        pictBoxImageView.Image = ByteToImage(a);
                    }
                }
                catch (Exception exc) { /*MessageBox.Show(exc.Message);*/ }
            }
            catch (Exception ex) { /*MessageBox.Show(ex.Message);*/ }
        }

        private void pictBoxAdd_MouseHover(object sender, EventArgs e)
        {
            pictBoxAdd.BackColor = activeColor;
        }

        private void pictBoxAdd_MouseLeave(object sender, EventArgs e)
        {
            pictBoxAdd.BackColor = passiveColor;
        }

        private void pictBoxRemove_MouseHover(object sender, EventArgs e)
        {
            pictBoxRemove.BackColor = activeColor;
        }

        private void pictBoxRemove_MouseLeave(object sender, EventArgs e)
        {
            pictBoxRemove.BackColor = passiveColor;
        }

        private void pictBoxEdit_MouseHover(object sender, EventArgs e)
        {
            pictBoxEdit.BackColor = activeColor;
        }

        private void pictBoxEdit_MouseLeave(object sender, EventArgs e)
        {
            pictBoxEdit.BackColor = passiveColor;
        }

        private void pictBoxImsertImage_MouseHover(object sender, EventArgs e)
        {
            pictBoxImsertImage.BackColor = activeColor;
        }

        private void pictBoxImsertImage_MouseLeave(object sender, EventArgs e)
        {
            pictBoxImsertImage.BackColor = passiveColor;
        }

        private void pictBoxReload_MouseHover(object sender, EventArgs e)
        {
            pictBoxReload.BackColor = activeColor;
        }

        private void pictBoxReload_MouseLeave(object sender, EventArgs e)
        {
            pictBoxReload.BackColor = passiveColor;
        }

        private void pictSearch_MouseHover(object sender, EventArgs e)
        {
            pictSearch.BackColor = activeColor;
        }

        private void pictSearch_MouseLeave(object sender, EventArgs e)
        {
            pictSearch.BackColor = passiveColor;
        }

        private void pictBoxDetailView_MouseHover(object sender, EventArgs e)
        {
            pictBoxDetailView.BackColor = activeColor;
        }

        private void pictBoxDetailView_MouseLeave(object sender, EventArgs e)
        {
            pictBoxDetailView.BackColor = passiveColor;
        }

        private void pictBoxExport_MouseHover(object sender, EventArgs e)
        {
            pictBoxExport.BackColor = activeColor;
        }

        private void pictBoxExport_MouseLeave(object sender, EventArgs e)
        {
            pictBoxExport.BackColor = passiveColor;
        }

        private void pictBoxImport_MouseHover(object sender, EventArgs e)
        {
            pictBoxImport.BackColor = activeColor;
        }

        private void pictBoxImport_MouseLeave(object sender, EventArgs e)
        {
            pictBoxImport.BackColor = passiveColor;
        }
    }
}
