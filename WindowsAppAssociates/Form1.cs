using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsAppAssociates
{
    public partial class Form1 : Form
    {
        
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Nisha Parikh\Desktop\DailyBoard.accdb");

        public Form1()
        {
            InitializeComponent();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into dailyBoard (Floor,Court,Client_Name,Client_Type,Case_Type,Case_Number,Last_Date,New_Date) values('" + txtFloor.Text + "','" + txtCourt.Text + "','" + txtClientName.Text + "','" + ClientType.SelectedItem.ToString() + "','" + txtCaseType.Text + "','" + txtCaseNumber.Text + "','" + txtLastDate.Value.Date.ToString() + "','"+txtNewDate.Value.Date.ToString()+"')";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Inserted");
        }

        private void View_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
         
            cmd.CommandText = "select * from dailyBoard where New_Date=#"+ ViewDateRecord.Value.Date.ToString() + "#";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
           
        }

        private void Print_Click(object sender, EventArgs e)
        {
            printDocument1.Print();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 10, 10);
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClientType.Items.Add("Plaintiff");
            ClientType.Items.Add("Applicant");
            ClientType.Items.Add("Petitioner");
            ClientType.Items.Add("Complainant");
            ClientType.Items.Add("Defendant");
            ClientType.Items.Add("Opponent");
            ClientType.Items.Add("Respondent");
            ClientType.Items.Add("Accused");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                txtFloor.Text = row.Cells["Floor"].Value.ToString();
                txtCourt.Text = row.Cells["Court"].Value.ToString();
                txtClientName.Text = row.Cells["Client_Name"].Value.ToString();
                ClientType.SelectedText = row.Cells["Client_Type"].Value.ToString();
                txtCaseType.Text = row.Cells["Case_Type"].Value.ToString();
                txtCaseNumber.Text = row.Cells["Case_Number"].Value.ToString();
                txtLastDate.Value =Convert.ToDateTime(row.Cells["Last_Date"].Value.ToString());
                txtNewDate.Value=Convert.ToDateTime(row.Cells["New_Date"].Value.ToString());

            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update dailyBoard SET Last_Date=#" + txtLastDate.Value.Date.ToString() + "#,Next_Date=#"+ txtNewDate.Value.Date.ToString() + "# where Case_Number='"+txtCaseNumber.Text+"'";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Record Updated");
        }
    }
}
