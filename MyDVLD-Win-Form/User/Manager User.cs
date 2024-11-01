using MyDVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDVLD_Win_Form
{
    public partial class Manager_User : Form
    {
        private DataTable _dtUsers;
        public Manager_User()
        {
            InitializeComponent();
        }
        private void _FillComboBox()
        {
            //Fill ComboBox (cbValueIsActive)
            //cbValueIsActive.DisplayMember = "Text";
            //cbValueIsActive.ValueMember = "Value";
            //cbValueIsActive.Items.Add(new { Text = "All", Value = "" });
            //cbValueIsActive.Items.Add(new { Text = "Yes", Value = "True" });
            //cbValueIsActive.Items.Add(new { Text = "No", Value = "False" });
            //
        }
        private void Manager_User_Load(object sender, EventArgs e)
        {
            
            _dtUsers = clsUser.ListUsers();

            dgvAllUsers.DataSource=_dtUsers;
            dgvAllUsers.Columns[0].HeaderText = "User ID";
            dgvAllUsers.Columns[1].HeaderText = "Person ID";
            dgvAllUsers.Columns[2].HeaderText = "Full Name";
            dgvAllUsers.Columns[3].HeaderText = "User Name";
            dgvAllUsers.Columns[4].HeaderText = "Is Active";

        
            cbFilter.SelectedIndex = 0;
            lblRecord.Text = dgvAllUsers.Rows.Count.ToString();
        }

        private void btnCloseManage_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Add_New_User frm = new Add_New_User();
            frm.ShowDialog();
            if (frm.IsSave)
                dgvAllUsers.DataSource = clsUser.ListUsers();
        }

        private void cahngePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            changePassword.ShowDialog();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_New_User frm = new Add_New_User((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            if (frm.IsSave) //   Refresh data
                dgvAllUsers.DataSource = clsUser.ListUsers();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_New_User frm = new Add_New_User();
            frm.ShowDialog();
            if (frm.IsSave) //   Refresh data
                dgvAllUsers.DataSource = clsUser.ListUsers();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbFilter.SelectedItem.ToString() == "Is Active")
            {
                txtFilter.Visible=false;
                cbValueIsActive.Visible = true;
                cbValueIsActive.SelectedIndex = 0;
                cbValueIsActive.Focus();
            }
            else
            {
                txtFilter.Visible = true;
                cbValueIsActive.Visible = false;
                txtFilter.Visible = (cbFilter.SelectedItem.ToString() != "None");
                txtFilter.Focus();
            }
            

            txtFilter.Clear();

        }
        void _Filter()
        {
            string ColmFilter = null;
            switch (cbFilter.SelectedItem) 
            {
                case "Person ID":
                    ColmFilter = "PersonID";
                    break;
                case "User ID":
                    ColmFilter = "UserID";
                    break;
                case "Full Name":
                    ColmFilter = "FullName";
                    break;
                case "User Name":
                    ColmFilter = "UserName";
                    break;
                case "None":
                    ColmFilter = "None";
                    break;
            }

            if (txtFilter.Text == string.Empty || ColmFilter == "None") 
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecord.Text=dgvAllUsers.Rows.Count.ToString();
                return;
            }
            if (ColmFilter == "PersonID" || ColmFilter == "UserID") 
            {
                _dtUsers.DefaultView.RowFilter = string.Format("{0}={1}", ColmFilter, int.Parse(txtFilter.Text));
            }
            else
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", ColmFilter,txtFilter.Text);
            }
            lblRecord.Text = dgvAllUsers.Rows.Count.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _Filter();
        }



        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtFilter.SelectedText == "PersonID" || txtFilter.SelectedText == "UserID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbValueIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ColmFilter = "IsActive";
            string ValueFilter = null;
            switch (cbValueIsActive.SelectedItem)
            {
                case "All":
                    ValueFilter = "";
                    break;
                case "Yes":
                    ValueFilter = "1";
                    break;
                case "No":
                    ValueFilter = "0";
                    break;
            }

            if (ValueFilter == string.Empty)
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecord.Text = dgvAllUsers.Rows.Count.ToString();
                return;
            }
            else
            {
                _dtUsers.DefaultView.RowFilter = string.Format("{0}={1}", ColmFilter, ValueFilter);
            }
            lblRecord.Text = dgvAllUsers.Rows.Count.ToString();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this User",
                "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.OK)
            {
                if (clsUser.DeleteUser((int)dgvAllUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("اتحذف يا معلم");
                    Manager_User_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Delete was not successfully , Because he is tied to another process", "Not Allow Process", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
