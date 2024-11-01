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
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID;
        private clsTestTypes.enTestType _TestType;
        private clsTests _Tests;
        public frmTakeTest( int TestAppointmentID, clsTestTypes.enTestType TestType)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestType = TestType;
        }

        

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest1.TestType = _TestType;
            ctrlSecheduledTest1.LoadData(_TestAppointmentID);
            if (ctrlSecheduledTest1.TestAppointmentID < 1)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
            int TestID = ctrlSecheduledTest1.TestID;
            if (TestID > 0) 
            {
                _Tests = clsTests.Find(TestID);
                if (_Tests.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;

                tbNotes.Text = _Tests.Notes;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                lblUserMessage.Visible = true;
            }
            else
            {
                _Tests = new clsTests();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No
             )
            {
                return;
            }
            _Tests.Notes=tbNotes.Text;
            _Tests.TestAppointmentID = _TestAppointmentID;
            _Tests.CreatedByUserID= Global.CurrentUser.UserID;
            _Tests.TestResult = rbPass.Checked;
            if (_Tests.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
