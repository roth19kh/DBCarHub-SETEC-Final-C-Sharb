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

namespace CarHub
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        //SqlCommand cmd;

        SqlConnection cn = new SqlConnection("Data Source=NAMWOONFEAE;Initial Catalog=DBCARHUB;User ID=sa;Password=koko");
        //string str;
        //string message = "Access Deny, Please Contact to administrator!";

        

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static string username;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                username = txtUserName.Text;
                string str = "SELECT * FROM tblStaff WHERE UserName = @UserName AND Password = @Password";
                SqlCommand cmd = new SqlCommand(str, cn);
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["UserType"].ToString() == "Admin")
                    {
                        dr.Close();
                        DashboardAdmin m = new DashboardAdmin();
                        this.Dispose(false);
                        m.Show();
                    }
                    else if (dr["UserType"].ToString() == "User")
                    {
                        dr.Close();
                        DashboardUser m = new DashboardUser();
                        this.Dispose(false);
                        m.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid user type.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dr.Close();
                        cn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect username or password.");
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUserName.SelectionStart = txtUserName.Text.Length;
            txtUserName.SelectionLength = 0;
            txtUserName.Focus();
        }


        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (login_showPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
    }
}
