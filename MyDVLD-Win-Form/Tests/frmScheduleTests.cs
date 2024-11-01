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
using static MyDVLD_Business.clsTestTypes;

namespace MyDVLD_Win_Form
{
    public partial class frmScheduleTests : Form
    {
        private clsTestTypes.enTestType _TestType;
        private int _LocalDrivingLicenseApplicationID;
        private int _TestAppointmentID = -1;
        public frmScheduleTests(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID, int TestAppointmentID = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = TestAppointmentID;
            _TestType = TestTypeID;
        }

        private void test_Load(object sender, EventArgs e)
        {
            ctrlSecheduleTest1.TestType = _TestType;
            ctrlSecheduleTest1.LoadData(_LocalDrivingLicenseApplicationID, _TestAppointmentID);
        }
    }
}
