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

namespace MyDVLD_Win_Form.Application.Control
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private clsApplication _application;
        private int _ApplicationID = -1;
        public int ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _application = clsApplication.FindBaseApplication(ApplicationID);
            if (_application == null) 
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillApplicationInfo();
        }
        private void FillApplicationInfo() 
        {
            _ApplicationID = _application.ApplicationID;
            lbl_ID.Text = _ApplicationID.ToString();
            lblStatus.Text = _application.ApplicationStatusText;
            lblFees.Text = _application.PaidFees.ToString();
            lblType.Text = _application.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicant.Text = _application.PersonInfo.FullName;
            lblDate.Text = _application.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _application.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = _application.UserInfo.UserName;
        }
        public void ResetApplicationInfo() 
        {
            _ApplicationID = -1;
            lbl_ID.Text = "(???)";
            lblStatus.Text = "(???)";
            lblFees.Text = "($$$)";
            lblType.Text = "(???)";
            lblApplicant.Text = "(???)";
            lblDate.Text = "[??/??/????]";
            lblStatusDate.Text = "[??/??/????]";
            lblCreatedBy.Text = "(???)";
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmPersonDetails(_application.ApplicantPersonID).ShowDialog();
            LoadApplicationInfo(_ApplicationID);
        }
    }
}
