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

namespace MyDVLD_Win_Form.Application.Local_Driving_Licenses
{
    public partial class frmIssueDrivingLicenseForTheFirstTime : Form
    {
        private int _LocalDrivingLicenseID;
        private  clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        public frmIssueDrivingLicenseForTheFirstTime(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            _LocalDrivingLicenseID=LocalDrivingLicenseID;
        }

        private void frmIssueDrivingLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            tbNotes.Focus();
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseID);
            if (_localDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_localDrivingLicenseApplication.PassedAllTest())
            {
                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            int LicenseID = _localDrivingLicenseApplication.GetActiveLicenseID();
            if (LicenseID > 0) 
            {
                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrl_LocalDrivingLicenseApplicationInfo2.LoadApplicationByLocalDrivingApp(_LocalDrivingLicenseID);
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int? LicenseID = _localDrivingLicenseApplication.IssueLicenseForFirstTime(tbNotes.Text, Global.CurrentUser.UserID);
            if (LicenseID.HasValue) 
            {

                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
