using MyDVLD_Business;
using MyDVLD_Win_Form.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using static MyDVLD_Business.clsPerson;
//using static MyDVLD_Win_Form.ctrlSecheduleTest;
namespace MyDVLD_Win_Form
{
    public partial class ctrlSecheduledTest : UserControl
    {
        private clsTestTypes.enTestType _TestType;
        private int _TestAppointmentID = -1;
        private int _TestID = -1;
        public clsTestTypes.enTestType TestType
        {
            get { return _TestType; }

            set
            {
                _TestType = value;
                switch (value)
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
        public int TestAppointmentID
        {
            get
            { return _TestAppointmentID; }
        }
        public int TestID
        { get { return _TestID; } }
        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }
        public void LoadData(int TestAppointmentID )
        {
          


            _TestAppointmentID = TestAppointmentID;
            clsTestAppointment TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if( TestAppointment == null )
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }
            _TestID = TestAppointment.TestID;
            clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(TestAppointment.LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + TestAppointment.LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.L_D_L_ApplacitionID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestType).ToString();
            lblDate.Text = TestAppointment.AppointmentDate.ToShortDateString();
              
            lblFees.Text = TestAppointment.PaidFees.ToString();
            lblTestID.Text = (TestAppointment.TestID == -1) ? "Not Taken Yet" : TestAppointment.TestID.ToString();
                  return;
        }
    }
}
