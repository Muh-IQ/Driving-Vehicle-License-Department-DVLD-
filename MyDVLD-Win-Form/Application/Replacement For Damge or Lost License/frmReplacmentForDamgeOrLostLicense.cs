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
    public partial class frmReplacmentForDamgeOrLostLicense : Form
    {

        private int _NewLicenseID;

        public frmReplacmentForDamgeOrLostLicense()
        {
            InitializeComponent();
        }
        private byte _GetAppTypeID()
        {
            return (byte) ((rbDamgeLicense.Checked) ? clsApplication.enApplicationType.ReplaceDamagedDrivingLicense : clsApplication.enApplicationType.ReplaceDamagedDrivingLicense);
        }
        private clsLicense.enIssueReason _GetIssueReason()
        {
            return (rbDamgeLicense.Checked) ? clsLicense.enIssueReason.ReplacementforDamaged : clsLicense.enIssueReason.Replacementforlost;
        }
        private void rbDamgeLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacement For Damge License";
            lblHeader.Text = this.Text;
            lblApplicationFees.Text =clsApplicationTypes.Find(_GetAppTypeID()).ApplicationFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacement For Lost License";
            lblHeader.Text = this.Text;
            lblApplicationFees.Text = clsApplicationTypes.Find(_GetAppTypeID()).ApplicationFees.ToString();
        }


        private void btnIssueReplacmentLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure about the Issue license?", "Issue License", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                MessageBox.Show("Canceled");
                return;  

            }
            clsLicense NewLicense = ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.ReplaceLicense(_GetIssueReason(), Global.CurrentUser.UserID);
            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            btnIssueReplacmentLicense.Enabled = false;
            ctrlDrivingLicenseWithFilter21.FilterEnable = false;
            groupBox1.Enabled = false;
            llShowLicenseInfo.Enabled = true;

            lblR_L_ApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            lblReplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ctrlDrivingLicenseWithFilter21_OnLicenseID(int obj)
        {
            if (obj <= 0)
                return;

            lblOldLicenseID.Text = obj.ToString();
            llShowLicenseHistory.Enabled = true;  
            if(!ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.IsActive) 
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                   , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacmentLicense.Enabled = false;
                return;
            }
            btnIssueReplacmentLicense.Enabled = true;
        }

        private void frmReplacmentForDamgeOrLostLicense_Load(object sender, EventArgs e)
        {
            rbDamgeLicense.Checked = true;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_NewLicenseID);
            this.Hide();
            frmLicenseInfo.ShowDialog();
            this.Show();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //frmLicenseHistory LicenseHistory = new frmLicenseHistory(_PersonID);
            //LicenseHistory.ShowDialog();
        }

        private void gpApplicationInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
