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
    public partial class frmLicenseHistory : Form
    {
        private int _PersonID = -1 ;
        public frmLicenseHistory()
        {
            InitializeComponent();

        }
        public frmLicenseHistory(int personID)
        {
            InitializeComponent();
            this._PersonID = personID;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if (this._PersonID != -1) 
            {
                ctrlPersonCardWithFilter1.EnableFilter = false;
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlDrivingLicenseHistory1.LoadDataByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter1.Enabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonID(int obj)
        {
            _PersonID = obj;
            if (obj > 1)
            {
                ctrlDrivingLicenseHistory1.LoadDataByPersonID(_PersonID);
            }
            else
            {
                ctrlDrivingLicenseHistory1.Clear();
            }
        }
    }
}
