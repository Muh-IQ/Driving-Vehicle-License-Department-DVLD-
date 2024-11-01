using MyDVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace MyDVLD_Win_Form
{
    public partial class ctrlDrivingLicenseHistory : UserControl
    {
        private clsDriver _clsDriver;
        private int _DriverID;
        private DataTable _dtLocalLicenses;
        private DataTable _dtInternationalLicenses;
        public ctrlDrivingLicenseHistory()
        {
            InitializeComponent();
        }
        public void Clear()
        {
            _dtInternationalLicenses.Clear();
            _dtLocalLicenses.Clear();
        }
        private void _LoadLocalLicenses()
        {
            _dtLocalLicenses = clsDriver.GetLicenses(_DriverID);
            if (_dtLocalLicenses.Rows.Count < 1 ) 
            {
                MessageBox.Show("Error In Load Local Licenses");
                cmsLocalLicenseHistory.Enabled = false;
                return;
            }

            dgvLocalLicensesHistory.DataSource = _dtLocalLicenses;
            lblLocalLicensesRecords.Text = _dtLocalLicenses.Rows.Count.ToString();

            dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
            dgvLocalLicensesHistory.Columns[0].Width = 110;

            dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
            dgvLocalLicensesHistory.Columns[1].Width = 110;

            dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
            dgvLocalLicensesHistory.Columns[2].Width = 270;

            dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
            dgvLocalLicensesHistory.Columns[3].Width = 170;

            dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
            dgvLocalLicensesHistory.Columns[4].Width = 170;

            dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
            dgvLocalLicensesHistory.Columns[5].Width = 110;
        }
        private void _LoadInternationalLicenses()
        {
            _dtInternationalLicenses = clsDriver.GetDriverInernationalLicenses(_DriverID);
            if (_dtInternationalLicenses.Rows.Count < 1) 
            {
                cmsInterenationalLicenseHistory.Enabled = false;
                MessageBox.Show("Error In Load Interenational Licenses");
                return;
            }

            dgvInternationalLicensesHistory.DataSource = _dtInternationalLicenses;
            lblInternationalLicensesRecords.Text = _dtInternationalLicenses.Rows.Count.ToString();

            dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
            dgvInternationalLicensesHistory.Columns[0].Width = 160;

            dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
            dgvInternationalLicensesHistory.Columns[1].Width = 130;

            dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
            dgvInternationalLicensesHistory.Columns[2].Width = 130;

            dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
            dgvInternationalLicensesHistory.Columns[3].Width = 180;

            dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
            dgvInternationalLicensesHistory.Columns[4].Width = 180;

            dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
            dgvInternationalLicensesHistory.Columns[5].Width = 120;

        }
        public void LoadData(int DriverID)
        {
            _DriverID=DriverID;
            _clsDriver = clsDriver.FindByDriverID(_DriverID);
            if (_clsDriver == null) 
            {
                MessageBox.Show($"Not Found Driver With ID {DriverID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadLocalLicenses();
            _LoadInternationalLicenses();
        }
        public void LoadDataByPersonID(int PersonID)
        {

            _clsDriver = clsDriver.FindByPersonID(PersonID);
            if (_clsDriver == null)
            {
                MessageBox.Show($"Not Found Driver With ID {PersonID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _clsDriver.DriverID;
            _LoadLocalLicenses();
            _LoadInternationalLicenses();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frmLicenseInfo.ShowDialog();

        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_International_Driving_Info frm_International = new frm_International_Driving_Info((int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value);
            frm_International.ShowDialog();
        }
    }
}
