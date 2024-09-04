using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CarHub
{
    public partial class DashboardAdmin : Form
    {

        public DashboardAdmin()
        {
            InitializeComponent();
        }

        public void loadform(object Form)
        {
            if (this.panelDashboardAdmin.Controls.Count > 0)
                this.panelDashboardAdmin.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelDashboardAdmin.Controls.Add(f);
            this.panelDashboardAdmin.Tag = f;
            f.Show();
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
            //btnStaff
            btnStaff.BackColor = Color.RoyalBlue;
            btnStaff.ForeColor = Color.White;
            pictureBoxStaff.Visible = false;
            //btnIncome
            btnIncome.BackColor = Color.RoyalBlue;
            btnIncome.ForeColor = Color.White;
            pictureBoxIncome.Visible = false;

        }

        public void ConnectionBTN()
        {
            string ip = "NAMWOONFEAE";
            string dbName = "DBCARHUB";
            DataConnection.ConnectionDB(ip, dbName);
        }

        private void DashboardAdmin_Load(object sender, EventArgs e)
        {
            labelUsername.Text =FormLogin.username;

            string ip = "NAMWOONFEAE";
            string dbName = "DBCARHUB";
            DataConnection.ConnectionDB(ip, dbName);

            
            ForeAndBGColor();
            btnHome.BackColor = Color.White;
            btnHome.ForeColor = Color.Black;
            pictureBoxHome.Visible = true;
            loadform(new FormHome(this));

            //panelSidebar.BackColor = Color.FromArgb(67, 118, 253);
            //btnHome.BackColor = Color.FromArgb(67, 118, 253);

            //btnHome.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnHome.Width, btnHome.Height, 20, 20));

            //panelDashboardAdmin.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelDashboardAdmin.Width, panelDashboardAdmin.Height, 15, 15));

            //this.FormBorderStyle = FormBorderStyle.None;
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ConnectionBTN();
            ForeAndBGColor();
            btnHome.BackColor = Color.White;
            btnHome.ForeColor = Color.Black;
            pictureBoxHome.Visible = true;
            loadform(new FormHome(this));

        }

        public void btnSale_Click(object sender, EventArgs e)
        {
            ConnectionBTN();
            ForeAndBGColor();
            btnSale.BackColor = Color.White;
            btnSale.ForeColor = Color.Black;
            pictureBoxSale.Visible = true;
            loadform(new FormSale(this));
        }

        public void btnProduct_Click(object sender, EventArgs e)
        {
            ConnectionBTN();
            ForeAndBGColor();
            btnProduct.BackColor = Color.White;
            btnProduct.ForeColor = Color.Black;
            pictureBoxProduct.Visible = true;
            loadform(new FormProduct(this));
        }

        public void btnStaff_Click(object sender, EventArgs e)
        {
            ConnectionBTN();
            ForeAndBGColor();
            btnStaff.BackColor = Color.White;
            btnStaff.ForeColor = Color.Black;
            pictureBoxStaff.Visible = true;
            loadform(new FormStaff(this));
        }

        public void btnIncome_Click(object sender, EventArgs e)
        {
            ConnectionBTN();
            ForeAndBGColor();
            btnIncome.BackColor = Color.White;
            btnIncome.ForeColor = Color.Black;
            pictureBoxIncome.Visible = true;
            loadform(new FormIncome(this));
        }
    }
}
