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
    public partial class FormReport : Form
    {
        public FormReport()
        {
            InitializeComponent();
        }
        public void SetSource(List<ReportDetail> arr)
        {
            CrystalReport1.SetDataSource(arr);
        }
        public void SetParamter(int index, object value)
        {
            CrystalReport1.SetParameterValue(index, value);
        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
        public void Print(int n, bool col, int start, int end)
        {
            CrystalReport1.PrintToPrinter(n, col, start, end);
        }
        private void FormReport_Load(object sender, EventArgs e)
        {

        }
    }
}
