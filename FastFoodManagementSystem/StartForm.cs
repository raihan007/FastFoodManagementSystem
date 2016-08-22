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
    public partial class StartForm : Form
    {
        private Employee _employees;
        private DataContext _context;
        public StartForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _employees = new Employee();

            _context = new DataContext();

            _employees = _context.GetLogger(textBox1.Text, textBox2.Text);


            if (_employees != null)
            {
                if (_employees.Type == "Manager")
                {
                    this.Hide();
                    Admin _admin = new Admin(_employees);
                    _admin.Show();
                }
                else
                {
                    this.Hide();
                    EmployeeForm _employeeForm = new EmployeeForm(_employees);
                    _employeeForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Wrong Username or Password!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllProduct _allProduct = new AllProduct();
            _allProduct.ShowDialog();
        }

        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
