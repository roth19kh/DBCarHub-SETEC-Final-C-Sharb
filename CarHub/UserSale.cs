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
    public partial class UserSale : Form
    {
        private DashboardUser _dashboardUser;
        public UserSale(DashboardUser dashboardUser)
        {
            InitializeComponent();
            _dashboardUser = dashboardUser;
        }

        List<InvoiceDetail> invoice = new List<InvoiceDetail>();
        private bool invoiceProduct(int pid)
        {
            string sql = "select * from tblProduct where PID=" + pid + ";";
            SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
            SqlDataReader r = s.ExecuteReader();
            if (r.Read())
            {
                string pname = r[1] + "";
                int qty;
                if (txtQty.Text == "")
                    qty = 1;
                else
                    qty = int.Parse(txtQty.Text.Trim());
                double price = double.Parse(r[3] + "");
                double amount = qty * price;
                int no = dataGridViewCart.Rows.Count + 1;

                dataGridViewCart.Rows.Add(no, pname, price.ToString("$0.00"), qty, amount.ToString("$#,##0.00"));
                InvoiceDetail obj = new InvoiceDetail(qty, pid);
                invoice.Add(obj);

                r.Close();
                s.Dispose();
                return true;
            }
            else
            {
                r.Close();
                s.Dispose();
                return false;
            }
        }
        private double TotalAmount()
        {
            double total = 0;
            for (int i = 0; i < dataGridViewCart.RowCount; i++)
            {
                total += double.Parse(dataGridViewCart.Rows[i].Cells[4].Value.ToString().Substring(1));
            }
            return total;
        }

        private void InsertInvoiceDetail(int inID)
        {
            //foreach (OrderDetails temp in order)
            //{
            //    int pid = temp.PID;
            //    int qty = temp.Qty;
            //    string sql = "insert INTO tblOrderDetails VALUES("+orID+","+pid+","+qty+");";
            //    SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
            //    s.ExecuteNonQuery();
            //    s.Dispose();
            //}

            string sql = "INSERT INTO tblInvoiceDetail (InID, PID, Qty) VALUES (@InvoiceID, @ProductID, @Quantity)";

            foreach (InvoiceDetail temp in invoice)
            {
                int pid = temp.PID;
                int qty = temp.Qty;

                using (SqlCommand s = new SqlCommand(sql, DataConnection.DataCon))
                {
                    s.Parameters.AddWithValue("@InvoiceID", inID);
                    s.Parameters.AddWithValue("@ProductID", pid);
                    s.Parameters.AddWithValue("@Quantity", qty);

                    s.ExecuteNonQuery();
                }
            }
        }

        private void UpdateStock()
        {
            foreach (InvoiceDetail tepm in invoice)
            {
                int qty = tepm.Qty;
                int pid = tepm.PID;
                string sql = "UPDATE tblProduct SET Qty = Qty - " + qty + " WHERE PiD = " + pid;
                SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
                s.ExecuteNonQuery();
                s.Dispose();
            }
        }

        public int InsertInvoice(double discount, double pay)
        {
            string invoicedate = DateTime.Now.ToString("yyyy-MM-dd");
            string invoicetime = DateTime.Now.ToString("HH:mm:ss");
            //string sellername = DataConnection.Seller;
            string sellername = FormLogin.username;

            double total = pay;
            double dis = discount;

            string sql = "insert into tblInvoice (Total,InDate,InTime,Discount,SName) Values ('" + total + "','" + invoicedate + "','" + invoicetime + "','" + dis + "','" + sellername + "');";
            SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
            s.ExecuteNonQuery();
            int iId = 0;
            sql = "Select SCOPE_IDENTITY();";
            s = new SqlCommand(sql, DataConnection.DataCon);
            SqlDataReader r = s.ExecuteReader();
            if (r.Read())
            {
                iId = int.Parse(r[0] + "");
            }
            r.Close();
            s.Dispose();
            return iId;
        }

        private void PrintReport(int inID, double discount, double cashReceived, bool preview)
        {
            FormReport reports = new FormReport();
            List<ReportDetail> arr = new List<ReportDetail>();
            foreach (DataGridViewRow temp in dataGridViewCart.Rows)
            {
                int no = int.Parse(temp.Cells[0].Value + "");
                string sName = temp.Cells[1].Value + "";
                double price = double.Parse(temp.Cells[2].Value + "", System.Globalization.NumberStyles.AllowCurrencySymbol | System.Globalization.NumberStyles.AllowDecimalPoint);
                int qty = int.Parse(temp.Cells[3].Value + "");
                //double amount = double.Parse(temp.Cells[4].Value + "", System.Globalization.NumberStyles.AllowCurrencySymbol | System.Globalization.NumberStyles.AllowDecimalPoint);
                ReportDetail obj = new ReportDetail(no, sName, price, qty);
                arr.Add(obj);
            }
            reports.SetSource(arr);
            reports.SetParamter(0, inID);
            reports.SetParamter(1, DataConnection.Seller);
            reports.SetParamter(2, discount);
            reports.SetParamter(3, cashReceived);
            if (preview)
                reports.ShowDialog();
            else
                reports.Print(1, true, 1, -1);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProductID.Clear();
            txtQty.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            _dashboardUser.btnSale_Click(sender, e);
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            int pid = int.Parse(txtProductID.Text.Trim());
            if (invoiceProduct(pid))
            {
                txtProductID.Clear();
                txtQty.Clear();
                txtTotalAmount.Text = TotalAmount().ToString("$#,##0.00");
            }
            else
            {
                MessageBox.Show("Product is not instock");
            }
        }

        private void btnToPay_Click(object sender, EventArgs e)
        {
            UserPayment p = new UserPayment(TotalAmount());

            if (p.ShowDialog() == DialogResult.OK)
            {
                int InID = InsertInvoice(p.Discount, p.Pay);

                InsertInvoiceDetail(InID);

                UpdateStock();

                invoice = new List<InvoiceDetail>();

                PrintReport(InID, p.Discount, p.CashReceived, p.Preview);
                //PrintReport(InID, p.Discount, p.CashReceived, p.Preview);

                dataGridViewCart.Rows.Clear();
                txtTotalAmount.Text = "$0.00";
            }
        }
    }
}
