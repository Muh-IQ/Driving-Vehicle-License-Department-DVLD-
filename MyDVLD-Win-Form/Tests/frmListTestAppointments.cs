using MyDVLD_Business;
using MyDVLD_Win_Form.Properties;
using System;
using System.Windows.Forms;
using System.Data;
using static MyDVLD_Business.clsTestTypes;
namespace MyDVLD_Win_Form
{
    public partial class frmListTestAppointments : Form
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsTestTypes.enTestType _TestTypeID;
        private DataTable _dtAllTestAppointments;
        
        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID = clsTestTypes.enTestType.VisionTest)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this._TestTypeID = TestTypeID;
        }
        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    this.lblTitle.Text = "Vision Test Appointments";
                    pbTestTypeImage.Image = Resources.Vision_512;
                    break;
                case clsTestTypes.enTestType.WrittenTest:
                    this.lblTitle.Text = "Written Test Appointments";
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    break;
                case clsTestTypes.enTestType.StreetTest:
                    this.lblTitle.Text = "Street Test Appointments";
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    break;
            }
            this.Text = this.lblTitle.Text;
        }

        private void _RefreashData()
        {

            _dtAllTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_TestTypeID, _LocalDrivingLicenseApplicationID);
            dgvLicenseTestAppointments.DataSource = _dtAllTestAppointments;
            lblRecord.Text= dgvLicenseTestAppointments.RowCount.ToString();
        }
        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {
            ctrl_LocalDrivingLicenseApplicationInfo1.LoadApplicationByLocalDrivingApp(_LocalDrivingLicenseApplicationID);
            _LoadTestTypeImageAndTitle();
            _dtAllTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_TestTypeID, _LocalDrivingLicenseApplicationID);
            dgvLicenseTestAppointments.DataSource = _dtAllTestAppointments;
            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CurrentAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmTakeTest frm = new frmTakeTest( CurrentAppointmentID, _TestTypeID);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                _RefreashData();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CurrentAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            {
                //تحقق اذا مقفول من هنا و كذلك يوجد من داخل الكونترول لذلك علقت هذا التحقق
                //clsTestAppointment testAppointment = clsTestAppointment.Find(CurrentAppointmentID);
                //if (testAppointment != null)
                //{
                //    if (testAppointment.IsLocked)
                //    {
                //        MessageBox.Show("The test cannot be Edit because it is locked" , "Not Allow Process",MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}
            }

            frmScheduleTests scheduleTest = new frmScheduleTests(_LocalDrivingLicenseApplicationID, _TestTypeID, CurrentAppointmentID);
            scheduleTest.ShowDialog();
            _RefreashData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest( _TestTypeID))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmScheduleTests scheduleTests;
            clsTests tests = localDrivingLicenseApplication.GetLastTestPerTestType(_TestTypeID);
            if (tests == null) 
            {
                scheduleTests = new frmScheduleTests(_LocalDrivingLicenseApplicationID, _TestTypeID);
                this.Hide();
                scheduleTests.ShowDialog();
                this.Show();
                _RefreashData();
                return;
            }
            //if person already passed the test s/he cannot retak it.
            if (tests.TestResult)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            scheduleTests = new frmScheduleTests(_LocalDrivingLicenseApplicationID, _TestTypeID);
            this.Hide();
            scheduleTests.ShowDialog();
            this.Show();
            _RefreashData();
        }
    }
}
