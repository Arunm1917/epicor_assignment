using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace student
{
    public partial class Form1 : Form
    {
        static string path = "Data Source=hp\\SQLEXPRESS;Initial Catalog=arun;Integrated Security=True";
        SqlConnection con = new SqlConnection(path);
        public Form1()
        {

            InitializeComponent();
        }
        public void datagrid()
        {
            string view = "select id,name,desp,email,dob from course12";
            SqlDataAdapter da = new SqlDataAdapter(view, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'arunDataSet1.course12' table. You can move, or remove it, as needed.
            this.course12TableAdapter.Fill(this.arunDataSet1.course12);

   
            datagrid();
           
        }
        private void cmdsave_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "Insert into course12(id,name,desp,email,dob) values ('" + txtid.Text.ToString().Trim() + "','" + txtname.Text.ToString().Trim() + "','" + txtdesp.Text.ToString().Trim() + "','" + txtemail.Text.ToString().Trim() + "','" + txtdob.Text.ToString().Trim() + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            
            cmd.ExecuteNonQuery();
            MessageBox.Show("INserted");
            datagrid();
            con.Close();
            txtid.Enabled = true;
            txtid.Clear();
            txtname.Clear();
            txtdesp.Clear();
            txtemail.Clear();
            txtdob.Clear();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;

            txtid.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtname.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtdesp.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtemail.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtdob.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtid.Enabled = false;
        }

        private void cmdclose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtdob_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtdob_Validating(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex(@"^([0-2]\d)-([0-2]\d)-(\d{4})$");
            Match m = reg.Match(txtdob.Text);
            if (m.Success)
            {
                int dd = int.Parse(m.Groups[1].Value);
                int mm = int.Parse(m.Groups[2].Value);
                int yyyy = int.Parse(m.Groups[3].Value);
                e.Cancel = dd < 1 || dd > 31 || mm < 1 || mm > 12 || yyyy > 2012;
            }
            else e.Cancel = true;
            if (e.Cancel)
            {
                if (MessageBox.Show("Wrong date format. The correct format is dd-mm-yyyy\n+ dd should be between 1 and 31.\n+ mm should be between 1 and 12.\n+ yyyy should be before 2013", "Invalid date", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    e.Cancel = false;
            }
        }


        

        

    }
}
