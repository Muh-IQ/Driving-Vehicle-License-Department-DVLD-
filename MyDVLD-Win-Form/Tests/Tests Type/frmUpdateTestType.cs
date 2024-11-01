using MyDVLD_Business;
using MyDVLD_Win_Form.GloablClasses;
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
    public partial class frmUpdateTestType : Form
    {
        private int _TestTypeID;
        private clsTestTypes _TestTypes;
        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void _LoadDataToObject()
        {
            _TestTypes.TestTypeTitle = tbTitle.Text;
            _TestTypes.TestTypeDescription = tbDescription.Text;
            _TestTypes.TestTypeFees=Convert.ToDecimal(tbFees.Text);
        }
        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            //_TestTypes = clsTestTypes.Find(_TestTypeID);
            if (_TestTypes != null) 
            {
                lblID.Text = _TestTypeID.ToString();
                tbTitle.Text = _TestTypes.TestTypeTitle;
                tbDescription.Text = _TestTypes.TestTypeDescription;
                tbFees.Text =_TestTypes.TestTypeFees.ToString();
            }
            else
            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

   

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _LoadDataToObject();
            if (_TestTypes.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void tbTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(tbTitle, null);
            };
        }

        private void tbDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbDescription, "Description cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(tbDescription, null);
            };
        }

        private void tbFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbFees, "Fees cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(tbFees, null);
            };

            if (!clsValidatoin.IsNumber(tbFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(tbFees, null);
            };
        }
    }
}
