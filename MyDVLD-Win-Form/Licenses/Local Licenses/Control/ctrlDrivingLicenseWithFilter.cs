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
    public partial class ctrlDrivingLicenseWithFilter : UserControl
    {
        public ctrlDrivingLicenseWithFilter()
        {
            InitializeComponent();
        }
        private bool _EnableFilter = true;
        public bool FilterEnable
        {
            set
            {
                _EnableFilter = value;
                gbFilter.Enabled = value;
            }
            get { return _EnableFilter; }
        }
        public event Action<int> OnSelectedLicenseID;

        private int _LicenseID;

        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get
            { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbLicenseID.Focus();
                return;

            }

            LoadData(int.Parse(tbLicenseID.Text));
        }
        public void LoadData(int LicenseID)
        {
            tbLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadData(LicenseID);
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;
            if (OnSelectedLicenseID != null) 
            {
                OnSelectedLicenseID?.Invoke(_LicenseID);

            }
        }
    

       
     
        private void tbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == (char)13) 
                btnSearch.PerformClick();
        }

        public void txtLicenseIDFocus()
        {
            tbLicenseID.Focus();
        }

        private void tbLicenseID_Validating(object sender, CancelEventArgs e)
        {
            ErrorProvider errorProvider = new ErrorProvider();
            if (string.IsNullOrEmpty(tbLicenseID.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(tbLicenseID, "This field is required!");
            }
            else
            {
                errorProvider.SetError(tbLicenseID, string.Empty);

            }
        }
    }
}
