using MyDVLD_Win_Form.Application.Application_Types;
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
    public partial class frmMain : Form
    {
        private frmLoginScreen _Login;
        public frmMain(frmLoginScreen login)
        {
            InitializeComponent();
            _Login = login;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonsList frm = new frmPersonsList();
            //frm.MdiParent = this;
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Manager_User();
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblLoginUser.Text = Global.CurrentUser.UserName.ToString().Trim();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.CurrentUser = null;
            _Login.Show();
            this.Close();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmUserInfo(Global.CurrentUser.UserID );
            form.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmChangePassword(Global.CurrentUser.UserID);
            form.ShowDialog();
        }

        private void localLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form form = new frmAddUpdateLocalDrivingLicesnseApplication();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void manageApplicatonsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmManageApplicationTypes();
            form.ShowDialog();
        }

        private void listApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lostDrivingLlicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacmentForDamgeOrLostLicense frm = new frmReplacmentForDamgeOrLostLicense();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void detianToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes form = new frmListTestTypes();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void locToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicensesAppliaction frm = new frmListLocalDrivingLicensesAppliaction();
            frm.ShowDialog();
        }

        private void internationlLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void localDrivingLicensesApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicensesApplication frm = new frmListInternationalLicensesApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void renewDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Renew_Local_Driving_License frm = new Renew_Local_Driving_License();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDriver frm = new frmListDriver();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicensesAppliaction frm = new frmListLocalDrivingLicensesAppliaction();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void manageDetianLicensesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void manageDetianLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void relaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void releasDrivingLicneseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }


    }
}
