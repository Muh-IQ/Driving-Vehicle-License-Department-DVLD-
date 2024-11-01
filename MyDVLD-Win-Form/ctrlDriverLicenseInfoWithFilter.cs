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
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
        public event Action<bool,int> OnIsActiveAndIDLicense;
        public event Action<int> OnLicenseID;
        protected virtual void IsActiveLicense(bool IsActiveLicense, int L_LicenseID)
        {
            Action<bool, int> handler = OnIsActiveAndIDLicense;
            if (handler != null)
            {
                handler(IsActiveLicense,  L_LicenseID);
            }

        }

        
        protected virtual void LicenseID(int L_LicenseID)
        {
            Action<int> handler = OnLicenseID;
            if (handler != null)
            {
                handler(L_LicenseID);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbLicenseID.Text)) 
            {
                MessageBox.Show("Please, Enter License ID","Error in Enter",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(int.TryParse(tbLicenseID.Text,out int L_D_LicenseID))
            {
                ctrlDriverLicenseInfo1.LoadDataByLicenseID(L_D_LicenseID);


                if (OnIsActiveAndIDLicense != null)
                {
                    IsActiveLicense(true,7);
                }
                if (OnLicenseID != null)
                {
                    LicenseID(7);
                }


            }
            else
            {
                MessageBox.Show("Please, Enter Correct the License ID\n Enter Only Number ", "Error in Enter", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
