using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarHub
{
    public partial class FormHome : Form
    {
        private DashboardAdmin _dashboardAdmin;
        public FormHome(DashboardAdmin dashboardAdmin)
        {
            InitializeComponent();
            _dashboardAdmin = dashboardAdmin;
        }

        private void FormHome_Load(object sender, EventArgs e)
        {

        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            this.Dispose();
            _dashboardAdmin.btnSale_Click(sender, e);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            this.Dispose();
            _dashboardAdmin.btnProduct_Click(sender, e);
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            this.Dispose();
            _dashboardAdmin.btnIncome_Click(sender, e);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            this.Dispose();
            _dashboardAdmin.btnStaff_Click(sender, e);
        }
    }
}
