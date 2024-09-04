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
    public partial class UserHome : Form
    {
        private DashboardUser _dashboardUser;
        public UserHome(DashboardUser dashboardUser)
        {
            InitializeComponent();
            _dashboardUser = dashboardUser;
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            this.Dispose();
            _dashboardUser.btnProduct_Click(sender, e);
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            this.Dispose();
            _dashboardUser.btnSale_Click(sender, e);
        }
    }
}
