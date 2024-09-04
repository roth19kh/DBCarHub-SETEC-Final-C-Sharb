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
    public partial class FormPayment : Form
    {
        public FormPayment()
        {
            InitializeComponent();
        }

        public FormPayment(double totalamount)
        {
            InitializeComponent();
            TotalAmount = totalamount;
        }

        private void FormPayment_Load(object sender, EventArgs e)
        {
            cboDiscount.SelectedIndex = 0;
            txtTotalAmount.Text = TotalAmount.ToString("$#,##0.00");
        }

        public double TotalAmount { get; set; }
        public double Discount { get; set; }
        public double DisPrice { get; set; }
        public double Pay { get; set; }
        public double CashReceived { get; set; }
        public double CashReturned { get; set; }

        public Boolean Preview { get; set; }

        private void cboDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dis = cboDiscount.SelectedItem + "";
            dis = dis.Substring(0, dis.Length - 1);
            Discount = double.Parse(dis);
            DisPrice = TotalAmount * Discount / 100;
            Pay = TotalAmount - DisPrice;

            txtDiscountPrice.Text = DisPrice.ToString("$#,##0.00");
            txtPayment.Text = Pay.ToString("$#,##0.00");
        }

        private void txtCashReceived_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                try
                {
                    string received = txtCashReceived.Text.Trim();
                    CashReceived = double.Parse(received);
                    if (CashReceived >= Pay)
                    {
                        CashReturned = CashReceived - Pay;
                        txtCashReturn.Text = CashReturned.ToString("$#,##0.00");
                        btnPay.Enabled = true;
                    }
                    else
                    {
                        txtCashReturn.Text = "$0.00";
                        btnPay.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {

            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            Preview = check.Checked;
            DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void check_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
