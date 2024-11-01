using DevExpress.Utils.Extensions;
using MyDVLD_Business;
using MyDVLD_Win_Form.Application.Local_Driving_Licenses;
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
    public partial class frmListLocalDrivingLicensesAppliaction : Form
    {
        private DataTable _dtAllLocalDrivingLicensesAppliaction;
        public frmListLocalDrivingLicensesAppliaction()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingLicensesAppliaction_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicensesAppliaction = clsLocalDrivingLicenseApplication.GetAllLocalApplications();
            dgvAllLocalApplications.DataSource = _dtAllLocalDrivingLicensesAppliaction;
            lblRecord.Text = _dtAllLocalDrivingLicensesAppliaction.Rows.Count.ToString();
            if (_dtAllLocalDrivingLicensesAppliaction.Rows.Count > 0)
            {
                dgvAllLocalApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvAllLocalApplications.Columns[0].Width = 120;

                dgvAllLocalApplications.Columns[1].HeaderText = "Driving Class";
                dgvAllLocalApplications.Columns[1].Width = 300;

                dgvAllLocalApplications.Columns[2].HeaderText = "National No.";
                dgvAllLocalApplications.Columns[2].Width = 150;

                dgvAllLocalApplications.Columns[3].HeaderText = "Full Name";
                dgvAllLocalApplications.Columns[3].Width = 350;

                dgvAllLocalApplications.Columns[4].HeaderText = "Application Date";
                dgvAllLocalApplications.Columns[4].Width = 170;

                dgvAllLocalApplications.Columns[5].HeaderText = "Passed Tests";
                dgvAllLocalApplications.Columns[5].Width = 150;
            }
            cbFilterBy.SelectedIndex = 0;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Focus();
                txtFilterValue.Text = string.Empty;
            }
            _dtAllLocalDrivingLicensesAppliaction.DefaultView.RowFilter = "";
            lblRecord.Text = _dtAllLocalDrivingLicensesAppliaction.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = string.Empty;
           
            switch (cbFilterBy.SelectedItem)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "ApplicationStatus";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicensesAppliaction.DefaultView.RowFilter = "";
                lblRecord.Text = _dtAllLocalDrivingLicensesAppliaction.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "LocalDrivingLicenseApplicationID")
            {
                _dtAllLocalDrivingLicensesAppliaction.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllLocalDrivingLicensesAppliaction.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            lblRecord.Text = _dtAllLocalDrivingLicensesAppliaction.Rows.Count.ToString();
        }
        private void btnLocalLicense_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            this.Hide();
            frm.ShowDialog();
            this.Show();
            frmListLocalDrivingLicensesAppliaction_Load(null, null);
        }
        private void smShowApplicationDetails_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = (int)dgvAllLocalApplications.CurrentRow.Cells[0].Value;
            frm_Local_DrivingLicense_Application_Info frm = new frm_Local_DrivingLicense_Application_Info(LocalDrivingLicenseAppID);
            frm.ShowDialog();
            frmListLocalDrivingLicensesAppliaction_Load(null,null);
        }

        private void smEditApplication_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = (int)dgvAllLocalApplications.CurrentRow.Cells[0].Value;

            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication(LocalDrivingLicenseAppID);
            this.Hide();
            frm.ShowDialog();
            this.Show();
            frmListLocalDrivingLicensesAppliaction_Load(null, null);
        }

        private void smDeleteApplication_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            int LocalDrivingLicenseAppID = (int)dgvAllLocalApplications.CurrentRow.Cells[0].Value;
            
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseAppID);
            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingLicensesAppliaction_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void smCancelApplication_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseAppID = (int)dgvAllLocalApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseAppID);
            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingLicensesAppliaction_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseAppID = (int)dgvAllLocalApplications.CurrentRow.Cells[0].Value;
            bool IsExistLicense = false;//فقط تشوف عندة رخصة قيادة 

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseAppID);
            IsExistLicense = LocalDrivingLicenseApplication.IsLicenseIssued();

            int TotalPassedTests = (int)dgvAllLocalApplications.CurrentRow.Cells[5].Value;
            smEditApplication.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication._enApplicationStatus.New);
            smDeleteApplication.Enabled = smEditApplication.Enabled;
            smCancelApplication.Enabled = smEditApplication.Enabled;
            smIssueDrivingLicenseFirstTime.Enabled = (TotalPassedTests == 3) && !IsExistLicense;
            smShowLicense.Enabled = IsExistLicense;


            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.StreetTest);

            smScheduleTests.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication._enApplicationStatus.New);
            if (smScheduleTests.Enabled)
            {
                smScheduleVisionTest.Enabled = !PassedVisionTest;
                smScheduleWrittenTest.Enabled = PassedVisionTest && !PassedWrittenTest;
                smScheduleStreetTest.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }
        }

        private void _ScheduleTest(clsTestTypes.enTestType testType)
        {
            int LocalDrivingLicenseAppID = (int)dgvAllLocalApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseAppID, testType) ;
            this.Hide();
            frm.ShowDialog();
            this.Show();
            frmListLocalDrivingLicensesAppliaction_Load(null, null);
        }
        private void smScheduleVisionTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.VisionTest);
        }

        private void smScheduleWrittenTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.WrittenTest);
        }

        private void smScheduleStreetTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.StreetTest);
        }

        private void smIssueDrivingLicenseFirstTime_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = (int)dgvAllLocalApplications.CurrentRow.Cells[0].Value;
            frmIssueDrivingLicenseForTheFirstTime firstTime = new frmIssueDrivingLicenseForTheFirstTime(LocalDrivingLicenseAppID);
            this.Hide();
            firstTime.ShowDialog();
            this.Show();
            frmListLocalDrivingLicensesAppliaction_Load(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this .Close();
        }

        private void smShowLicense_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = (int)dgvAllLocalApplications.CurrentRow.Cells[0].Value;
            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseAppID).GetActiveLicenseID();
            if (LicenseID != -1) 
            {
                frmLicenseInfo licenseInfo = new frmLicenseInfo(LicenseID);
                licenseInfo.ShowDialog();
            }
        }

        private void smShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvAllLocalApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            frmLicenseHistory frm = new frmLicenseHistory(localDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
