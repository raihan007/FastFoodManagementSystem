using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodManagementSystem
{
    public partial class EmployeeForm : Form
    {
        private DataContext _context;
        private Employee _employee;
        private int emp_Id = 0;
        private double ProductPrice;
        private double cost;
        private double totalCost;
        private int discount;
        private int vat;
        private int customerPaid;
        private double returnPaid;
        private int ProductId;
        private int quantity;
        private List<Order> OrderList; 
        public EmployeeForm(Employee _employee)
        {
            this._employee = _employee;
            InitializeComponent();

            
            label1.Text = "Welcome Mr./Mrs. " + _employee.Name;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)//Products Info
            {
                ShowAllProductsData();
                ClearProductInput();
            }

            if (tabControl1.SelectedIndex == 2)//Own Info
            {
                showOwnInfo(_employee);
                ClearProductInput();
            }

            if (tabControl1.SelectedIndex == 0)//Take Order
            {
                GetProductNameList();
                ClearProductInput();
            }
        }

        private void GetProductNameList()
        {
            _context = new DataContext();
            bindingSource1.DataSource = _context.GetProductNameList();
            cmbProductName.DataSource = bindingSource1.DataSource;
            cmbProductName.DisplayMember = "P_Name";
            cmbProductName.ValueMember = "P_Id";
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

        private void ClearProductInput()
        {
            txtSearch.Text = "";
        }

        private void ShowAllProductsData()
        {
            _context = new DataContext();
            productsGrid.DataSource = _context.GetProductList();
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

        private void linkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartForm start = new StartForm();
            start.Show();
            this.Dispose();
        }

        private void linkChangePass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePasswordForm changePassword = new ChangePasswordForm(_employee);
            changePassword.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("You Can Search With Product ID/Price.");
            }
            else
            {
                _context = new DataContext();
                productsGrid.DataSource = _context.GetSearchProduct(txtSearch.Text);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearProductInput();
            ShowAllProductsData();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            GetProductNameList();
            ClearProductInput();
        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbProductName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            ProductId = Convert.ToInt32(cmbProductName.SelectedValue);
            if (ProductId != 0)
            {
                _context = new DataContext();
                ProductPrice = _context.GetProductPrice(ProductId);
                txtProductPrice.Text = ProductPrice.ToString();
            }
            else
            {
                MessageBox.Show("Please Select Valid Product!");
                txtProductPrice.Text = "";
            }
            
        }

        private void EmployeeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(txtQuantity.Text, out quantity))
            {
                cost = (ProductPrice*quantity);
                txtCost.Text = cost.ToString();
                btnAddItem.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please Enter Valid Quantity!");
                txtCost.Text = "";
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            OrderGrid.Rows.Add(this.ProductId, cmbProductName.Text, this.ProductPrice, this.quantity, this.cost);
            this.totalCost += cost;
            lblTotalCost.Text = totalCost.ToString();
            txtGrandTotal.Text = totalCost.ToString();
            clearEntry();
        }

        private void clearEntry()
        {
            cmbProductName.SelectedIndex = 0;
            txtProductPrice.Text = "0";
            txtQuantity.Text = "0";
            txtCost.Text = "0";
            txtDiscount.Text = "0";
            txtVat.Text = "0";
            txtCustomerPaid.Text = "0";
            txtReturn.Text = "0";
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            this.totalCost = Convert.ToInt32(lblTotalCost.Text);
            if (Int32.TryParse(txtDiscount.Text, out discount))
            {
                double extra = totalCost * (discount / 100.0);
                totalCost = totalCost - extra;
                txtGrandTotal.Text = totalCost.ToString();
            }
            else
            {
                MessageBox.Show("Please Enter Valid Discount!");
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            this.totalCost = Convert.ToInt32(lblTotalCost.Text);
            if (Int32.TryParse(txtVat.Text, out vat))
            {
                double extra = totalCost * (vat / 100.0);
                totalCost = totalCost + extra;
                txtGrandTotal.Text = totalCost.ToString();
            }
            else
            {
                MessageBox.Show("Please Enter Valid Vat!");
            }
        }

        private void btnTakeOrder_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(txtCustomerPaid.Text, out customerPaid))
            {
                label25.Visible = true;
                txtReturn.Visible = true;
                returnPaid = customerPaid - totalCost;
                txtReturn.Text = returnPaid.ToString();
                TakeOrder();
            }
            else
            {
                MessageBox.Show("Please Enter Valid Customer Amount!");
            }
        }

        private void TakeOrder()
        {
            List<SellDetails> sellDetailses = new List<SellDetails>();
            Sell sell = new Sell()
            {
                Cost = Convert.ToInt32(lblTotalCost.Text),
                Discount = Convert.ToInt32(txtDiscount.Text),
                Vat = Convert.ToInt32(txtVat.Text),
                TotalCost = Convert.ToInt32(txtGrandTotal.Text),
                CreatedBy = _employee.Id
            };

            _context = new DataContext();
            _context.AddSell(sell);

            int sell_id = _context.GetSellId();

            foreach (DataGridViewRow row in OrderGrid.Rows)
            {
                SellDetails item = new SellDetails()
                {
                    Product_Id = Convert.ToInt32(row.Cells[0].Value.ToString()),
                    Quantity = Convert.ToInt32(row.Cells[3].Value.ToString()),
                    ProductCost = Convert.ToInt32(row.Cells[4].Value.ToString()),
                    Sell_Id = sell_id
                };

                sellDetailses.Add(item);
            }

            if (_context.AddOrder(sellDetailses))
            {
                MessageBox.Show("Order Taken!");
            }
            else
            {
                MessageBox.Show("Order taken Failed!");
            }
        }


        private void btnOderReset_Click(object sender, EventArgs e)
        {
            cmbProductName.SelectedIndex = 0;
            txtProductPrice.Text = "0";
            txtQuantity.Text = "0";
            txtGrandTotal.Text = "0";
            txtCost.Text = "0";
            txtDiscount.Text = "0";
            txtVat.Text = "0";
            txtCustomerPaid.Text = "0";
            txtReturn.Text = "0";
            lblTotalCost.Text = "0000.000";
            this.OrderGrid.Rows.Clear();
        }


    }
}
