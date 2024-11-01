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
    public partial class frmListDriver : Form
    {
        public frmListDriver()
        {
            InitializeComponent();
        }
        private DataTable _dtDrivers;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _RefreshData()
        {
            _dtDrivers = clsDriver.GetAllDriver();
            dgvDrivers.DataSource =_dtDrivers;
            if (dgvDrivers.RowCount < 1)
            {
                MessageBox.Show("Error in Load Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            dgvDrivers.Columns[0].HeaderText = "Driver ID";
            dgvDrivers.Columns[0].Width = 90;

            dgvDrivers.Columns[1].HeaderText = "Person ID";
            dgvDrivers.Columns[1].Width = 90;

            dgvDrivers.Columns[2].HeaderText = "National No.";
            dgvDrivers.Columns[2].Width = 110;

            dgvDrivers.Columns[3].HeaderText = "Full Name";
            dgvDrivers.Columns[3].Width = 290;

            dgvDrivers.Columns[4].HeaderText = "Date";
            dgvDrivers.Columns[4].Width = 140;

            dgvDrivers.Columns[5].HeaderText = "Active Licenses";
            dgvDrivers.Columns[5].Width = 120;

            lblRecordsCount.Text = dgvDrivers.RowCount.ToString();
            cbFilterBy.SelectedIndex = 0;
        }
        private void frmListDriver_Load(object sender, EventArgs e)
        {
            _RefreshData();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory LicenseHistory = new frmLicenseHistory((int)dgvDrivers.CurrentRow.Cells[1].Value);
            this.Hide();
            LicenseHistory.ShowDialog();
            this.Show();
        }

        private void issueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
            
            {
                _RefreshData();
            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = txtFilterValue.Enabled = (cbFilterBy.SelectedIndex != 0);

            txtFilterValue.Text = string.Empty;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string ColumnText = string.Empty;
            string FilterValue = txtFilterValue.Text.Trim();
            switch (cbFilterBy.SelectedItem)
            {
                case "Driver ID":
                    ColumnText = "DriverID";
                    break;
                case "Person ID":
                    ColumnText = "PersonID";
                    break;
                case "National No.":
                    ColumnText = "NationalNo";
                    break;
                case "Full Name":
                    ColumnText = "FullName";
                    break;
                default:
                    ColumnText = "None";
                    break;
            }
            if (ColumnText == "None" || FilterValue == string.Empty)
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.RowCount.ToString();
                return;
            }

            if (ColumnText == "DriverID" || ColumnText == "PersonID")
            {
                _dtDrivers.DefaultView.RowFilter = $"{ColumnText} = {FilterValue}";
            }
            else
            {
                _dtDrivers.DefaultView.RowFilter = $"{ColumnText} LIKE '{FilterValue}%'";
            }
            lblRecordsCount.Text = dgvDrivers.RowCount.ToString();
        }
    }
}
