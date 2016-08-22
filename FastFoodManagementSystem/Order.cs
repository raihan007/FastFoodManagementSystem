using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagementSystem
{
    public class Order
    {
        public int Product_Id { get; set; }
        public String ProductsName { get; set; }
        public Double ProductCost { get; set; }
        public int Quantity { get; set; }
        public Double GrandCost { get; set; }
          
    }
}
