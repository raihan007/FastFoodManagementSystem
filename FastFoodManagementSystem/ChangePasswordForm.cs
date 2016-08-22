using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodManagementSystem
{
    public partial class ChangePasswordForm : Form
    {
        private DataContext _context;
        private Employee _employee;
        public ChangePasswordForm(Employee _employee)
        {
            this._employee = _employee;
            InitializeComponent();
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {

        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (txtConfirmPass.Text == "")
            {
                MessageBox.Show("Please Confirm Your Password.");
            }
            else if (txtConfirmPass.Text != _employee.Password)
            {
                MessageBox.Show("Password Did Not Matched.");
            }
            else
            {
                if (txtNewPass.Text != "")
                {
                    _context = new DataContext();
                    if (_context.ChangePassword(_employee.Id, txtNewPass.Text))
                    {
                        MessageBox.Show("Your Password Successfully Changed.");
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("We Failed To Change Your Password.");
                    }
                }
                else
                {
                    MessageBox.Show("Please Set Your New Password.");
                }
                
            }

        }
    }
}
