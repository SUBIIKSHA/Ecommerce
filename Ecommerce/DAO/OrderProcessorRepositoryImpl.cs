using System;
using Entity;
using Microsoft.Data.SqlClient;
using myexceptions;
namespace DAO
{
    public class OrderProcessorRepositoryImpl : IOrderProcessorRepository
    {
        public bool CreateProduct(Products product)
        {
            try
            {
                using (SqlConnection con = util.DBConnection.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO Products (ProductName, Price, Description, StockQuantity) VALUES (@name, @price, @desc, @stock)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", product.GetProductName);
                    cmd.Parameters.AddWithValue("@price", product.GetPrice);
                    cmd.Parameters.AddWithValue("@desc", product.GetDescription);
                    cmd.Parameters.AddWithValue("@stock", product.GetStockQuantity);

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch
            {
                return false;
            }
            
        }


        public bool CreateCustomer(Customers customer)
        {
             try
        {
            using (SqlConnection con = util.DBConnection.GetConnection())
            {
                con.Open();
                string query = "INSERT INTO Customers (CustomerName, Email, Password) VALUES (@name, @mail, @pass)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", customer.GetCustomerName);
                cmd.Parameters.AddWithValue("@mail", customer.GetEmail);
                cmd.Parameters.AddWithValue("@pass", customer.GetPassword);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
        catch
        {
            return false;
        }
        }

        public bool DeleteProduct(int productId)
        {
        try
        {
            using (SqlConnection con =util.DBConnection.GetConnection())
            {
                con.Open();
                string query = "DELETE FROM Products WHERE ProductId = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", productId);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
        catch
        {
            return false;
        }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                using (SqlConnection con = util.DBConnection.GetConnection())
                {
                    con.Open();
                    string checkquery = "SELECT COUNT(*) FROM Customers WHERE CustomerId = @id";
                    SqlCommand checkCmd = new SqlCommand(checkquery, con);
                    checkCmd.Parameters.AddWithValue("@id", customerId);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                        throw new CustomerNotFoundException($"Customer with ID {customerId} not found.");

                    string delquery = "DELETE FROM Customers WHERE CustomerId = @id";
                    SqlCommand delcmd = new SqlCommand(delquery, con);
                    delcmd.Parameters.AddWithValue("@id", customerId);
                    int rows = delcmd.ExecuteNonQuery();

                    return rows > 0;
                }
            }
            catch (CustomerNotFoundException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddToCart(Customers customer, Products product, int quantity)
        {
            try
            {
                using (SqlConnection con = util.DBConnection.GetConnection())
                {
                    con.Open();
                    SqlCommand checkProduct = new SqlCommand("SELECT COUNT(*) FROM Products WHERE ProductId = @pid", con);
                    checkProduct.Parameters.AddWithValue("@pid", product.GetProductId);
                    int exists = (int)checkProduct.ExecuteScalar();

                    if (exists == 0)
                        throw new ProductNotFoundException($"Product with ID {product.GetProductId} not found.");

                    string query = "INSERT INTO Cart (CustomerId, ProductId, Quantity) VALUES (@cid, @pid, @qty)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cid", customer.GetCustomerId);
                    cmd.Parameters.AddWithValue("@pid", product.GetProductId);
                    cmd.Parameters.AddWithValue("@qty", quantity);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (ProductNotFoundException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool RemoveFromCart(Customers customer, Products product)
        { 
        try
        {
            using (SqlConnection con = util.DBConnection.GetConnection())
            {
                con.Open();
                string query = "DELETE FROM Cart WHERE CustomerId = @cid AND ProductId = @pid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cid", customer.GetCustomerId);
                cmd.Parameters.AddWithValue("@pid", product.GetProductId);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
        catch
        {
            return false;
        }
        }

        public List<Products> GetAllFromCart(Customers customer)
        {
            List<Products> cartItems = new List<Products>();
            try
            {
                using (SqlConnection con = util.DBConnection.GetConnection())
                {
                    con.Open();
                    string query = @"SELECT p.ProductId, p.ProductName, p.Price, p.Description, p.StockQuantity
                                    FROM Cart c
                                    JOIN Products p ON c.ProductId = p.ProductId
                                    WHERE c.CustomerId = @cid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cid", customer.GetCustomerId);
  
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Products p = new Products
                        {
                            GetProductId = (int)reader["ProductId"],
                            GetProductName = reader["ProductName"].ToString(),
                            GetPrice = (decimal)reader["Price"],
                            GetDescription = reader["Description"].ToString(),
                            GetStockQuantity = (int)reader["StockQuantity"]
                        };
                        cartItems.Add(p);
                    }
                    reader.Close();
                }
            }
            catch { }
            return cartItems;
        }


       public bool PlaceOrder(Customers customer, List<KeyValuePair<Products, int>> productList, string shippingAddress)
        {
            try
            {
                using (SqlConnection con = util.DBConnection.GetConnection())
                {
                    con.Open();
                    SqlTransaction tx = con.BeginTransaction();

                    try
                    {
                        decimal total = 0;
                        foreach (var item in productList)
                        {
                            total += item.Key.GetPrice * item.Value;
                        }

                        SqlCommand cmdOrder = new SqlCommand(
                            "INSERT INTO Orders (CustomerId, OrderDate, TotalPrice, ShippingAddress) " +
                            "VALUES (@cid, @date, @total, @addr); SELECT SCOPE_IDENTITY();", con, tx);
                        cmdOrder.Parameters.AddWithValue("@cid", customer.GetCustomerId);
                        cmdOrder.Parameters.AddWithValue("@date", DateTime.Now);
                        cmdOrder.Parameters.AddWithValue("@total", total);
                        cmdOrder.Parameters.AddWithValue("@addr", shippingAddress);
                        int orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());

                        foreach (var item in productList)
                        {
                            SqlCommand cmdItem = new SqlCommand(
                                "INSERT INTO OrderItems (OrderId, ProductId, Quantity) VALUES (@oid, @pid, @qty)",
                                con, tx);
                            cmdItem.Parameters.AddWithValue("@oid", orderId);
                            cmdItem.Parameters.AddWithValue("@pid", item.Key.GetProductId);
                            cmdItem.Parameters.AddWithValue("@qty", item.Value);
                            cmdItem.ExecuteNonQuery();
                        }

                        SqlCommand clearCart = new SqlCommand("DELETE FROM Cart WHERE CustomerId = @cid", con, tx);
                        clearCart.Parameters.AddWithValue("@cid", customer.GetCustomerId);
                        clearCart.ExecuteNonQuery();

                        tx.Commit();
                        return true;
                    }
                    catch
                    {
                        tx.Rollback();
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public List<KeyValuePair<Products, int>> GetOrdersByCustomer(int customerId)
        {
            List<KeyValuePair<Products, int>> orderList = new List<KeyValuePair<Products, int>>();

            try
            {
                using (SqlConnection con = util.DBConnection.GetConnection())
                {
                    con.Open();
                    string query = @"SELECT p.ProductId, p.ProductName, p.Price, p.Description, p.StockQuantity, oi.Quantity
                                    FROM Orders o
                                    JOIN OrderItems oi ON o.OrderId = oi.OrderId
                                    JOIN Products p ON oi.ProductId = p.ProductId
                                    WHERE o.CustomerId = @cid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cid", customerId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        throw new OrderNotFoundException($"No orders found for Customer ID: {customerId}");
                    }
                    while (reader.Read())
                    {
                        Products p = new Products
                        {
                            GetProductId = (int)reader["ProductId"],
                            GetProductName = reader["ProductName"].ToString(),
                            GetPrice = (decimal)reader["Price"],
                            GetDescription = reader["Description"].ToString(),
                            GetStockQuantity = (int)reader["StockQuantity"]
                        };

                        int quantity = (int)reader["Quantity"];
                        orderList.Add(new KeyValuePair<Products, int>(p, quantity));
                    }
                    reader.Close();
                }
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return orderList;
        }

    }
}
