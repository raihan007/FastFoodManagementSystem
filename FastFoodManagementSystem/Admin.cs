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
    public partial class Admin : Form
    {
        private DataContext _context;
        private Employee _employee;
        private int emp_Id = 0;
        public Admin(Employee _employee)
        {
            this._employee = _employee;
            InitializeComponent();
            cmbGender.SelectedIndex = 0;
            toDate.Value = DateTime.Now;
            FromDate.Value = DateTime.Now;
            label1.Text = "Welcome Mr./Mrs. " + _employee.Name;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)//Products Info
            {
                ShowAllProductsData();
                EmpInfoTabState();
                ClearProductInput();
            }
            
            if (tabControl1.SelectedIndex == 2)//Employees Info
            {
                ShowAllEmployeesData();
                EmpInfoTabState();
                ClearProductInput();
            }

            if (tabControl1.SelectedIndex == 3)//Sells Info
            {
                toDate.Enabled = false;
                ShowAllSellsData();
                EmpInfoTabState();
                ClearProductInput();
            }
        }

        private void ShowAllSellsData()
        {
            _context = new DataContext();
            SellsDataGrid.DataSource = _context.GetAllSellsList();
            if (SellsDataGrid.Rows.Count > 0)
            {
                SellsDataGrid.CurrentRow.Selected = false;
            }
            SellsDataGrid.Columns[0].HeaderText = "ID";
            SellsDataGrid.Columns[1].HeaderText = "Sell Date";
            SellsDataGrid.Columns[2].HeaderText = "Cost";
            SellsDataGrid.Columns[3].HeaderText = "Discount";
            SellsDataGrid.Columns[4].HeaderText = "Vat";
            SellsDataGrid.Columns[5].HeaderText = "Total Bill";
            SellsDataGrid.Columns[6].Visible = false;
            SellsDataGrid.Columns[7].HeaderText = "Order Taken BY";
        }

        public void ShowAllEmployeesData()
        {
            _context = new DataContext();
            EmployeesDataGrid.DataSource = _context.GetAllEmployeesList();
            EmployeesDataGrid.Columns[0].HeaderText = "ID";
            EmployeesDataGrid.Columns[1].HeaderText = "Name";
            EmployeesDataGrid.Columns[2].HeaderText = "Address";
            EmployeesDataGrid.Columns[3].HeaderText = "Gender";
            EmployeesDataGrid.Columns[4].HeaderText = "Email";
            EmployeesDataGrid.Columns[5].HeaderText = "Phone";
            EmployeesDataGrid.Columns[7].HeaderText = "Birthdate";
            EmployeesDataGrid.Columns[6].HeaderText = "National ID No";
            EmployeesDataGrid.Columns[8].HeaderText = "Username";
            EmployeesDataGrid.Columns[9].Visible = false;
            EmployeesDataGrid.Columns[10].HeaderText = "Job Role";
            EmployeesDataGrid.Columns[11].HeaderText = "Salary";
            EmployeesDataGrid.Columns[12].Visible = false;
            EmployeesDataGrid.Columns[0].Width = 50;
            EmployeesDataGrid.Columns[1].Width = 150;
            EmployeesDataGrid.Columns[2].Width = 200;
            EmployeesDataGrid.Columns[3].Width = 150;
            EmployeesDataGrid.Columns[7].Width = 250;
            EmployeesDataGrid.CurrentRow.Selected = false;
        }

        private void ShowAllProductsData()
        {
            _context = new DataContext();
            productsGrid.DataSource = _context.GetAllProductList();
            productsGrid.CurrentRow.Selected = false;
            productsGrid.Columns[0].HeaderText = "ID";
            productsGrid.Columns[1].HeaderText = "Product Name";
            productsGrid.Columns[2].HeaderText = "Product Price";
            productsGrid.Columns[3].HeaderText = "Product Status";
            productsGrid.Columns[0].Width = 72;
            productsGrid.Columns[1].Width = 200;
            productsGrid.Columns[2].Width = 150;
            productsGrid.Columns[3].Width = 150;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            showOwnInfo(_employee);
        }

        private void showOwnInfo(Employee _employee)
        {
            txtName.Text = _employee.Name;
            txtAddress.Text = _employee.Address;
            if (_employee.Gender == "Male")
            {
                cmbGender.SelectedIndex = 0;
            }
            else
            {
                cmbGender.SelectedIndex = 1;
            }
            txtEmail.Text = _employee.Email;
            txtPhone.Text = _employee.Phone;
            txtBirthdate.Value = _employee.Birthdate;
            txtNIDno.Text = _employee.NID;
            txtUsername.Text = _employee.Username;
            txtPassword.Text = _employee.Password;
            txtSalary.Text = _employee.Salary.ToString(); 
            lblRole.Text = _employee.Role;
        }

        private void linkChangePass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePasswordForm changePassword = new ChangePasswordForm(_employee);
            changePassword.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String sts = "Unabailable";
            if (chkAvailable.Checked)
            {
                sts = "Available";
            }

            Product product = new Product()
            {
                P_Name = txtProductName.Text,
                P_Price = Convert.ToInt32(txtProductPrice.Text),
                P_Status = sts
            };

            _context = new DataContext();
            if (_context.SaveProduct(product))
            {
                MessageBox.Show("Successfully Saved.");
                ClearProductInput();
                ShowAllProductsData();
            }
            else
            {
                MessageBox.Show("Failed to Save.");
            }
        }


        private void linkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartForm start = new StartForm();
            start.Show();
            this.Dispose();
        }

        private void productsGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in productsGrid.SelectedRows)
            {
                txtProductId.Text = row.Cells[0].Value.ToString();
                txtProductName.Text = row.Cells[1].Value.ToString();
                txtProductPrice.Text = row.Cells[2].Value.ToString();
                String check = row.Cells[3].Value.ToString();
                if (check == "Available")
                {
                    chkAvailable.Checked = true;
                }
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Are You Want To Delete This?","Confirm!",MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                int p_Id = Convert.ToInt32(txtProductId.Text);
                _context = new DataContext();
                if (_context.DeleteProduct(p_Id))
                {
                    MessageBox.Show("Successfully Deleted.");
                    ClearProductInput();
                    ShowAllProductsData();
                }
                else
                {
                    MessageBox.Show("Failed to Delete.");
                }
            }
        }

        private void ClearProductInput()
        {
            txtProductId.Text = "";
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtProductPrice.Text = "";
            chkAvailable.Checked = false;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Are You Want To Update This?", "Confirm!", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                String sts = "Unabailable";
                if (chkAvailable.Checked)
                {
                    sts = "Available";
                }

                Product product = new Product()
                {
                    P_Id = Convert.ToInt32(txtProductId.Text),
                    P_Name = txtProductName.Text,
                    P_Price = Convert.ToInt32(txtProductPrice.Text),
                    P_Status = sts
                };
                _context = new DataContext();
                if (_context.UpdateProduct(product))
                {
                    MessageBox.Show("Successfully Updated.");
                    ClearProductInput();
                    ShowAllProductsData();
                }
                else
                {
                    MessageBox.Show("Failed to Update.");
                }
            }
        }

        private void EmployeesDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnAddEmp.Enabled = false;
            btnUpdateEmp.Enabled = true;
            btnDeleteEmp.Enabled = true;
        }

        public void EmpInfoTabState()
        {
            btnAddEmp.Enabled = true;
            btnUpdateEmp.Enabled = false;
            btnDeleteEmp.Enabled = false;
        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            EmpInfoEntryForm empInfoEntry = new EmpInfoEntryForm(this,0);
            empInfoEntry.ShowDialog();
        }

        private void btnUpdateEmp_Click(object sender, EventArgs e)
        {
            emp_Id = Convert.ToInt32(EmployeesDataGrid.SelectedRows[0].Cells[0].Value.ToString());
            EmpInfoEntryForm empInfoEntry = new EmpInfoEntryForm(this,emp_Id);
            empInfoEntry.ShowDialog();
        }

        private void btnDeleteEmp_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Are You Want To Delete This?", "Confirm!", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                emp_Id = Convert.ToInt32(EmployeesDataGrid.SelectedRows[0].Cells[0].Value.ToString());
                _context = new DataContext();
                if (_context.DeleteEmployee(emp_Id))
                {
                    MessageBox.Show("Successfully Deleted.");
                    ShowAllEmployeesData();
                    EmpInfoTabState();
                }
                else
                {
                    MessageBox.Show("Failed to Delete.");
                }
            }
        }

        private void SellsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SellsDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnSellDetails.Enabled = true;
        }

        private void btnSellDetails_Click(object sender, EventArgs e)
        {
            int Sell_Id = Convert.ToInt32(SellsDataGrid.SelectedRows[0].Cells[0].Value.ToString());
            SellDetailsForm SellDetails = new SellDetailsForm(Sell_Id);
            SellDetails.ShowDialog();
        }

        private void btnSearchByDate_Click(object sender, EventArgs e)
        {
            DateTime sDate = DateTime.Now;
            if (singleSearch.Checked)
            {
                toDate.Value = FromDate.Value.AddDays(1);
            }
            _context = new DataContext();
            SellsDataGrid.DataSource = _context.GetAllSellsListByDate(FromDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"), toDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (SellsDataGrid.Rows.Count > 0)
            {
                SellsDataGrid.CurrentRow.Selected = false;
            }
            SellsDataGrid.Columns[0].HeaderText = "ID";
            SellsDataGrid.Columns[1].HeaderText = "Sell Date";
            SellsDataGrid.Columns[2].HeaderText = "Cost";
            SellsDataGrid.Columns[3].HeaderText = "Discount";
            SellsDataGrid.Columns[4].HeaderText = "Vat";
            SellsDataGrid.Columns[5].HeaderText = "Total Bill";
            SellsDataGrid.Columns[6].Visible = false;
            SellsDataGrid.Columns[7].HeaderText = "Order Taken BY";

        }

        private void singleSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (!singleSearch.Checked)
            {
                toDate.Enabled = true;
            }
            else
            {
                toDate.Enabled = false;
            }
        }
    }
}
