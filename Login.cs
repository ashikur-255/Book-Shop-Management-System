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
using System.Xml.Linq;

namespace BookShopTuto
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookShopDb;Integrated Security=True;");
        public static string UserName = "";
        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from UserTbl  Where UName = '" + UsernameTB.Text + "' and UPass= '" + PasswordTB.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1") 
            {
              
                MessageBox.Show("Login Successful!");
                UserName = UsernameTB.Text;
                Billing_Interface bi = new Billing_Interface();
                bi.Show();
                this.Hide();
                Con.Close();
            }
            else
            {
                MessageBox.Show("Wrong Username Or Password!");
            }
            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (UsernameTB.Text == "" || PasswordTB.Text == "")
            {
                MessageBox.Show("Enter Your Username And Password");
                return;
            }
            try
            {
                Con.Open();
                string query = "SELECT * FROM UsersLogin Where Username =@Uname AND Password =@Pass";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@Uname", UsernameTB.Text);
                cmd.Parameters.AddWithValue("@Pass", PasswordTB.Text);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                { 
                    MessageBox.Show("Admin Login Successful!");
                    //Books books = new Books();
                    DashBoard ai = new DashBoard();
                    ai.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Admin Username Or Password Not Valid! Contact Admin");
                }
                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}
