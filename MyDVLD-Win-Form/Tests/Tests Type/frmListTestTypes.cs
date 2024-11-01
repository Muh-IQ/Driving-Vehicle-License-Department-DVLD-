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
    public partial class frmListTestTypes : Form
    {
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            dgvTestTypesList.DataSource= clsTestTypes.GetAllTestTypes();
            lblRecord.Text = dgvTestTypesList.RowCount.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType updateTestType = new frmUpdateTestType((int)dgvTestTypesList.CurrentRow.Cells[0].Value);
            this.Hide();
            updateTestType.ShowDialog();
            this.Show();
        }
    }
}
