using MyDVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace MyDVLD_Win_Form
{
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _dtDetainedLicenses = new DataTable();
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }
        private void _RefreshData()
        {
            cbFilterBy.SelectedIndex = 0;
            cbIsReleased.SelectedIndex = 0;
            _dtDetainedLicenses = clsDetian.GetAllDetainedLicense();
            if (_dtDetainedLicenses.Rows.Count < 1) 
            {
                MessageBox.Show("there aren't Detained Licenses");
                return;
            }
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;

            dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
            dgvDetainedLicenses.Columns[0].Width = 90;

            dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
            dgvDetainedLicenses.Columns[1].Width = 90;

            dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
            dgvDetainedLicenses.Columns[2].Width = 160;

            dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
            dgvDetainedLicenses.Columns[3].Width = 110;

            dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
            dgvDetainedLicenses.Columns[4].Width = 110;

            dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
            dgvDetainedLicenses.Columns[5].Width = 160;

            dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
            dgvDetainedLicenses.Columns[6].Width = 90;

            dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
            dgvDetainedLicenses.Columns[7].Width = 330;

            dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
            dgvDetainedLicenses.Columns[8].Width = 150;

            lblTotalRecords.Text = dgvDetainedLicenses.RowCount.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void frmListDetainedLicenses_Load_1(object sender, EventArgs e)
        {
            _RefreshData();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm= new frmDetainLicenseApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
            
            {
                _RefreshData();
            }
           
        }

        private void btnReleaseDetainedLicensebtnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
            {
                _RefreshData();
            }
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;

            frmLicenseInfo frm = new frmLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;

            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.ShowDialog();
            //refresh
            _RefreshData();

        }

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(Boolean)(dgvDetainedLicenses.CurrentRow.Cells[3].Value);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIsReleased.Visible = ( cbFilterBy.SelectedIndex == 2);
            txtFilterValue.Visible = (cbFilterBy.SelectedIndex != 0 && !cbIsReleased.Visible);
            txtFilterValue.Text = string.Empty;
            txtFilterValue.Focus();
            
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            string FilterValue = txtFilterValue.Text.Trim();
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    {
                        FilterColumn = "IsReleased";
                        break;
                    };

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if(FilterValue == string.Empty || FilterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Empty;
                lblTotalRecords.Text = dgvDetainedLicenses.RowCount.ToString();
                return;
            }
            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = $"{FilterColumn} = {FilterValue}";
            }
            else
            {
                _dtDetainedLicenses.DefaultView.RowFilter = $"{FilterColumn} LIKE '{FilterValue}%'";
            }
            lblTotalRecords.Text = dgvDetainedLicenses.RowCount.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtDetainedLicenses.DefaultView.RowFilter = string.Empty;
            else
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblTotalRecords.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
