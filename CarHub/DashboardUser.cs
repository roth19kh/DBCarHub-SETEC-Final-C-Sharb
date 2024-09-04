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
    public partial class DashboardUser : Form
    {
        public DashboardUser()
        {
            InitializeComponent();
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new FormLogin().Show();
        }

        public void ForeAndBGColor()
        {
            //btnHome
            btnHome.BackColor = Color.RoyalBlue;
            btnHome.ForeColor = Color.White;
            pictureBoxHome.Visible = false;
            //btnSale
            btnSale.BackColor = Color.RoyalBlue;
            btnSale.ForeColor = Color.White;
            pictureBoxSale.Visible = false;
            //btnProduct
            btnProduct.BackColor = Color.RoyalBlue;
            btnProduct.ForeColor = Color.White;
            pictureBoxProduct.Visible = false;

        }

        public void ConnectionBTN()
        {
            string ip = "NAMWOONFEAE";
            string dbName = "DBCARHUB";
            DataConnection.ConnectionDB(ip, dbName);
        }

        public void loadform(object Form)
        {
            if (this.panelDashboardUser.Controls.Count > 0)
                this.panelDashboardUser.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelDashboardUser.Controls.Add(f);
            this.panelDashboardUser.Tag = f;
            f.Show();
        }

        private void DashboardUser_Load(object sender, EventArgs e)
        {
            labelUsername.Text = FormLogin.username;
            string ip = "NAMWOONFEAE";
            string dbName = "DBCARHUB";
            DataConnection.ConnectionDB(ip, dbName);

            ForeAndBGColor();
            btnHome.BackColor = Color.White;
            btnHome.ForeColor = Color.Black;
            pictureBoxHome.Visible = true;
            loadform(new UserHome(this));
        }

        public void btnHome_Click(object sender, EventArgs e)
        {
            ConnectionBTN();
            ForeAndBGColor();
            btnHome.BackColor = Color.White;
            btnHome.ForeColor = Color.Black;
            pictureBoxHome.Visible = true;
            loadform(new UserHome(this));
        }

        public void btnSale_Click(object sender, EventArgs e)
        {
            ConnectionBTN();
            ForeAndBGColor();
            btnSale.BackColor = Color.White;
            btnSale.ForeColor = Color.Black;
            pictureBoxSale.Visible = true;
            loadform(new UserSale(this));
        }

        public void btnProduct_Click(object sender, EventArgs e)
        {
            ConnectionBTN();
            ForeAndBGColor();
            btnProduct.BackColor = Color.White;
            btnProduct.ForeColor = Color.Black;
            pictureBoxProduct.Visible = true;
            loadform(new UserProduct(this));
        }
    }
}
