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
using Microsoft.Win32;

namespace MyDVLD_Win_Form
{
    public partial class frmLoginScreen : Form
    {
        private enum _enModeRegister { Read = 0, Write = 1 };
        private _enModeRegister _ModeReg= _enModeRegister.Read;
        public frmLoginScreen()
        {
            InitializeComponent();

        }
        private bool ReadOrWriteToRegister()
        {
            string StringPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
            string ValueNameUser = "UserName";
            string ValueDataUser = tbUserName.Text.Trim();
            string ValueNamePass = "Password";
            string ValueDataPass = tbPassword.Text.Trim();
            if (Convert.ToBoolean( _ModeReg))//Write
            {
                try
                {
                    Registry.SetValue(StringPath, ValueNameUser, ValueDataUser, RegistryValueKind.String);
                    Registry.SetValue(StringPath, ValueNamePass, ValueDataPass, RegistryValueKind.String);
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error in Write toRegister : {ex.Message}");
                    return false;
                }
                
            }
            else   //Read
            {
                try
                {
                    ValueDataUser = Registry.GetValue(StringPath, ValueNameUser, default) as string;
                    ValueDataPass = Registry.GetValue(StringPath, ValueNamePass, default) as string;
                    if (ValueDataUser != string.Empty && ValueDataPass != string.Empty)
                    {
                        tbUserName.Text = ValueDataUser;
                        tbPassword.Text = ValueDataPass;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Error in Write toRegister : {ex.Message}");
                    return false;
                }
            }
            return true;
        }
       
        private bool _CheckLogin()
        {
            clsUser user = clsUser.FindByUserNameAndPassword(tbUserName.Text,tbPassword.Text);
            if (user != null) 
            { 
             
                    if (!user.IsActive )
                    {
                        MessageBox.Show("Account is Not Active!");
                        return false;
                    }

                if(!cbRememberMe.Checked) 
                {
                    tbPassword.Text = tbUserName.Text = string.Empty;
                }
                _ModeReg = _enModeRegister.Write;
                ReadOrWriteToRegister();

                Global.CurrentUser = user;
                    return true;
                
            }
            MessageBox.Show("Error in UserName/Password!");
            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_CheckLogin()) 
            {
                frmMain form = new frmMain(this);
                this.Hide();
                //tbUserName.Clear();
                //tbPassword.Clear();
                form.ShowDialog();
            }
           
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            _ModeReg = _enModeRegister.Read;
            cbRememberMe.Checked = ReadOrWriteToRegister();
            
        }
    }
}
