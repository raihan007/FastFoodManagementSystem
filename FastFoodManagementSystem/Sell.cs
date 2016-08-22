using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagementSystem
{
    public class Sell
    {
        public int S_Id { get; set; }
        public DateTime S_Date { get; set; }
        public double Cost { get; set; }
        public int Discount { get; set; }
        public int Vat { get; set; }
        public double TotalCost { get; set; }
        public int CreatedBy { get; set; }
        public String EmpName { get; set; }
    }
}
