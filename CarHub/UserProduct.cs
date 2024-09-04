using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarHub
{
    public partial class UserProduct : Form
    {
        public UserProduct(DashboardUser dashboardUser)
        {
            InitializeComponent();
        }

        private void ShowData()
        {
            try
            {
                dataGridViewProduct.Rows.Clear();
                string sql1 = "SELECT * FROM tblProduct";
                SqlCommand s1 = new SqlCommand(sql1, DataConnection.DataCon);
                SqlDataReader r1 = s1.ExecuteReader();
                while (r1.Read())
                {
                    string pID = r1[0].ToString();
                    string pName = r1[1].ToString();
                    string qty = r1[2].ToString();
                    string price = r1[3].ToString();
                    string catID = r1[4].ToString();
                    byte[] image = r1["image"] as byte[];
                    Image img = null;
                    if (image != null)
                    {
                        using (MemoryStream ms = new MemoryStream(image))
                        {
                            img = Image.FromStream(ms);
                        }
                    }
                    dataGridViewProduct.Rows.Add(pID, pName, qty, price, catID, img);
                }
                r1.Close();
                s1.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserProduct_Load(object sender, EventArgs e)
        {
            try
            {
                //cboCategoryName.Items.Clear();
                string sql = "SELECT * FROM tblCategory";
                SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
                SqlDataReader r = s.ExecuteReader();
                while (r.Read())
                {
                    //string catID = r[0].ToString();
                    //string catName = r[1].ToString();
                    //cboCategoryName.Items.Add(catName);
                    //cid.Add(catID);
                    //cname.Add(catName);
                }
                r.Close();
                s.Dispose();
                //cboCategoryName.SelectedIndex = -1;

                ShowData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
