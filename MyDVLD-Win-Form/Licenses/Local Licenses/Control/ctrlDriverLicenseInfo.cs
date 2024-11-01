using MyDVLD_Business;
using MyDVLD_Win_Form.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDVLD_Win_Form
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID;
        private clsLicense _LicenseInfo;
        public int LicenseID
        {
            get { return _LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get
            { return _LicenseInfo; }
        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }
        private void _LoadImage()
        {
            pbPersonImage.Image = lblGendor.Text == "Male" ? Resources.Male_512 : Resources.Female_512;
            string ImagePath = _LicenseInfo.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != null) 
            {
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void LoadData(int LicenseID)
        {
            _LicenseID = LicenseID;
            _LicenseInfo = clsLicense.Find(LicenseID);
            if (_LicenseInfo == null) 
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }
            lblClass.Text = _LicenseInfo.LicenseClassInfo.ClassName;
            lblFullName.Text = _LicenseInfo.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _LicenseID.ToString() ;
            lblNationalNo.Text = _LicenseInfo.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text =  _LicenseInfo.DriverInfo.PersonInfo.Gender ? "Male" : "Female";
            lblIssueDate.Text = _LicenseInfo.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _LicenseInfo.ExpirationDate.ToShortDateString();
            lblNotes.Text = _LicenseInfo.Notes.ToString();
            lblIsActive.Text = _LicenseInfo.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _LicenseInfo.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text=_LicenseInfo.DriverID.ToString();
            lblIssueReason.Text = _LicenseInfo.GetIssueReasonText;
            lblIsDetained.Text = _LicenseInfo.IsDetained ? "Yes" : "No";
            _LoadImage();
        }
      
    }
}
