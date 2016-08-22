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
    public partial class SellDetailsForm : Form
    {
        private int Sell_Id;
        private DataContext _context;
        private Sell _sell;
        
        public SellDetailsForm(int Sell_Id)
        {
            this.Sell_Id = Sell_Id;
            InitializeComponent();
        }

        private void SellDetailsForm_Load(object sender, EventArgs e)
        {
            _context = new DataContext();
            _sell = _context.SellDetailsById(this.Sell_Id);
            ShowSellData(_sell);
            ShowSellDetailsData();
        }

        private void ShowSellData(Sell sell)
        {
            txtSellId.Text = sell.S_Id.ToString();
            txtSellDate.Text = sell.S_Date.ToShortDateString();
            txtTotalCost.Text = sell.TotalCost.ToString();
            txtBilledBy.Text = sell.EmpName.ToString();
            txtDiscount.Text = sell.Discount.ToString();
            txtVat.Text = sell.Vat.ToString();
        }

        private void ShowSellDetailsData()
        {
            _context = new DataContext();
            SellDetailsGrid.DataSource = _context.GetSellDetailsList(this.Sell_Id);
            SellDetailsGrid.Columns[1].HeaderText = "Product ID";
            SellDetailsGrid.Columns[1].Width = 150;
            SellDetailsGrid.Columns[2].HeaderText = "Product Name";
            SellDetailsGrid.Columns[2].Width = 300;
            SellDetailsGrid.Columns[3].HeaderText = "Quantity";
            SellDetailsGrid.Columns[4].HeaderText = "Cost";
            SellDetailsGrid.Columns[4].Width = 110;
            SellDetailsGrid.Columns[0].Visible = false;
            SellDetailsGrid.Columns[5].Visible = false;
            
        }
    }
}
