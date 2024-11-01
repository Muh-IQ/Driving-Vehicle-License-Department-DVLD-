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
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _LicenseID;
        
        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
            ctrlDrivingLicenseWithFilter21.txtLicenseIDFocus();
        }
        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            this._LicenseID = LicenseID;
            ctrlDrivingLicenseWithFilter21.LoadData(LicenseID);
            ctrlDrivingLicenseWithFilter21.FilterEnable = false;
        }

        private void ctrlDrivingLicenseWithFilter21_OnLicenseID(int obj)
        {
            _LicenseID = obj;
            if (obj < 1) 
            {
                return;
            }
            lblLicenseID.Text = _LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;
            if (!ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsDetian detian = ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.DetianInfo;
            lblDetainID.Text = detian.DetainID.ToString();
            lblDetainDate.Text = detian.DetainDate.ToShortDateString();
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblCreatedByUser.Text = detian.CreatedByUserID.ToString();
            lblFineFees.Text = detian.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();
            
            btnRelease.Enabled = true;

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Cancelled");
                return;
            }
            int? ApplicationID = null;
            bool IsRelease = ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.ReleaseDetainedLicense(Global.CurrentUser.UserID, ref ApplicationID);

            if (!IsRelease)
            { 
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblApplicationID.Text = ApplicationID.ToString();
            btnRelease.Enabled = false;
            ctrlDrivingLicenseWithFilter21.FilterEnable = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory LicenseHistory = new frmLicenseHistory(ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.DriverInfo.PersonID);
            LicenseHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_LicenseID);
            this.Hide();
            frmLicenseInfo.ShowDialog();
            this.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
