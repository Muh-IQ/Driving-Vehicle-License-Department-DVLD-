using DevExpress.Utils.About;
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
    public partial class frmDetainLicenseApplication : Form
    {
        private int _LicenseID;

        private int? _DetainLicenseID;
    
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }



        private void ctrlDrivingLicenseWithFilter21_OnLicenseID(int obj)
        {
            _LicenseID = obj;
            if (_LicenseID < 1)
            {
                return;
            }

            if (ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblLicenseID.Text = _LicenseID.ToString();
            btnDetain.Enabled = true;
            llShowLicenseHistory.Enabled = true;

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Cancelled");
                return;
            }
            _DetainLicenseID = ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.Detain(Convert.ToDecimal(txtFineFees.Text.Trim()), Global.CurrentUser.UserID);
            if(_DetainLicenseID == null)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            lblDetainID.Text= _DetainLicenseID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlDrivingLicenseWithFilter21.FilterEnable = false;
            btnDetain.Enabled = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

   
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_LicenseID);
            this.Hide();
            frmLicenseInfo.ShowDialog();
            this.Show();
        }

      
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory LicenseHistory = new frmLicenseHistory(ctrlDrivingLicenseWithFilter21.SelectedLicenseInfo.DriverInfo.PersonID);
            LicenseHistory.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            ctrlDrivingLicenseWithFilter21.txtLicenseIDFocus();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Field is Required");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, string.Empty);

            }
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
