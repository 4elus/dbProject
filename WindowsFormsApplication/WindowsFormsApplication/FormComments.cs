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

namespace WindowsFormsApplication
{
    public partial class FormComments : Form
    {

        private SQLiteConnection db;
        private DataGridView dgv;

        public FormComments(SQLiteConnection db)
        {
            this.db = db;

            InitializeComponent();
        }

        public FormComments(SQLiteConnection db, DataGridView dgv)
        {
            this.db = db;
            this.dgv = dgv;
            InitializeComponent();
        }

        private void FormComments_Load(object sender, EventArgs e)
        {
            output();
        }


        private void output() {
            if (dgv == null)
            {
                DataSet ds = new DataSet();

                string query = "SELECT * FROM Comments";

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, db);
                adapter.Fill(ds, "Comments");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Comments";
            }
            else {
                dataGridView1.DataSource = dgv.DataSource;
            }
        }
    }
}
