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
    public partial class FormIncome : Form
    {
        public FormIncome(DashboardAdmin dashboardAdmin)
        {
            InitializeComponent();
        }

        private void ShowData(DateTime date)
        {
            try
            {
                dataGridViewSellRecord.Rows.Clear();
                decimal dailyIncome = 0;
                decimal weeklyIncome = 0;
                decimal monthlyIncome = 0;

                // Query for daily income
                string sqlDaily = "SELECT * FROM tblInvoice WHERE CAST(InDate AS DATE) = @Date";
                // Query for weekly income
                string sqlWeekly = "SELECT * FROM tblInvoice WHERE DATEPART(week, InDate) = DATEPART(week, @Date) AND DATEPART(year, InDate) = DATEPART(year, @Date)";
                // Query for monthly income
                string sqlMonthly = "SELECT * FROM tblInvoice WHERE DATEPART(month, InDate) = DATEPART(month, @Date) AND DATEPART(year, InDate) = DATEPART(year, @Date)";

                // Calculate daily income
                using (SqlCommand cmdDaily = new SqlCommand(sqlDaily, DataConnection.DataCon))
                {
                    cmdDaily.Parameters.AddWithValue("@Date", date);
                    using (SqlDataReader readerDaily = cmdDaily.ExecuteReader())
                    {
                        while (readerDaily.Read())
                        {
                            string inID = readerDaily["InID"].ToString();
                            string inDate = readerDaily["InDate"].ToString();
                            string inTime = readerDaily["InTime"].ToString();
                            string total = readerDaily["Total"].ToString();
                            string discount = readerDaily["Discount"].ToString();
                            string sName = readerDaily["SName"].ToString();

                            decimal totalAmount = decimal.Parse(total);
                            dailyIncome += totalAmount;

                            dataGridViewSellRecord.Rows.Add(inID, inDate, inTime, total, discount, sName);
                        }
                    }
                }

                // Calculate weekly income
                using (SqlCommand cmdWeekly = new SqlCommand(sqlWeekly, DataConnection.DataCon))
                {
                    cmdWeekly.Parameters.AddWithValue("@Date", date);
                    using (SqlDataReader readerWeekly = cmdWeekly.ExecuteReader())
                    {
                        while (readerWeekly.Read())
                        {
                            string total = readerWeekly["Total"].ToString();
                            decimal totalAmount = decimal.Parse(total);
                            weeklyIncome += totalAmount;
                        }
                    }
                }
                // Calculate monthly income
                using (SqlCommand cmdMonthly = new SqlCommand(sqlMonthly, DataConnection.DataCon))
                {
                    cmdMonthly.Parameters.AddWithValue("@Date", date);
                    using (SqlDataReader readerMonthly = cmdMonthly.ExecuteReader())
                    {
                        while (readerMonthly.Read())
                        {
                            string total = readerMonthly["Total"].ToString();
                            decimal totalAmount = decimal.Parse(total);
                            monthlyIncome += totalAmount;
                        }
                    }
                }

                // Update TextBoxDaily, TextBoxWeekly, and TextBoxMonthly with the calculated incomes
                txtDaily.Text = dailyIncome.ToString("C"); // Format as currency
                txtWeekly.Text = weeklyIncome.ToString("C"); // Format as currency
                txtMonthly.Text = monthlyIncome.ToString("C"); // Format as currency
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // Optionally log the exception for further analysis
            }
        }

        private void dateTimePickerIncome_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePickerIncome.Value;
            txtDay.Text = selectedDate.Day.ToString();
            txtMonth.Text = selectedDate.Month.ToString();
            txtYear.Text = selectedDate.Year.ToString();
        }


        private void FormIncome_Load(object sender, EventArgs e)
        {
            lbday.Hide();
            txtDay.Hide();
            lbMonth.Hide();
            txtMonth.Hide();
            lbYear.Hide();
            txtYear.Hide();
            // Set the dateTimePicker to the current date
            dateTimePickerIncome.Value = DateTime.Now;

            // Attach the ValueChanged event handler
            dateTimePickerIncome.ValueChanged += new EventHandler(dateTimePickerIncome_ValueChanged);


            // Set the text boxes to the current date
            DateTime currentDate = DateTime.Now;
            txtDay.Text = currentDate.Day.ToString();
            txtMonth.Text = currentDate.Month.ToString();
            txtYear.Text = currentDate.Year.ToString();

            // Optionally, show data for the current date on form load
            ShowData(currentDate);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int day = int.Parse(txtDay.Text);
                int month = int.Parse(txtMonth.Text);
                int year = int.Parse(txtYear.Text);
                DateTime selectedDate = new DateTime(year, month, day);

                ShowData(selectedDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid date input. Please enter a valid date.");
            }
        }
    }
}