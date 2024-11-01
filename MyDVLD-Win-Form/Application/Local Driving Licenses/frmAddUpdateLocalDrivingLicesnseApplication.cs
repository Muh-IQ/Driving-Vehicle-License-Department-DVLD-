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
using static MyDVLD_Business.clsApplication;

namespace MyDVLD_Win_Form
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        private enum _enMode { Update, AddNew }
        private _enMode _Mode;
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _PersonID = -1;
        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _Mode = _enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _Mode = _enMode.Update;
        }

        private void _ResetDefaultData()
        {
            cbLicenseClass.DisplayMember= "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
            cbLicenseClass.DataSource = clsLicenseClass.GetAllLicenseClasses();
            if ( _Mode == _enMode.AddNew )
            {
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                ctrlPersonCardWithFilter1.TextFilter = string.Empty;
                ctrlPersonCardWithFilter1.FilterFocus();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                cbLicenseClass.SelectedIndex = 2;
                lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblCreatedByUser.Text = Global.CurrentUser.UserName;
                btnSave.Enabled = false;
                tpApplicationInfo.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
            }
        }
        private void _LoadData()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null) 
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            ctrlPersonCardWithFilter1.EnableFilter = false;
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToShortDateString();
            lblCreatedByUser.Text= _LocalDrivingLicenseApplication.UserInfo.UserName;
            lblFees.Text= _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblLocalDrivingLicebseApplicationID.Text= _LocalDrivingLicenseApplication.L_D_L_ApplacitionID.ToString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(_LocalDrivingLicenseApplication.LicenseClassInfo.ClassName);///يبحث في كومبو بوكس على اندكس اسم لكلاس و يرجع الاندكس و اذا لم يجده يرجع -1
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultData();
            if (_Mode == _enMode.Update) 
            {
                _LoadData();
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonID(int obj)
        {
            _PersonID = obj;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == _enMode.Update) 
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcApplicationInfo.SelectedTab = tpApplicationInfo;
                return;
            }

            if(ctrlPersonCardWithFilter1.PersonID != -1) 
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcApplicationInfo.SelectedTab = tpApplicationInfo;
                return;
            }
            MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ctrlPersonCardWithFilter1.FilterFocus();
        }
        private void _LoadDataToObj()
        {
            _LocalDrivingLicenseApplication.ApplicantPersonID= _PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication._enApplicationStatus.New;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToDecimal(lblFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = Global.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = (int)cbLicenseClass.SelectedValue;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            _LoadDataToObj();
            int ActiveApplicationID;
            // هل يوجد طلب جديد لنفس نوع رخصة القيادةاذا لم يرجع 1- معناه يوجد طلب جديد
            if ((ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_PersonID,clsApplication.enApplicationType.NewDrivingLicense, (int)cbLicenseClass.SelectedValue,clsApplication._enApplicationStatus.New) )!= -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            // هل يوجد طلب مكتمل لنفس نوع رخصة القيادة اذا مكتمل يعني تم اصدار رخصة له لنفس نوع رخصة القيادة
            if (clsApplication.IsCompleteApplicationExistForLicenseClass(_PersonID, clsApplication.enApplicationType.NewDrivingLicense, (int)cbLicenseClass.SelectedValue))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if(_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.L_D_L_ApplacitionID.ToString();
                //change form mode to update.
                _Mode = _enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
