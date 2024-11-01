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
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();

            ctrlPersonDetails1.LoadPersonData(PersonID);
        }
        public frmPersonDetails(string NationalNo)
        {
            InitializeComponent();

            ctrlPersonDetails1.LoadPersonData(NationalNo);
        }
        private void btnCloseManage_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
