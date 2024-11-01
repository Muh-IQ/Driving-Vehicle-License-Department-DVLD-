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
    public partial class frmApplicationTypes : Form
    {
        public frmApplicationTypes()
        {
            InitializeComponent();
        }
        private void _RefreshAllData()
        {   
            dgvApplicationTypesList.DataSource = clsApplicationTypes.GetAllApplicationTypes();
            lblRecord.Text = dgvApplicationTypesList.RowCount.ToString();
        }
        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshAllData();
        }

        private void editApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationTypes form = new frmEditApplicationTypes((int)dgvApplicationTypesList.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            if (form.IsSave)
            {
                _RefreshAllData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblRecord_Click(object sender, EventArgs e)
        {

        }
    }
}
