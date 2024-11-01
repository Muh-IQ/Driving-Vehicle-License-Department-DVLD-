using MyDVLD_Business;
using MyDVLD_Win_Form.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MyDVLD_Business.clsApplication;

namespace MyDVLD_Win_Form
{
    public partial class ctrlSecheduleTest : UserControl
    {
        private enum _enMode { AddNew,Update}
        private _enMode _Mode;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private clsTestAppointment _TestAppointment;
        private clsTestTypes.enTestType _TestType;
        private int _LocalDrivingLicenseApplicationID;
        private int _TestAppointmentID = -1;
        public clsTestTypes.enTestType TestType 
        {  
            get { return _TestType; }

            set 
            {
                _TestType = value;
                switch(value) 
                {
                    case clsTestTypes.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    case clsTestTypes.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    case clsTestTypes.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                }
            }
        }
        public ctrlSecheduleTest()
        {
            InitializeComponent();
        }

        public void LoadData(int L_D_L_App_ID, int AppointmentID = -1)
        {
            if (AppointmentID == -1) 
            {
                _Mode = _enMode.AddNew;  
            }
            else 
            {
                _Mode = _enMode.Update; 
            }


            _TestAppointmentID =AppointmentID;
            _LocalDrivingLicenseApplicationID = L_D_L_App_ID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(L_D_L_App_ID);
            if (_LocalDrivingLicenseApplication == null) 
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;

            }
            
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.L_D_L_ApplacitionID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestType).ToString();
            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestType))
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;
            }
            else
            {
                _CreationMode = enCreationMode.FirstTimeSchedule;
            }

            if(_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                gbRetakeTestInfo.Enabled = true;
                lblRetakeAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
                lblRetakeTestAppID.Text = "N/A";
                lblTitle.Text = "Schedule Retake Test";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = "0";
            }

            if (_Mode == _enMode.AddNew)
            {
                _TestAppointment = new clsTestAppointment();
                dtpTestDate.MinDate = DateTime.Now;
                lblFees.Text = clsTestTypes.Find(_TestType).TestTypeFees.ToString();
            }
            else 
            {
                if (!_LoadTestAppointmentData())
                    return;
            }
            lblTotalFees.Text = (Convert.ToSingle(lblRetakeAppFees.Text) +
                Convert.ToSingle(lblFees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
                return;
            if (!_HandleAppointmentLockedConstraint())
                return;
            if (!_HandlePrviousTestConstraint())
                return;

        }
 

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null) 
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
            {
                dtpTestDate.MinDate = DateTime.Now;
            }
            else 
            {
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;
            }

            if(_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = "0";
            }
            else 
            {
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
            }
            return true;
        }
        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == _enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestType))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }

            return true;
        }
        private bool _HandleAppointmentLockedConstraint()
        {
            //if appointment is locked that means the person already sat for this test
            //we cannot update locked appointment
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            lblUserMessage.Visible = false;
            return true;
        }
        private bool _HandlePrviousTestConstraint()
        {
            //we need to make sure that this person passed the prvious required test before apply to the new test.
            //person cannno apply for written test unless s/he passes the vision test.
            //person cannot apply for street test unless s/he passes the written test.
            switch(_TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;
                case clsTestTypes.enTestType.WrittenTest:
                    if (_LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                       
                    }
                    return true;
                case clsTestTypes.enTestType.StreetTest:
                    if (_LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.StreetTest))
                    {
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;

                    }
                    return true;
            }
            return true;
        }
        private bool _HandleRetakeApplication()
        {
            if (_Mode == _enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication application = new clsApplication();
                application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                application.ApplicationDate = DateTime.Now.Date;
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                application.ApplicationStatus = _enApplicationStatus.Complated;
                application.LastStatusDate = DateTime.Now.Date;
                application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                application.CreatedByUserID = Global.CurrentUser.UserID;
                if (!application.Save())
                {
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _TestAppointment.RetakeTestApplicationID = null;
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = application.ApplicationID;
            }
            return true;
        }


        private void _LoadDataToObject()
        {
            _TestAppointment.TestTypeID = _TestType;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToDecimal(lblFees.Text);
            _TestAppointment.CreatedByUserID = Global.CurrentUser.UserID;
            _TestAppointment.IsLocked = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _LoadDataToObject();
            if (_TestAppointment.Save())
            {
                _Mode = _enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

    }
}
