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
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace MyDVLD_Win_Form
{
    public partial class frmChangePassword : Form
    {
     
        private int _UserID;
        private clsUser _Currentuser;
        public frmChangePassword(int UserID  )
        {
            InitializeComponent();
            this._UserID = UserID;
        }
        private void _ResetDefualtValues()
        {
            tbCurrentPassword.Text = "";
            tbNewPassword.Text = "";
            tbConfirmPassword.Text = "";
            tbCurrentPassword.Focus();
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _Currentuser = clsUser.FindByUserID(_UserID);

            if (_Currentuser == null) 
            {
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
            }
            ctrlUserCard1._LoadInfoUser(_UserID);
        }
        //
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ChangePassword()
        {
            _Currentuser.Password = tbNewPassword.Text;

            if (_Currentuser.Save())
            {
                MessageBox.Show("Password Changed Successfully.",
                   "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefualtValues();
            }
            else
            {
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            _ChangePassword();
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            if (tbConfirmPassword.Text.Trim() != tbNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "Password Confirmation does not match New Password!");
            }
            else
            {
                errorProvider1.SetError(tbConfirmPassword, null);
            };
        }

        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbCurrentPassword, "Password cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(tbCurrentPassword, null);
            };
        }


    }
}
