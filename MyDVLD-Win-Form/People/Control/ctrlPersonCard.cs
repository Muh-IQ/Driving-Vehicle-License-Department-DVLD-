using MyDVLD_Business;
using MyDVLD_Win_Form.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDVLD_Win_Form
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _currentPerson;
        private int _PersonID = -1;
        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return _currentPerson; }
        }
        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        
        public bool LoadPersonData(int PersonID)
        {
            _currentPerson = clsPerson.FindByPersonID(PersonID);
            if (_currentPerson != null)
            {
                FillControlCardWithPersonData();
                return true;
            }
            else
            {
                ResetPersonInfo();
                MessageBox.Show($"Not Found Person by Person ID : {PersonID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;

        }
        public bool LoadPersonData(string NationalNo)
        {

            _currentPerson = clsPerson.FindByNationalNo(NationalNo);

            if (_currentPerson != null)
            {
                FillControlCardWithPersonData();
                return true;
            }
            else
            {
                ResetPersonInfo();
                MessageBox.Show($"Not Found Person by National No {NationalNo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void FillControlCardWithPersonData()
        {
            lnlEditPerson.Enabled = true;
            _PersonID = _currentPerson.PersonID;
            lblPersonD.Text = _currentPerson.PersonID.ToString();
            lblFullName.Text = _currentPerson.FullName;
            lblNationalNO.Text = _currentPerson.NationalNo;
            lblGender.Text = _currentPerson.Gender == false ? "Female" : "Male";
            lblEmail.Text = _currentPerson.Email;
            lblAddress.Text = _currentPerson.Address;
            lblDateOfBirth.Text = _currentPerson.DateOfBirth.ToShortDateString();
            lblPhone.Text = _currentPerson.Phone;
            lblCountry.Text = clsCountry.Find(_currentPerson.CountryID).CountryName;

            _SetImage();
        }

        private void _SetImage()
        {
            string ImagePath = _currentPerson.ImagePath;
            if (!string.IsNullOrWhiteSpace(ImagePath))
            {
                if (File.Exists(ImagePath))
                {
                    pbImagePerson.ImageLocation = ImagePath;
                }
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                pbImagePerson.Image = _currentPerson.Gender == false ? Resources.User_Female_Photo : Resources.User_Male_Photo;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_currentPerson.PersonID < 0) 
            {
                MessageBox.Show("Choose Person!");
            }
            else
            {
                frmAddNewPerson form = new frmAddNewPerson(_currentPerson.PersonID);
                form.ShowDialog();
                if (form.IsSaved)
                {
                    LoadPersonData(_currentPerson.PersonID); 
                }
            }
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonD.Text = "[????]";
            lblNationalNO.Text = "[????]";
            lblFullName.Text = "[????]";
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbImagePerson.Image = Resources.Woman;

        }

    }
}
