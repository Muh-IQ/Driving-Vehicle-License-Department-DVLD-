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
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        private clsInternationalLicense _InternationalLicense;
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        private void _LoadImage()
        {
            pbImage.Image = lblGender.Text == "Male" ? Resources.Male_512 : Resources.Female_512;
            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != null)
            {
                if (File.Exists(ImagePath))
                {
                    pbImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void LoadData(int InternationalLicenseID)
        {
            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);
            if (_InternationalLicense == null) 
            {
                MessageBox.Show("Error in load data");
                return;
            }


                lblInlLicenseID.Text = InternationalLicenseID.ToString();
                lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
                lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
                lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();
                lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
                lblDriverID.Text = _InternationalLicense.DriverID.ToString();
                lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
                lblName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
                lblDateOtBirth.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
                lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
                lblGender.Text = _InternationalLicense.DriverInfo.PersonInfo.Gender ? "Male" : "Female";
            _LoadImage();



        }
       
    }
}
