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
using System.IO;

namespace WindowsFormsApplication
{
    public partial class FormDetailView : Form
    {
        private SQLiteConnection db;
        private int id;
        private string desc;
        private string comment;

        public FormDetailView(SQLiteConnection db, int id, string desc, string comment)
        {
            this.db = db;
            this.id = id;
            this.desc = desc;
            this.comment = comment;

            InitializeComponent();
        }

        private void FormDetailView_Load(object sender, EventArgs e)
        {
            getImage();
            richTextBox1.Text = desc;
        }



        private void getImage()
        {
           
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


        public Image ByteToImage(byte[] imageBytes)
        {
            // Convert byte[] to Image
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = new Bitmap(ms);
            return image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(comment);
        }
    }
}
