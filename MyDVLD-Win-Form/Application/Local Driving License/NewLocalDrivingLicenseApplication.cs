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
    public partial class NewLocalDrivingLicenseApplication : Form
    {
        private clsApplication _Application;
        private clsLocalDrivingLicenseApplication _LocalDriving;
        private int _L_D_L_ApplacitionID;
        private int _PersonID;
        private DataTable _dataTable;
        private enum _enMode { Update, AddNew }
        private _enMode _Mode;

        public bool IsSave=false;
        public NewLocalDrivingLicenseApplication(int L_D_L_ApplacitionID)
        {
            InitializeComponent();
            
            if (L_D_L_ApplacitionID > 0)
            {
                _Mode = _enMode.Update;
                this._L_D_L_ApplacitionID = L_D_L_ApplacitionID;
            }
            else
            {
                _Mode = _enMode.AddNew;
            }
            
        }

        private void _ChangeHeader()
        {
            if (_Mode == _enMode.Update)
            {
                lblHeader.Text = "Update Local Driving License Application";
                lblD_L_ApplacitionID.Text = _LocalDriving.L_D_L_ApplacitionID.ToString();
            }
            else
            {
                lblHeader.Text = "New Local Driving License Application";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
        private void _FillComboBox()
        {
            _dataTable = clsLicenseClass.GetAllLicenseClasses();
            cbLicensesClass.DisplayMember = "ClassName";
            cbLicensesClass.ValueMember = "ClassFees";
            cbLicensesClass.DataSource = _dataTable;
        }
        private void _FillControlWithAddNew()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatesBy.Text=Global.CurrentUser.UserName;
            lblApplicationFee.Text = clsApplicationTypes.Find(1).ApplicationFees.ToString();
        }
        private void _FillControlWithUpdate()
        {
            //Select LicenseClass from cls into cbLicensesClass(combobox)
            _LocalDriving = clsLocalDrivingLicenseApplication.Find(_L_D_L_ApplacitionID);
            if (_LocalDriving != null) 
            {
                cbLicensesClass.SelectedIndex = _LocalDriving.LicenseClassID-1;
            }
            else
            {
                MessageBox.Show("Error In Load Data!");
                return;
            }

            _Application = clsApplication.FindApplication(_LocalDriving.ApplicationID);
            if (_Application != null)
            {
                lblD_L_ApplacitionID.Text = _Application.ApplicationID.ToString();
                lblApplicationDate.Text = _Application.ApplicationDate.ToString();
                lblCreatesBy.Text = _Application.CreatedByUserID.ToString();
                lblApplicationFee.Text = _Application.PaidFees.ToString();
                ctrlPersonCardWithFilter1.LoadPersonInfo(_Application.ApplicantPersonID);
            }
            else
            {
                MessageBox.Show("Error In Load Data!");
                return;
            }





        }
        private bool _SaveInApplication()
        {
            _Application.ApplicantPersonID = _PersonID;
            _Application.ApplicationStatus = 1;
            _Application.PaidFees = decimal.Parse(lblApplicationFee.Text);
            _Application.CreatedByUserID = Global.CurrentUser.UserID;
            return _Application.Save();
        }
        private bool _SaveInL_D_License()
        {
            _LocalDriving.ApplicationID = _Application.ApplicationID;
            _LocalDriving.LicenseClassID = cbLicensesClass.SelectedIndex + 1;
            return _LocalDriving.Save();
        }
        private bool _IsExistApplicationNewLocal()
        {
           return clsApplication.IsExistApplicationNewLocal(_PersonID, 1, cbLicensesClass.SelectedIndex + 1);
        }
        private void NewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            
            _FillComboBox();
            if (_Mode == _enMode.Update)
            {
                tbApplicationInfo.Enabled = true;
                btnNext.Enabled = true;
                _FillControlWithUpdate();
            }
            else
            {
                //Mode = AddNew
                _Application = new clsApplication();
                _LocalDriving = new clsLocalDrivingLicenseApplication();
                tbApplicationInfo.Enabled = false;
                btnNext.Enabled = false;
                _FillControlWithAddNew();
            }
            _ChangeHeader();
        }

        private void ctrlPersonCardWithFilter1_OnPersonID(int obj)
        {
            if (obj > 0)
            {
                tbApplicationInfo.Enabled = true;
                btnNext.Enabled = true;
                _PersonID = obj;
            }           
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_IsExistApplicationNewLocal())
            {
                MessageBox.Show("There is an application of the same type and the license is incomplete", "Not Allow Process", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_SaveInApplication())
            {
                if (_SaveInL_D_License()) 
                {
                    _Mode = _enMode.Update;
                    MessageBox.Show("Add Seccussfully");
                    _ChangeHeader();
                    IsSave=true;
                }
                else
                    MessageBox.Show("Error in Add!");

            }
            else
                MessageBox.Show("Error in Add!");
                
        }

        private void cbLicensesClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
