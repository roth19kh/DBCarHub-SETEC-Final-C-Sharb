using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarHub
{
    public partial class FormStaff : Form
    {
        public FormStaff(DashboardAdmin dashboardAdmin)
        {
            InitializeComponent();
        }

        private void FormStaff_Load(object sender, System.EventArgs e)
        {
            ShowData();
        }

        private void Clear()
        {
            //Staff
            txtStaffName.Text = "";
            cboSex.SelectedIndex = -1;
            cboPosition.SelectedIndex = -1;
            cboShift.SelectedIndex = -1;
            //User
            cboUserType.SelectedIndex = -1;
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void ShowData()
        {
            try
            {
                dataGridViewStaff.Rows.Clear();

                string sql = "SELECT * FROM tblStaff";
                using (SqlCommand s = new SqlCommand(sql, DataConnection.DataCon))
                {
                    using (SqlDataReader r = s.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            dataGridViewStaff.Rows.Add(r["SID"].ToString(), r["SName"].ToString(), r["Sex"].ToString(), r["Position"].ToString(), r["Shift"].ToString(), r["UserName"].ToString(), r["Password"].ToString(), r["UserType"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewStaff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int row = e.RowIndex;
                int pid = int.Parse(dataGridViewStaff.Rows[row].Cells[0].Value.ToString());
                txtStaffName.Text = dataGridViewStaff.Rows[row].Cells[1].Value.ToString();
                cboSex.Text = dataGridViewStaff.Rows[row].Cells[2].Value.ToString();
                cboPosition.Text = dataGridViewStaff.Rows[row].Cells[3].Value.ToString();
                cboShift.Text = dataGridViewStaff.Rows[row].Cells[4].Value.ToString();
                txtUserName.Text = dataGridViewStaff.Rows[row].Cells[5].Value.ToString();
                txtPassword.Text = dataGridViewStaff.Rows[row].Cells[6].Value.ToString();
                cboUserType.Text = dataGridViewStaff.Rows[row].Cells[7].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewStaff.SelectedRows.Count > 0)
                {
                    int row = dataGridViewStaff.SelectedRows[0].Index;
                    int id = int.Parse(dataGridViewStaff.Rows[row].Cells[0].Value.ToString());

                    string sql = "DELETE FROM tblStaff WHERE SID = @SID";
                    using (SqlCommand s = new SqlCommand(sql, DataConnection.DataCon))
                    {
                        s.Parameters.AddWithValue("@SID", id);
                        s.ExecuteNonQuery();
                    }

                    ShowData();
                    MessageBox.Show("Data Deleted!");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtStaffName.Text == "" || cboPosition.Text == "" || cboSex.Text == "" || cboShift.Text == "")
            {
                MessageBox.Show("Please fill!!");
            }
            else
            {
                try
                {
                    //Staff
                    string sname = txtStaffName.Text.Trim();
                    string sex = cboSex.Text.Trim();
                    string position = cboPosition.Text.Trim();
                    string shift = cboShift.Text.Trim();
                    //User
                    string username = txtUserName.Text.Trim();
                    string password = txtPassword.Text.Trim();
                    string usertype = cboUserType.Text.Trim();

                    // Check if the username already exists
                    string checkSql = "SELECT COUNT(*) FROM tblStaff WHERE UserName = @UserName";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, DataConnection.DataCon))
                    {
                        checkCmd.Parameters.AddWithValue("@UserName", username);
                        int userCount = (int)checkCmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.");
                            return;
                        }
                    }
                    // Insert the new staff record
                    string sql = "INSERT INTO tblStaff (SName, Sex, Position, Shift, UserName, Password, UserType) VALUES (@SName, @Sex, @Position, @Shift, @UserName, @Password, @UserType)";
                    using (SqlCommand s = new SqlCommand(sql, DataConnection.DataCon))
                    {
                        s.Parameters.AddWithValue("@SName", sname);
                        s.Parameters.AddWithValue("@Sex", sex);
                        s.Parameters.AddWithValue("@Position", position);
                        s.Parameters.AddWithValue("@Shift", shift);
                        s.Parameters.AddWithValue("@UserName", username);
                        s.Parameters.AddWithValue("@Password", password);
                        s.Parameters.AddWithValue("@UserType", usertype);

                        s.ExecuteNonQuery();
                    }
                    ShowData();
                    MessageBox.Show("Data Added!");
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewStaff.SelectedRows.Count > 0)
                {
                    string oldSid = dataGridViewStaff.SelectedRows[0].Cells[0].Value.ToString().Trim();
                    string sname = txtStaffName.Text.Trim();
                    string sex = cboSex.Text.Trim();
                    string position = cboPosition.Text.Trim();
                    string shift = cboShift.Text.Trim();
                    string username = txtUserName.Text.Trim();
                    string usertype = cboUserType.Text.Trim();
                    string password = txtPassword.Text.Trim();

                    if (string.IsNullOrEmpty(sname) || string.IsNullOrEmpty(sex) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(shift))
                    {
                        MessageBox.Show("Please fill in all fields.");
                        return;
                    }

                    // Check if the username already exists for a different record
                    string checkSql = "SELECT COUNT(*) FROM tblStaff WHERE UserName = @UserName AND SID != @OldSID";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, DataConnection.DataCon))
                    {
                        checkCmd.Parameters.AddWithValue("@UserName", username);
                        checkCmd.Parameters.AddWithValue("@OldSID", oldSid);
                        int userCount = (int)checkCmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.");
                            return;
                        }
                    }

                    // Update the staff record
                    string sql = "UPDATE tblStaff SET SName = @SName, Sex = @Sex, Position = @Position, Shift = @Shift, UserName = @UserName, Password = @Password, UserType = @UserType WHERE SID = @OldSID";
                    using (SqlCommand s = new SqlCommand(sql, DataConnection.DataCon))
                    {
                        s.Parameters.AddWithValue("@OldSID", oldSid);
                        s.Parameters.AddWithValue("@SName", sname);
                        s.Parameters.AddWithValue("@Sex", sex);
                        s.Parameters.AddWithValue("@Position", position);
                        s.Parameters.AddWithValue("@Shift", shift);
                        s.Parameters.AddWithValue("@UserName", username);
                        s.Parameters.AddWithValue("@Password", password);
                        s.Parameters.AddWithValue("@UserType", usertype);

                        int rowsAffected = s.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ShowData();
                            MessageBox.Show("Data Updated!");
                            Clear();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Please ensure the Staff ID exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
