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
    public partial class frm_International_Driving_Info : Form
    {
        public frm_International_Driving_Info(int InternationalLicenseID)
        {
            InitializeComponent();
            ctrlDriverInternationalLicenseInfo1.LoadData(InternationalLicenseID);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
