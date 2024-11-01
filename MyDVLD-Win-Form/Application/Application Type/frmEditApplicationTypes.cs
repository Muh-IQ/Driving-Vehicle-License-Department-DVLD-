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
    public partial class frmEditApplicationTypes : Form
    {
        private int _ApplicationTypeID = 0;
        private clsApplicationTypes _applicationTypes;

        public frmEditApplicationTypes(int ID)
        {
            InitializeComponent();
            _ApplicationTypeID = ID;
        }
        public bool IsSave=false;

        private void _FillDataToObject()
        {
            _applicationTypes.ApplicationFees = Convert.ToDecimal(txtFees.Text);
            _applicationTypes.ApplicationTypeTitle = txtTitle.Text;
        }
        private void _FillControl()
        {
             _applicationTypes = clsApplicationTypes.Find(_ApplicationTypeID);
            if (_applicationTypes != null) 
            {
                lblID.Text= _ApplicationTypeID.ToString();
                txtFees.Text= _applicationTypes.ApplicationFees.ToString();
                txtTitle.Text = _applicationTypes.ApplicationTypeTitle;
            }
        }

        private void frmEditApplicationTypes_Load(object sender, EventArgs e)
        {
            _FillControl();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FillDataToObject();
            if(_applicationTypes.Save())
            {
                IsSave = true;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }
    }
}
