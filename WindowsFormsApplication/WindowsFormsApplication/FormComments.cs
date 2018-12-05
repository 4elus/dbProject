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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            output();

            
        }


        private void output() {
            if (dgv == null)
            {
                updateView();
            }
            else {
                dataGridView1.DataSource = dgv.DataSource;
            }
        }

        private void updateView() {
            DataSet ds = new DataSet();

            string query = "SELECT * FROM Comments";

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, db);
            adapter.Fill(ds, "Comments");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Comments";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());
            string text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            SQLiteCommand cmd = new SQLiteCommand("UPDATE Comments SET text = @txt  WHERE id = " + id, db);
            cmd.Parameters.Add("@txt", DbType.String).Value = text;
            cmd.ExecuteNonQuery();

            updateView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Comments WHERE id = " + id, db);

            cmd.ExecuteNonQuery();
            updateView();
        }
    }
}
