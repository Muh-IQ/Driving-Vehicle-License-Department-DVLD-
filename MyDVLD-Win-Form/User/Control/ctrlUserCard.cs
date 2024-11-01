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
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _User = null;
        public ctrlUserCard()
        {
            InitializeComponent();
            
        }

        private void _FillInfoUser(clsUser user)
        {
            lblUserID.Text = user.UserID.ToString();
            lblUserName.Text = user.UserName.ToString();    
            lblIsActive.Text = user.IsActive.ToString();
        }

        //Load All Data in once Function 
        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonData(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";
        }
        public void _LoadInfoUser(int UserID)
        {
             _User = clsUser.FindByUserID(UserID);
            if (_User == null) 
            {
                _ResetDefaultValues();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }

  
        private void _ResetDefaultValues()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

    }
}
