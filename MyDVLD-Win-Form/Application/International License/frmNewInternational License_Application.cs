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
    public partial class frmNewInternationalLicenseApplication : Form
    {

        private int  _InternationalID = -1;
       
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void _RefreshData()
        {
            lblApplicationDate.Text = DateTime.Now.GetDateTimeFormats().First().ToString();
            lblIssueData.Text = DateTime.Now.GetDateTimeFormats().First().ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).GetDateTimeFormats().First().ToString();
            lblFees.Text= clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCeatedBy.Text = Global.CurrentUser.UserName;
            
        }
    
        private void _IssueInternationalLicense()
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Cancelled");
                return;
            }


            clsInternationalLicense internationalLicense = new clsInternationalLicense();
            clsLicense licenseObject = ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo;
            //Base
            internationalLicense.ApplicantPersonID = licenseObject.DriverInfo.PersonID;
            internationalLicense.ApplicationDate = DateTime.Now;
            internationalLicense.LastStatusDate = DateTime.Now;
            internationalLicense.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            internationalLicense.CreatedByUserID = Global.CurrentUser.UserID;

            //sub
            internationalLicense.DriverID = licenseObject.DriverInfo.DriverID;
            internationalLicense.IssuedUsingLocalLicenseID = licenseObject.LicenseID;
            internationalLicense.ExpirationDate = DateTime.Now.AddYears(10);//يمكن وضع تيبل في الداتا بيز خاص ب اعدادات النظام مثل صلاحية الرخصة الدولية
            if (!internationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            _InternationalID = internationalLicense.InternationalLicenseID;
            lbInternationl_LicenseID.Text = _InternationalID.ToString();
            lbApplicationID.Text = internationalLicense.ApplicationID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + _InternationalID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrlDrivingLicenseWithFilter21.FilterEnable = false;
            lilblShowLicenseslnfo.Enabled = true;
        }
        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            _RefreshData();
        }

       

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _IssueInternationalLicense();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       

        private void ctrlDrivingLicenseWithFilter21_OnLicenseID(int obj)
        {
          
            if (obj < 1) 
            {
                return;
            }
            lblLocalLicenseID.Text = obj.ToString();
            lilblShowLicensesHistory.Enabled = true;
            if (ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _InternationalID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.DriverID);
            if (_InternationalID > 1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + _InternationalID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }
            btnIssue.Enabled = true;
        }

  
        private void lilblShowLicenseslnfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_International_Driving_Info frm_International = new frm_International_Driving_Info(_InternationalID);
            
            frm_International.ShowDialog();
        }

        private void lnlblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory LicenseHistory = new frmLicenseHistory(ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.DriverInfo.PersonID);

            LicenseHistory.ShowDialog();
        }

      
    }
}
