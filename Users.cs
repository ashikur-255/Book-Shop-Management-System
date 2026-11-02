using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopTuto
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookShopDb;Integrated Security=True;");
      
        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersDGV.DataSource = ds.Tables[0];
            Con.Close();

        }

       
        private void SaveBtn_Click_1(object sender, EventArgs e)
        {
            if (UName.Text == "" || UPhone.Text == "" || UAdd.Text == "" || UPass.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into UserTbl values('" + UName.Text + "','" + UPhone.Text + "','" + UAdd.Text + "','" + UPass.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added Successfully");
                    Con.Close();
                    populate();
                    //ResetBtn.Enabled = true;
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UName.Text == "" || UPhone.Text == "" || UAdd.Text == "" || UPass.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update UserTbl set UName='" + UName.Text + "',UPhone='" + UPhone.Text + "',UAdd='" + UAdd.Text + "',UPass='" + UPass.Text + "' where UId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully");
                    Con.Close();
                    populate();
                    //ResetBtn.Enabled = true;
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (UName.Text == "" || UPhone.Text == "" || UAdd.Text == "" || UPass.Text == "") 
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from UserTbl where UId=" + key + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully");
                    Con.Close();
                    populate();
                    //ResetBtn.Enabled = true;
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void UsersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow row = UsersDGV.Rows[e.RowIndex];

            //UName.Text = row.Cells[1].Value.ToString();
            //UPhone.Text = row.Cells[2].Value.ToString();
            //UAdd.Text = row.Cells[3].Value.ToString();
            //UPass.Text = row.Cells[4].Value.ToString();

            UName.Text = UsersDGV.SelectedRows[0].Cells[1].Value.ToString();
            UPhone.Text = UsersDGV.SelectedRows[0].Cells[2].Value.ToString();
            UAdd.Text = UsersDGV.SelectedRows[0].Cells[3].Value.ToString();
            UPass.Text = UsersDGV.SelectedRows[0].Cells[4].Value.ToString();


            if (UName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UsersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }


        public void Reset()
        {
            UName.Text = "";
            UPhone.Text = "";
            UAdd.Text = "";
            UPass.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Books Obj = new Books();
            Obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DashBoard Obj = new DashBoard();
            Obj.Show();
            this.Hide();
        }
    }
}
