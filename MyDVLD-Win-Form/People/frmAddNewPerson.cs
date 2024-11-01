using MyDVLD_Business;
using MyDVLD_Win_Form.GloablClasses;
using MyDVLD_Win_Form.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace MyDVLD_Win_Form
{
    public partial class frmAddNewPerson : Form
    {

        public delegate void DataBackEventHandler(int PersonID);
        public event DataBackEventHandler DataBack;
        enum enMode { AddNew, Update };
        private enMode _Mode;
        private clsPerson _CurrentPerson;
        private int _PersonID = -1;
        public bool IsSaved { get; set; } = false;

        public frmAddNewPerson()
        {
            InitializeComponent();
           
                _Mode = enMode.AddNew;
           
        }
        public frmAddNewPerson(int PersonID)
        {
            InitializeComponent();
         
                _Mode = enMode.Update;
                _PersonID = PersonID;

        }

        private void _SendDataBack()
        {
            int PersonID = Convert.ToInt32(lblPersonD.Text);
            DataBack?.Invoke(PersonID);           
        }
        
        private void _ChangeGenderStatus()
        {
            if (string.IsNullOrEmpty(pbPersonPhoto.ImageLocation))
            {
                pbPersonPhoto.Image = (rbMale.Checked) ? Resources.User_Male_Photo : Resources.User_Female_Photo;
            }
        }

        private void _ReseteDefulteValue()
        {
            _FillComboBoxWithCountries();
            if (_Mode == enMode.AddNew) 
            {
                _CurrentPerson = new clsPerson();
                lblHeader.Text = "Add New Person";
            }
            else
            {
                lblHeader.Text = "Update Person";
            }

            _ChangeGenderStatus();

            //process lable Remove Image
            lnlRemoveImage.Visible = (pbPersonPhoto.ImageLocation != null);
            //Process date
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
            dateTimePicker1.MaxDate= DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;

            cbNationality.SelectedIndex = 0;

            txtFirst.Text = string.Empty;
            txtSecond.Text = string.Empty;
            txtThird.Text = string.Empty;
            txtLast.Text = string.Empty;
            txtNationalNo.Text = string.Empty;
           
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
        }

        private void _FillComboBoxWithCountries()
        {
            cbNationality.DisplayMember = "CountryName";
            cbNationality.ValueMember = "CountryID";

            cbNationality.DataSource = clsCountry.ListCounties();
        }
        private void _ChangeLebalImage()
        {
            if(!string.IsNullOrEmpty(pbPersonPhoto.ImageLocation))
            {
                lnlSetImage.Text = "Change Image";
                lnlRemoveImage.Visible = true;
            }
            else
            {
                lnlSetImage.Text = "Set Image";
                lnlRemoveImage.Visible = false;
            }
        }


        //FillCurrent object : _CurrentPerson
        private void _FillPersonData()
        {
            _CurrentPerson.FirstName = txtFirst.Text;
            _CurrentPerson.SecondName = txtSecond.Text;
            _CurrentPerson.ThirdName = txtThird.Text;
            _CurrentPerson.LastName = txtLast.Text;
            _CurrentPerson.NationalNo = txtNationalNo.Text;
            _CurrentPerson.Gender = rbMale.Checked;
            _CurrentPerson.DateOfBirth = Convert.ToDateTime(dateTimePicker1.Value);
            _CurrentPerson.Email = txtEmail.Text;
            _CurrentPerson.Address = txtAddress.Text;
            _CurrentPerson.Phone = txtPhone.Text;
            _CurrentPerson.CountryID = Convert.ToInt32(cbNationality.SelectedValue);
            _CurrentPerson.ImagePath = pbPersonPhoto.ImageLocation;
        }

        private void _LoadData()
        {
            _CurrentPerson = clsPerson.FindByPersonID(_PersonID);
            if (_CurrentPerson == null) 
            {
                MessageBox.Show("No Person With ID : "+_PersonID,"Not Found Person",MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            txtFirst.Text  = _CurrentPerson.FirstName;
            txtSecond.Text = _CurrentPerson.SecondName;
            txtThird.Text  = _CurrentPerson.ThirdName;
            txtLast.Text   = _CurrentPerson.LastName;
            txtNationalNo.Text    = _CurrentPerson.NationalNo;
            dateTimePicker1.Value = _CurrentPerson.DateOfBirth;
            txtEmail.Text         = _CurrentPerson.Email;
            txtAddress.Text       = _CurrentPerson.Address;
            txtPhone.Text         = _CurrentPerson.Phone;
            cbNationality.SelectedValue = _CurrentPerson.CountryID;


            if (!string.IsNullOrEmpty(_CurrentPerson.ImagePath))
            {
                pbPersonPhoto.ImageLocation = _CurrentPerson.ImagePath;
            }

            if (_CurrentPerson.Gender) //male = true
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;
            lnlRemoveImage.Visible = (_CurrentPerson.ImagePath != null);
        }

        private bool _HandlePersonImage()
        {

            //في حالة الاضافة يكون _CurrentPerson.ImagePath is Null or Empty
            /// في حالة التحديث اذا حدث الصورة  يكون مسار الصورة في الكائن يختلف عن الموجود في البكجر بوكس
            if (_CurrentPerson.ImagePath != pbPersonPhoto.ImageLocation) 
            {
                //It works when updating
                if (_CurrentPerson.ImagePath != "") 
                {
                    try
                    {
                        File.Delete(_CurrentPerson.ImagePath);
                    }
                    catch (IOException)
                    {

                    }
                }
                //It works when AddNew
                if (pbPersonPhoto.ImageLocation != null) 
                {
                    string ImagePath = pbPersonPhoto.ImageLocation;
                    //نسخ الصورة في ملف المشروع
                    if (clsUtil.CopyImageToProjectImageFolder(ref ImagePath))
                    {
                        pbPersonPhoto.ImageLocation = ImagePath;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }

        // Events
        private void btnCloseManage_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddNewPerson_Load(object sender, EventArgs e)
        {
            _ReseteDefulteValue();
            if (_Mode == enMode.Update) 
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (!_HandlePersonImage())
            {
                return;
            }

            //FillCurrent object : _CurrentPerson
            _FillPersonData();
          
            if (_CurrentPerson.Save())
            {
                lblPersonD.Text=_CurrentPerson.PersonID.ToString();
                _Mode = enMode.Update;
                lblHeader.Text = "Update Person";
                this.IsSaved = true;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _SendDataBack();
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private void lnlSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "اختر الصورة";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbPersonPhoto.ImageLocation = openFileDialog.FileName;
                _ChangeLebalImage();
            }

        }

        private void lnlRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonPhoto.Image = null;
            _CurrentPerson.ImagePath = "";
            pbPersonPhoto.ImageLocation = "";
            lnlRemoveImage.Visible = false;
            _ChangeGenderStatus();
            _ChangeLebalImage();
        }

        //استخدمت هذه الميثود في  rbMale and rbFemale
        //ثنينهن يستخدمن نفس الحدث
        private void rbGender_CheckedChanged(object sender, EventArgs e)
        {
            _ChangeGenderStatus();
        }

        private  void ValidatoinTextBox(object sender, CancelEventArgs e)
        {
            
            string Text = ((TextBox)sender).Text;
            if (string.IsNullOrEmpty(Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError((TextBox)sender, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError((TextBox)sender, null);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
          

            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _CurrentPerson.NationalNo && clsPerson.IsExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
           
            //validate email format
            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };
        }
    }
}
