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
using static MyDVLD_Business.clsPerson;

namespace MyDVLD_Win_Form
{
    public partial class Add_New_User : Form
    {
        private clsUser _CurrentUser;        
        private int _UserID;
        private int _PersonID;
        public  bool IsSave = false;
        private enum _enMode { Update,AddNew}
        private _enMode _Mode;
        public Add_New_User()
        {
            InitializeComponent();
            _Mode = _enMode.AddNew;
        }
        public Add_New_User(int UserID)
        {
            InitializeComponent();
            _Mode = _enMode.Update;
            _UserID = UserID;       
        }


        private void _ResetDefualtValues()
        {
            if (_Mode == _enMode.Update)
            {
                this.Text = "Update User";
                lblHeader.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                this.Text = "Add New User";
                lblHeader.Text = "Add New User";
                _CurrentUser = new clsUser();
                tpLoginInfo.Enabled = false;
                btnSave.Enabled = false;
            }

            tbUserName.Text=string.Empty;
            tbPassword.Text=string.Empty;
            tbConfirmPassword.Text=string.Empty;
            chbIsActive.Checked = true;
        }
        private void _LoadData()
        {
            _CurrentUser = clsUser.FindByUserID(_UserID);
            if (_CurrentUser != null)
            {
                lblUserID.Text = _UserID.ToString();
                tbUserName.Text = _CurrentUser.UserName;
                tbPassword.Text = _CurrentUser.Password;
                tbConfirmPassword.Text = _CurrentUser.Password;
                chbIsActive.Checked = _CurrentUser.IsActive;
                ctrlPersonCardWithFilter1.LoadPersonInfo(_CurrentUser.PersonID);
                ctrlPersonCardWithFilter1.EnableFilter = false;
            }
            else
                MessageBox.Show($"Can Not Load data for User ID : {_UserID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        private bool _Save()
        {
            
            {
                _CurrentUser.IsActive = chbIsActive.Checked;
                _CurrentUser.PersonID = _PersonID;
                _CurrentUser.UserName = tbUserName.Text;
                _CurrentUser.Password = tbConfirmPassword.Text;
                _CurrentUser.Permission = -1;
            }
            
            return _CurrentUser.Save();
            
        }
        private bool _ControlButton()
        {
            switch(_Mode) 
            {
                case _enMode.Update:
                    {
                        btnSave.Enabled = true;
                        tpLoginInfo.Enabled = true;
                        return true;
                    }
                case _enMode.AddNew: 
                    {
                        if (ctrlPersonCardWithFilter1.PersonID < 1)
                        {
                            MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ctrlPersonCardWithFilter1.FilterFocus();
                            return false;
                        }
                        if (clsUser.IsExistByPersonID(ctrlPersonCardWithFilter1.PersonID))
                        {
                            MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ctrlPersonCardWithFilter1.FilterFocus();
                            return false;
                        }
                        btnSave.Enabled = true;
                        tpLoginInfo.Enabled = true;
                        return true;
                    }
               
            }
            return false;
            
        }
        private void btnCloseManage_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_ControlButton())
            {
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                //tcUserInfo.SelectedIndex = 1;
            }
            
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_Save())
            {
                _Mode = _enMode.Update;
                lblUserID.Text = _CurrentUser.UserID.ToString();
                IsSave = true;
                lblHeader.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            // e.Cancel = true;
            if (_Mode == _enMode.Update)
            {
                return;
            }
            if(clsUser.IsExistByUserName(tbUserName.Text))
            {
                errorProvider1.SetError(tbUserName, "Is Exist UserName , choose another");
                tbUserName.Focus();
            }
            else
            {
                errorProvider1.SetError(tbUserName, "");
            }
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(!(tbPassword.Text == tbConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "ConfirmPassword not equale Password");
            }
            else
            {
                errorProvider1.SetError(tbConfirmPassword, "");
            }
        }

        private void Add_New_User_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == _enMode.Update)
            {
                _LoadData();
            }

        }


        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {     
            if (!(tbPassword.Text == tbConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbPassword, "Password not equale ConfirmPassword");
            }
            else
            {
                errorProvider1.SetError(tbPassword, "");
            }
        }

        private void Add_New_User_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
