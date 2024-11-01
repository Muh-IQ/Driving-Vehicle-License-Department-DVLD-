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
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; } 
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Enabled = _ShowAddPerson;
            }
        }

        private bool _EnableFilter = true;
        public bool EnableFilter
        {
            get { return _EnableFilter; }
            set
            {
                _EnableFilter = value;
                gbFilter.Enabled = _EnableFilter;
            }
        }
        public string TextFilter 
        {
            set
            {
                txtFilter.Text = value;
            }
        }
        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
           
        }

        public event Action<int> OnPersonID;
        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnPersonID;
            if (handler != null) 
            {
                handler(personID);
            }
        }

        private void _FindNow()
        {
            switch (cbFilter.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonData(Convert.ToInt32(txtFilter.Text));
                    break;
                case "National No":
                    ctrlPersonCard1.LoadPersonData(txtFilter.Text);
                    break;
            }
            if (OnPersonID != null) 
            {
                
                this.PersonSelected(ctrlPersonCard1.PersonID);
            }
        }

        public void LoadPersonInfo(int personID)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Text = personID.ToString();   
            _FindNow();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FindNow();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) //Enter = 13
            {
                btnSearch.PerformClick();
            }
            if (cbFilter.SelectedItem is "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            // e.Handled  تمنع كتابة اذا كان الشرط صحيح   
            // IsControl  تمنع تنفيذ التحكمات مثل الحذف و اي تحكم اخر
        }

        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            ErrorProvider errorProvider = new ErrorProvider();
            if (string.IsNullOrEmpty(txtFilter.Text)) 
            {
                if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtFilter, "This field is required!");
                }
                else
                {
                    //e.Cancel = false;
                    errorProvider.SetError(txtFilter, null);
                }
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();
            frm.DataBack += LoadPersonInfo;
            frm.ShowDialog();
        }

        public void FilterFocus()
        {
            txtFilter.Focus();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text=string.Empty;
            txtFilter.Focus();
        }


    }
}
