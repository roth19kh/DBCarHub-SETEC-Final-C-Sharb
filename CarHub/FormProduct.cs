using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

namespace CarHub
{
    public partial class FormProduct : Form
    {
        private Image defaultImage;
        public FormProduct(DashboardAdmin dashboardAdmin)
        {
            InitializeComponent();
            defaultImage = pictureBoxProductImage.Image;
        }

        private List<string> cid = new List<string>();
        private List<string> cname = new List<string>();

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
                    dataGridViewProduct.Rows.Add(pID, pName, qty, price, catID,img);
                }
                r1.Close();
                s1.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            try
            {
                cboCategoryName.Items.Clear();
                string sql = "SELECT * FROM tblCategory";
                SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
                SqlDataReader r = s.ExecuteReader();
                while (r.Read())
                {
                    string catID = r[0].ToString();
                    string catName = r[1].ToString();
                    cboCategoryName.Items.Add(catName);
                    cid.Add(catID);
                    cname.Add(catName);
                }
                r.Close();
                s.Dispose();
                cboCategoryName.SelectedIndex = -1;

                ShowData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        

        private void Clear()
        {
            txtPName.Clear();
            txtQty.Clear();
            txtPrice.Clear();
            cboCategoryName.SelectedIndex = 0;
            //txtCategoryID.Clear();
            pictureBoxProductImage.Image = defaultImage;
            //txtCategoryID.Clear();
            pictureBoxProductImage.Image = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewProduct.SelectedRows.Count > 0)
                {
                    int row = dataGridViewProduct.SelectedRows[0].Index;
                    int id = int.Parse(dataGridViewProduct.Rows[row].Cells[0].Value.ToString());
                    string sql = "DELETE FROM tblProduct WHERE PID = @PID";
                    SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
                    s.Parameters.AddWithValue("@PID", id);
                    s.ExecuteNonQuery();
                    s.Dispose();

                    ShowData();
                    MessageBox.Show("Data Deleted!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string imgLocation;
        private void btnInsert_Click(object sender, EventArgs e)
        {
            //try
            //{
                //byte[] images = null;
                //FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                //BinaryReader brs = new BinaryReader(stream);
                //images = brs.ReadBytes((int)stream.Length);



                //string pname = txtPName.Text.Trim();
                //string qty = txtQty.Text.Trim();
                //string price = txtPrice.Text.Trim();
                //string catid = txtCategoryID.Text.Trim();

                ////if (!decimal.TryParse(price, out decimal parsedPrice) || !int.TryParse(qty, out int parsedQty))
                ////{
                ////    MessageBox.Show("Please enter valid numeric values for Price and Qty.");
                ////    return;
                ////}

                ////if (!string.IsNullOrEmpty(imgLocation))
                ////{
                ////    using (FileStream streams = new FileStream(imgLocation, FileMode.Open, FileAccess.Read))
                ////    {
                ////        using (BinaryReader br = new BinaryReader(streams))
                ////        {
                ////            images = br.ReadBytes((int)streams.Length);
                ////        }
                ////    }
                ////}


                //string sql = "INSERT INTO tblProduct (PName, Qty, Price, CatID,Image) VALUES (@PName, @Qty, @Price,  @CatID,@Image)";
                //SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
                //s.Parameters.AddWithValue("@PName", pname);
                //s.Parameters.AddWithValue("@Qty", qty);
                //s.Parameters.AddWithValue("@Price", price);
                //s.Parameters.AddWithValue("@CatID", catid);
                //s.Parameters.AddWithValue("@Image", images);
                //s.ExecuteNonQuery();
                //s.Dispose();
                //FormProduct_Load(sender, e);
                //ShowData();
                //btnClear_Click(sender, e);

            try
            {
                    if (string.IsNullOrEmpty(imgLocation))
                {
                    MessageBox.Show("Image location cannot be null or empty.");
                    return;
                }

                byte[] images = null;
                using (FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader brs = new BinaryReader(stream))
                    {
                        images = brs.ReadBytes((int)stream.Length);
                    }
                }

                string pname = txtPName.Text.Trim();
                string qty = txtQty.Text.Trim();
                string price = txtPrice.Text.Trim();
                string catid = txtCategoryID.Text.Trim();

                string sql = "INSERT INTO tblProduct (PName, Qty, Price, CatID, Image) VALUES (@PName, @Qty, @Price, @CatID, @Image)";
                using (SqlCommand s = new SqlCommand(sql, DataConnection.DataCon))
                {
                    s.Parameters.AddWithValue("@PName", pname);
                    s.Parameters.AddWithValue("@Qty", qty);
                    s.Parameters.AddWithValue("@Price", price);
                    s.Parameters.AddWithValue("@CatID", catid);
                    s.Parameters.AddWithValue("@Image", images);
                    s.ExecuteNonQuery();
                }

                FormProduct_Load(sender, e);
                ShowData();
                btnClear_Click(sender, e);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string pname = txtPName.Text.Trim();
                string qty = txtQty.Text.Trim();
                string price = txtPrice.Text.Trim();
                string catid = txtCategoryID.Text.Trim();

                byte[] images = null;
                if (!string.IsNullOrEmpty(imgLocation))
                {
                    using (FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader brs = new BinaryReader(stream))
                        {
                            images = brs.ReadBytes((int)stream.Length);
                        }
                    }
                }

                string sql = "UPDATE tblProduct SET PName = @PName, Qty = @Qty, Price = @Price, CatID = @CatID";
                if (images != null)
                {
                    sql += ", Image = @Image";
                }
                sql += " WHERE PID = @PID;";

                using (SqlCommand s = new SqlCommand(sql, DataConnection.DataCon))
                {
                    s.Parameters.AddWithValue("@PName", pname);
                    s.Parameters.AddWithValue("@Qty", qty);
                    s.Parameters.AddWithValue("@Price", price);
                    s.Parameters.AddWithValue("@CatID", catid);
                    if (images != null)
                    {
                        s.Parameters.AddWithValue("@Image", images);
                    }
                    s.Parameters.AddWithValue("@PID", idforupdate);
                    s.ExecuteNonQuery();
                    s.Dispose();
                }
                ShowData();
                Clear();
                
                MessageBox.Show("Data Updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = cboCategoryName.SelectedIndex;
                txtCategoryID.Text = cid[index];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int idforupdate;
        private void dataGridViewProduct_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewProduct.SelectedRows.Count > 0)
            {
                int row = dataGridViewProduct.SelectedRows[0].Index;
                int pid = int.Parse(dataGridViewProduct.Rows[row].Cells[0].Value.ToString());
                idforupdate = pid;
                string pname = dataGridViewProduct.Rows[row].Cells[1].Value.ToString();
                int qty = int.Parse(dataGridViewProduct.Rows[row].Cells[2].Value.ToString());
                double price = double.Parse(dataGridViewProduct.Rows[row].Cells[3].Value.ToString());
                int catID = int.Parse(dataGridViewProduct.Rows[row].Cells[4].Value.ToString());

                txtPName.Text = pname;
                txtQty.Text = qty.ToString();
                txtPrice.Text = price.ToString();
                txtCategoryID.Text = catID.ToString();

                string testcatID = catID.ToString();
                int ind = 0;
                for (int i = 0; i < cid.Count; i++)
                {
                    if (cid[i] == testcatID)
                    {
                        ind = i;
                        break;
                    }
                }
                cboCategoryName.SelectedItem = cname[ind];

                // Retrieve the image from the database
                byte[] imageData = null;
                using (SqlCommand cmd = new SqlCommand("SELECT Image FROM tblProduct WHERE PID = @PID", DataConnection.DataCon))
                {
                    cmd.Parameters.AddWithValue("@PID", pid);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        imageData = reader["Image"] as byte[];
                    }
                    reader.Close();
                }

                // Convert byte array to image and display it
                if (imageData != null)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        pictureBoxProductImage.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBoxProductImage.Image = null; // Clear the picture box if no image is found
                }
            }
        }

        private void dataGridViewProduct_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridViewProduct.SelectedRows.Count > 0)
            //{
            //    int row = dataGridViewProduct.SelectedRows[0].Index;
            //    //int pid = int.Parse(dataGridViewProduct.Rows[row].Cells[0].Value.ToString());
            //    //string barcode = dataGridViewProduct.Rows[row].Cells[1].Value.ToString();
            //    string pname = dataGridViewProduct.Rows[row].Cells[0].Value.ToString();
            //    int price = int.Parse(dataGridViewProduct.Rows[row].Cells[1].Value.ToString());
            //    int qty = int.Parse(dataGridViewProduct.Rows[row].Cells[2].Value.ToString());
            //    int catID = int.Parse(dataGridViewProduct.Rows[row].Cells[3].Value.ToString());

            //    //txtBarcode.Text = barcode;
            //    txtPName.Text = pname + "";
            //    txtPrice.Text = price + "";
            //    txtQty.Text = qty + "";
            //    txtCategoryID.Text = catID + "";
            //}
        }

        private void pictureBoxProductImage_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";

            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    string imgLocation = dialog.FileName.ToString();
            //    pictureBoxProductImage.ImageLocation = imgLocation;
            //}


            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgLocation = ofd.FileName;
                pictureBoxProductImage.ImageLocation = imgLocation; // Optional: to display the selected image in a PictureBox
            }
        }

        private void dataGridViewProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
