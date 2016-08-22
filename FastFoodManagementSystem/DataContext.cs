using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagementSystem
{
    public class DataContext
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private String _sql;

        public DataContext()
        {
            try
            {
                _connection = new SqlConnection(@"Data Source=RAIHAN-PC\SQLEXPRESS;Initial Catalog=DFF;Integrated Security=True");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }

        public Employee GetLogger(string username, string password)
        {
            using (_connection)
            {
                _connection.Open();
                
                _sql = "Select * from Employees where Email='" + username + "' or Username='" + username + "' and Password='" + password + "'";
                _command = new SqlCommand(_sql, _connection);
                _reader = _command.ExecuteReader();
                if (_reader.Read())
                {
                    Employee emp = new Employee()
                    {
                        Id = _reader.GetInt32(0),
                        Name = _reader.GetString(1),
                        Address = _reader.GetString(2),
                        Gender = _reader.GetString(3),
                        Email = _reader.GetString(4),
                        Phone = _reader.GetString(5),
                        Birthdate = _reader.GetDateTime(6),
                        NID = _reader.GetString(7),
                        Username = _reader.GetString(8),
                        Password = _reader.GetString(9),
                        Role = _reader.GetString(10),
                        Salary = _reader.GetInt32(11),
                        Type = _reader.GetString(12)
                    };
                    _connection.Close();
                    return emp;
                }
                else
                {
                    Employee emp = new Employee();
                    emp = null;
                    _connection.Close();
                    return emp;
                } 
            }
        }

        public List<Product> GetProductList()
        {
            List<Product> productList = new List<Product>();
            
            _connection.Open();

            _sql = "Select * from Products where P_Status='Available'";
            _command = new SqlCommand(_sql, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                Product pro = new Product()
                {
                    P_Id = _reader.GetInt32(0),
                    P_Name = _reader.GetString(1),
                    P_Price = _reader.GetDouble(2),
                    P_Status = _reader.GetString(3)
                };

                productList.Add(pro);
            }
            _connection.Close();
            return productList;
        }

        public List<Product> GetAllProductList()
        {
            List<Product> productList = new List<Product>();

            _connection.Open();

            _sql = "Select * from Products";
            _command = new SqlCommand(_sql, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                Product pro = new Product()
                {
                    P_Id = _reader.GetInt32(0),
                    P_Name = _reader.GetString(1),
                    P_Price = _reader.GetDouble(2),
                    P_Status = _reader.GetString(3)
                };

                productList.Add(pro);
            }
            _connection.Close();
            return productList;
        }

        public bool SaveProduct(Product product)
        {
            _connection.Open();
            try{
                _command = new SqlCommand("INSERT INTO [Products] (P_Name,P_Price,P_Status)" + "VALUES (@P_NAME,@P_PRICE,@P_STATUS)", _connection);

                _command.Parameters.AddWithValue("@P_NAME", product.P_Name);
                _command.Parameters.AddWithValue("@P_PRICE", product.P_Price);
                _command.Parameters.AddWithValue("@P_STATUS", product.P_Status);

                _command.ExecuteNonQuery();
                _connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                _connection.Close();
                return false;
            }
            
        }

        public bool DeleteProduct(int pId)
        {
            _connection.Open();
            try
            {
                _command = new SqlCommand("DELETE [Products] WHERE P_Id = @ID", _connection);

                _command.Parameters.AddWithValue("@ID", pId);

                _command.ExecuteNonQuery();
                _connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                _connection.Close();
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {
            _connection.Open();
            try
            {
                _command = new SqlCommand("UPDATE [Products] SET P_Name=@P_NAME,P_Price=@P_PRICE,P_Status=@P_STATUS WHERE P_Id=@P_ID", _connection);

                _command.Parameters.AddWithValue("@P_ID", product.P_Id);
                _command.Parameters.AddWithValue("@P_NAME", product.P_Name);
                _command.Parameters.AddWithValue("@P_PRICE", product.P_Price);
                _command.Parameters.AddWithValue("@P_STATUS", product.P_Status);

                _command.ExecuteNonQuery();
                _connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                _connection.Close();
                return false;
            }
        }

        public bool ChangePassword(int id, string newPassword)
        {
            _connection.Open();
            try
            {
                _command = new SqlCommand("UPDATE [Employees] SET Password=@PASSWORD WHERE ID=@E_ID", _connection);

                _command.Parameters.AddWithValue("@E_ID", id);
                _command.Parameters.AddWithValue("@PASSWORD", newPassword);

                _command.ExecuteNonQuery();
                _connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                _connection.Close();
                return false;
            }
        }

        public List<Employee> GetAllEmployeesList()
        {
            List<Employee> employeeList = new List<Employee>();

            _connection.Open();

            _sql = "Select * from Employees";
            _command = new SqlCommand(_sql, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                Employee employee = new Employee()
                {
                    Id = _reader.GetInt32(0),
                    Name = _reader.GetString(1),
                    Address = _reader.GetString(2),
                    Gender = _reader.GetString(3),
                    Email = _reader.GetString(4),
                    Phone = _reader.GetString(5),
                    Birthdate = _reader.GetDateTime(6),
                    NID = _reader.GetString(7),
                    Username = _reader.GetString(8),
                    Password = _reader.GetString(9),
                    Role = _reader.GetString(10),
                    Salary = _reader.GetInt32(11),
                    Type = _reader.GetString(12)
                };

                employeeList.Add(employee);
            }
            _connection.Close();
            return employeeList;
        }

        public Employee EmployeeInfoById(int empId)
        {
            _connection.Open();

            _command = new SqlCommand("SELECT * FROM [Employees] WHERE ID=@E_ID", _connection);

            _command.Parameters.AddWithValue("@E_ID", empId);
            _reader = _command.ExecuteReader();
            _reader.Read();
            Employee employee = new Employee()
            {
                Id = _reader.GetInt32(0),
                Name = _reader.GetString(1),
                Address = _reader.GetString(2),
                Gender = _reader.GetString(3),
                Email = _reader.GetString(4),
                Phone = _reader.GetString(5),
                Birthdate = _reader.GetDateTime(6),
                NID = _reader.GetString(7),
                Username = _reader.GetString(8),
                Password = _reader.GetString(9),
                Role = _reader.GetString(10),
                Salary = _reader.GetInt32(11),
                Type = _reader.GetString(12)
            };
            _connection.Close();
            return employee;
        }

        public bool DeleteEmployee(int empId)
        {
            _connection.Open();
            try
            {
                _command = new SqlCommand("DELETE [Employees] WHERE ID=@EMP_ID", _connection);

                _command.Parameters.AddWithValue("@EMP_ID", empId);

                _command.ExecuteNonQuery();
                _connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                _connection.Close();
                return false;
            }
        }

        public bool UpdateEmployee(Employee emp)
        {
            _connection.Open();
            try
            {
                _command = new SqlCommand("UPDATE [Employees] SET Name=@NAME,Address=@ADDRESS,Gender=@GENDER,Email=@EMAIL,Phone=@PHONE,Birthdate=@BIRTHDATE,NID=@E_NID,Username=@USERNAME,Password=@PASSWORD,Role=@ROLE,Salary=@SALARY,Type=@TYPE WHERE ID=@E_ID", _connection);

                _command.Parameters.AddWithValue("@E_ID", emp.Id);
                _command.Parameters.AddWithValue("@NAME", emp.Name);
                _command.Parameters.AddWithValue("@Address", emp.Address);
                _command.Parameters.AddWithValue("@Gender", emp.Gender);
                _command.Parameters.AddWithValue("@Email", emp.Email);
                _command.Parameters.AddWithValue("@Phone", emp.Phone);
                _command.Parameters.AddWithValue("@Birthdate", emp.Birthdate);
                _command.Parameters.AddWithValue("@E_NID", emp.NID);
                _command.Parameters.AddWithValue("@Username", emp.Username);
                _command.Parameters.AddWithValue("@Password", emp.Password);
                _command.Parameters.AddWithValue("@Role", emp.Role);
                _command.Parameters.AddWithValue("@Salary", emp.Salary);
                _command.Parameters.AddWithValue("@Type", emp.Type);

                _command.ExecuteNonQuery();
                _connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                _connection.Close();
                return false;
            }
        }

        public bool SaveEmployee(Employee emp)
        {
            _connection.Open();
            try
            {
                _command = new SqlCommand("INSERT INTO [Employees] (Name,Address,Gender,Email,Phone,Birthdate,NID,Username,Password,Role,Salary,Type)" + "VALUES (@NAME,@ADDRESS,@GENDER,@EMAIL,@PHONE,@BIRTHDATE,@E_NID,@USERNAME,@PASSWORD,@ROLE,@SALARY,@TYPE)", _connection);

                _command.Parameters.AddWithValue("@NAME", emp.Name);
                _command.Parameters.AddWithValue("@Address", emp.Address);
                _command.Parameters.AddWithValue("@Gender", emp.Gender);
                _command.Parameters.AddWithValue("@Email", emp.Email);
                _command.Parameters.AddWithValue("@Phone", emp.Phone);
                _command.Parameters.AddWithValue("@Birthdate", emp.Birthdate);
                _command.Parameters.AddWithValue("@E_NID", emp.NID);
                _command.Parameters.AddWithValue("@Username", emp.Username);
                _command.Parameters.AddWithValue("@Password", emp.Password);
                _command.Parameters.AddWithValue("@Role", emp.Role);
                _command.Parameters.AddWithValue("@Salary", emp.Salary);
                _command.Parameters.AddWithValue("@Type", emp.Type);

                _command.ExecuteNonQuery();
                _connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                _connection.Close();
                return false;
            }
        }

        public List<Sell> GetAllSellsList()
        {
            List<Sell> SellList = new List<Sell>();

            _connection.Open();

            _sql = "SELECT S.*,E.Name FROM Sells S INNER JOIN Employees E ON S.CreatedBy = E.ID";
            _command = new SqlCommand(_sql, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                Sell sell = new Sell()
                {
                    S_Id = _reader.GetInt32(0),
                    S_Date = _reader.GetDateTime(1),
                    Cost = _reader.GetDouble(2),
                    Discount = _reader.GetInt32(3),
                    Vat = _reader.GetInt32(4),
                    TotalCost = _reader.GetDouble(5),
                    CreatedBy = _reader.GetInt32(6),
                    EmpName = _reader.GetString(7)
                };

                SellList.Add(sell);
            }
            _connection.Close();
            return SellList;
        }

        public Sell SellDetailsById(int sellId)
        {
            _connection.Open();

            _command = new SqlCommand("SELECT S.*,E.Name FROM Sells S INNER JOIN Employees E ON S.CreatedBy = E.ID WHERE S.ID=@SELL_ID", _connection);

            _command.Parameters.AddWithValue("@SELL_ID", sellId);
            _reader = _command.ExecuteReader();
            _reader.Read();
            Sell sell = new Sell()
            {
                S_Id = _reader.GetInt32(0),
                S_Date = _reader.GetDateTime(1),
                Cost = _reader.GetDouble(2),
                Discount = _reader.GetInt32(3),
                Vat = _reader.GetInt32(4),
                TotalCost = _reader.GetDouble(5),
                CreatedBy = _reader.GetInt32(6),
                EmpName = _reader.GetString(7)
            };
            _connection.Close();
            return sell;
        }

        public List<SellDetails> GetSellDetailsList(int sellId)
        {
            List<SellDetails> SellDetailsList = new List<SellDetails>();

            _connection.Open();

            _sql = "SELECT SD.Product_ID,P.P_Name,SD.P_Quantity,SD.P_Cost FROM Sell_Details SD INNER JOIN Products P ON SD.Product_ID=P.P_Id WHERE SD.Sell_ID=@SELL_ID";
            _command = new SqlCommand(_sql, _connection);
            _command.Parameters.AddWithValue("@SELL_ID", sellId);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                SellDetails SellDetails = new SellDetails()
                {
                    Product_Id = _reader.GetInt32(0),
                    ProductsName = _reader.GetString(1),
                    Quantity = _reader.GetInt32(2),
                    ProductCost = _reader.GetDouble(3)
                };

                SellDetailsList.Add(SellDetails);
            }
            _connection.Close();
            return SellDetailsList;
        }

        public List<Product> GetSearchProduct(string p)
        {
            List<Product> productList = new List<Product>();

            _connection.Open();

            _sql = "Select * from Products where P_Status='Available' AND P_Id=@SEARCH OR P_Price=@SEARCH";
            _command = new SqlCommand(_sql, _connection);
            _command.Parameters.AddWithValue("@SEARCH", p);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                Product pro = new Product()
                {
                    P_Id = _reader.GetInt32(0),
                    P_Name = _reader.GetString(1),
                    P_Price = _reader.GetDouble(2),
                    P_Status = _reader.GetString(3)
                };

                productList.Add(pro);
            }
            _connection.Close();
            return productList;
        }

        public List<Product> GetProductNameList()
        {
            List<Product> productList = new List<Product>();

            Product prod = new Product()
            {
                P_Id = 0,
                P_Name = "Select Your Product",
                P_Price = 0
            };

            productList.Add(prod);

            _connection.Open();

            _sql = "Select * from Products where P_Status='Available'";
            _command = new SqlCommand(_sql, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                Product pro = new Product()
                {
                    P_Id = _reader.GetInt32(0),
                    P_Name = _reader.GetString(1),
                    P_Price = _reader.GetDouble(2),
                    P_Status = _reader.GetString(3)
                };

                productList.Add(pro);
            }
            _connection.Close();
            return productList;
        }

        public double GetProductPrice(int productId)
        {
            _connection.Open();

            _sql = "Select P_Price from Products where P_Status='Available' AND P_Id=@P_ID";
            _command = new SqlCommand(_sql, _connection);
            _command.Parameters.AddWithValue("@P_ID", productId);
            _reader = _command.ExecuteReader();
            _reader.Read();
            double price  = _reader.GetDouble(0);
            _connection.Close();
            return price;
        }

        public List<Sell> GetAllSellsListByDate(String fromDate,String toDate)
        {
            List<Sell> SellList = new List<Sell>();

            _connection.Open();

            _sql = "SELECT S.*,E.Name FROM Sells S INNER JOIN Employees E ON S.CreatedBy = E.ID WHERE S.SellDate >=@FDATE and S.SellDate <=@TODATE";
            _command = new SqlCommand(_sql, _connection);
            _command.Parameters.AddWithValue("@FDATE", fromDate);
            _command.Parameters.AddWithValue("@TODATE", toDate);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                Sell sell = new Sell()
                {
                    S_Id = _reader.GetInt32(0),
                    S_Date = _reader.GetDateTime(1),
                    Cost = _reader.GetDouble(2),
                    Discount = _reader.GetInt32(3),
                    Vat = _reader.GetInt32(4),
                    TotalCost = _reader.GetDouble(5),
                    CreatedBy = _reader.GetInt32(6),
                    EmpName = _reader.GetString(7)
                };

                SellList.Add(sell);
            }
            _connection.Close();
            return SellList;
        }

        public bool AddOrder(List<SellDetails> sellDetailses)
        {
            _connection.Open();
            try
            {
                foreach (SellDetails sellDetails in sellDetailses)
                {
                    _command = new SqlCommand("INSERT INTO [Sell_Details] (Product_ID,P_Quantity,P_Cost,Sell_ID)" + "VALUES (@PRODUCT_ID,@P_QUANTITY,@P_COST,@SELL_ID)", _connection);

                    _command.Parameters.AddWithValue("@PRODUCT_ID", sellDetails.Product_Id);
                    _command.Parameters.AddWithValue("@P_QUANTITY", sellDetails.Quantity);
                    _command.Parameters.AddWithValue("@P_COST", sellDetails.ProductCost);
                    _command.Parameters.AddWithValue("@SELL_ID", sellDetails.Sell_Id);

                    _command.ExecuteNonQuery();
                }
                _connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                _connection.Close();
                return false;
            }
        }

        public int GetSellId()
        {
            _connection.Open();

            _sql = "SELECT IDENT_CURRENT('Sells') AS 'SellID'";
            _command = new SqlCommand(_sql, _connection);
           
            _reader = _command.ExecuteReader();
            _reader.Read();
            decimal id = _reader.GetDecimal(0);
            _connection.Close();
            return Convert.ToInt32(id);
        }

        public void AddSell(Sell sell)
        {
            _connection.Open();
            try
            {
                _command = new SqlCommand("INSERT INTO [Sells] (Cost,Discount,Vat,TotalCost,CreatedBy)" + "VALUES (@COST,@DISCOUNT,@VAT,@TOTALCOST,@CREATEDBY)", _connection);

                _command.Parameters.AddWithValue("@COST", sell.Cost);
                _command.Parameters.AddWithValue("@DISCOUNT", sell.Discount);
                _command.Parameters.AddWithValue("@VAT", sell.Vat);
                _command.Parameters.AddWithValue("@TOTALCOST", sell.TotalCost);
                _command.Parameters.AddWithValue("@CREATEDBY", sell.CreatedBy);

                _command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception exception)
            {
                _connection.Close();
            }
        }
    }
}
