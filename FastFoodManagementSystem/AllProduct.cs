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
    public partial class AllProduct : Form
    {
        private DataContext _context;
        public AllProduct()
        {
            InitializeComponent();
        }

        private void AllProduct_Load(object sender, EventArgs e)
        {
            _context = new DataContext();
            dataGridView1.DataSource = _context.GetProductList();
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[0].HeaderText = "Product ID";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Product Price";
            dataGridView1.Columns[3].Visible = false;
            
        }
    }
}
