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
    public partial class Renew_Local_Driving_License : Form
    {
        
        private int _NewLicenseID;
       
        public Renew_Local_Driving_License()
        {
            InitializeComponent();
        }

  

        private void ctrlDrivingLicenseWithFilter21_OnLicenseID(int obj)
        {
            int SelectedPersonID = obj;
            if (SelectedPersonID == -1)
            {
                return;
            }
            lblOldLicenseID.Text= SelectedPersonID.ToString();
            llShowLicenseHistory.Enabled= true;
            int DefaultValidityLength = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength;
            lblExpirationDate.Text = DateTime.Now.AddYears(DefaultValidityLength).ToString();
            lblLicenseFees.Text = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.Notes;

            if (!ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {

                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate.ToShortDateString()
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            //check the license is not Expired.
            if (!ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }
            btnRenewLicense.Enabled = true;
        }

  
        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            
      
            if(MessageBox.Show("Are you Sure to Save?", " Allow Processed",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question)!=DialogResult.OK)
            {
                MessageBox.Show("Cancelled");
                return;
            }
            clsLicense NewLicense = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(), Global.CurrentUser.UserID); ;
            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            lblR_L_ApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text =_NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnRenewLicense.Enabled = false;
            ctrlDrivingLicenseWithFilter1.FilterEnable = false;
            llShowLicenseInfo.Enabled = true;
        }


        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //frmLicenseHistory LicenseHistory = new frmLicenseHistory(_PersonID);

            //LicenseHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_NewLicenseID);
            this.Hide();
            frmLicenseInfo.ShowDialog();
            this.Show();
        }

        private void Renew_Local_Driving_License_Load(object sender, EventArgs e)
        {
            btnRenewLicense.Enabled = false;
            ctrlDrivingLicenseWithFilter1.txtLicenseIDFocus();
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            lblApplicationDate.Text=DateTime.Now.ToShortDateString();
            lblIssueDate.Text=DateTime.Now.ToShortDateString();
            lblApplicationFees.Text= clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName; 

        }

        private void Renew_Local_Driving_License_Activated(object sender, EventArgs e)
        {
            ctrlDrivingLicenseWithFilter1.txtLicenseIDFocus();
        }
    }
}
