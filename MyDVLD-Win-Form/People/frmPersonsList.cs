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
    public partial class frmPersonsList : Form
    {

        private static DataTable _dtAllPerson = clsPerson.GetAllPersons();
        private DataTable _dtPerson = _dtAllPerson.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gender", "DateOfBirth", "Nationalty",
                                                       "Phone", "Email");

        public frmPersonsList()
        {
            InitializeComponent();
        }

        private void btnCloseManage_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefershAllDate()
        {
            _dtAllPerson = clsPerson.GetAllPersons();
            _dtPerson = _dtAllPerson.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gender", "DateOfBirth", "Nationalty",
                                                       "Phone", "Email");
            dgvAllPersons.DataSource = _dtPerson;
            lblRecord.Text = dgvAllPersons.RowCount.ToString();
        }

        private void frmPersonsList_Load(object sender, EventArgs e)
        {
            dgvAllPersons.DataSource = _dtPerson;
            cbFilter.SelectedIndex = 0;
            lblRecord.Text=dgvAllPersons.RowCount.ToString();
            if (dgvAllPersons.RowCount > 0) 
                dgvAllPersons.Columns[0].HeaderText = "Person ID";
                dgvAllPersons.Columns[1].HeaderText = "National No.";
                dgvAllPersons.Columns[2].HeaderText = "First Name";
                dgvAllPersons.Columns[3].HeaderText = "Second Name";
                dgvAllPersons.Columns[4].HeaderText = "Third Name";
                dgvAllPersons.Columns[5].HeaderText = "Last Name";
                dgvAllPersons.Columns[6].HeaderText = "Gender";
                dgvAllPersons.Columns[7].HeaderText = "Date Of Birth";
                dgvAllPersons.Columns[8].HeaderText = "Nationalty";
                dgvAllPersons.Columns[9].HeaderText = "Phone";
                dgvAllPersons.Columns[10].HeaderText = "Email";

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "Nationality";
                    break;

                case "Gendor":
                    FilterColumn = "Gender";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (FilterColumn == "None" || txtFilter.Text.Trim() == "")
            {
                _dtPerson.DefaultView.RowFilter = "";
                lblRecord.Text = dgvAllPersons.RowCount.ToString();
                return;
            }
            if (FilterColumn == "PersonID")
            {
                _dtPerson.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, int.Parse(txtFilter.Text));
            }
            else
            {
                _dtPerson.DefaultView.RowFilter = string.Format("{0} like '{1}%'", FilterColumn, txtFilter.Text);

            }

            lblRecord.Text = dgvAllPersons.RowCount.ToString();

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilter.Text != "None");
            if (cbFilter.Text == "None")
            {
                txtFilter.Text = string.Empty;
                txtFilter.Focus();
            }
        }

        private void btnAddPerson_MouseHover(object sender, EventArgs e)
        {
            btnAddPerson.BackColor = Color.Blue;
        }

        private void btnAddPerson_MouseLeave(object sender, EventArgs e)
        {
            btnAddPerson.BackColor = Color.White;
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();
            frm.ShowDialog();

            if (frm.IsSaved)
                _RefershAllDate();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvAllPersons.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson((int)dgvAllPersons.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            if (frm.IsSaved)
                _RefershAllDate();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this person",
                "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.OK)
            {
                if (clsPerson.DeletePersonByID((int)dgvAllPersons.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("اتحذف يا معلم");
                    _RefershAllDate();
                }
                else
                {
                    MessageBox.Show("Delete was not successfully , Because he is tied to another process", "Not Allow Process",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();
            frm.ShowDialog();

            if (frm.IsSaved)
                _RefershAllDate();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID") 
                  e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            
        }
    }
}
