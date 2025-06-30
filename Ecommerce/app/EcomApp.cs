using System;
using System.Collections.Generic;
using DAO;
using Entity;
using util;
using Microsoft.Data.SqlClient;

namespace app
{
    class EcomApp
    {
        static void Main(string[] args)
        {
            OrderProcessorRepositoryImpl repo = new OrderProcessorRepositoryImpl();

            while (true)
            {
                Console.WriteLine("\nE-Commerce App");
                Console.WriteLine("1. Register Customer");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("5. Add to Cart");
                Console.WriteLine("6. View Cart");
                Console.WriteLine("7. Place Order");
                Console.WriteLine("8. View Customer Order");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Customers customer = new Customers();
                        Console.Write("Enter Name: ");
                        customer.GetCustomerName = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        customer.GetEmail = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        customer.GetPassword = Console.ReadLine();
                        Console.WriteLine(repo.CreateCustomer(customer)? "Customer registered successfully!": "Failed to register customer.");
                        break;

                    case 2:
                        Products product = new Products();
                        Console.Write("Enter Product Name: ");
                        product.GetProductName = Console.ReadLine();
                        Console.Write("Enter Price: ");
                        product.GetPrice = Convert.ToDecimal(Console.ReadLine());
                        Console.Write("Enter Description: ");
                        product.GetDescription = Console.ReadLine();
                        Console.Write("Enter Stock Quantity: ");
                        product.GetStockQuantity = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(repo.CreateProduct(product)? "Product created successfully!": "Failed to create product.");
                        break;

                    case 3:
                        Console.Write("Enter Product ID to delete: ");
                        int productId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(repo.DeleteProduct(productId)? "Product deleted successfully!": "Product not found.");
                        break;
                    
                    case 4:
                        Console.Write("Enter Customer ID to delete: ");
                        int delCustId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(repo.DeleteCustomer(delCustId)? "Customer deleted successfully.": "Failed to delete customer.");
                        break;

                    case 5:
                        Console.Write("Enter Customer ID: ");
                        int custId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Product ID: ");
                        int prodId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Quantity: ");
                        int qty = Convert.ToInt32(Console.ReadLine());
                        bool added=repo.AddToCart(new Customers { GetCustomerId = custId }, new Products { GetProductId = prodId }, qty);
                        if(added)
                            Console.WriteLine("Product added to cart.");
                        else
                            Console.WriteLine("Failed to add.");
                        break;

                    case 6:
                        Console.Write("Enter Customer ID: ");
                        int cartCustId = Convert.ToInt32(Console.ReadLine());
                        List<Products> cartItems = repo.GetAllFromCart(new Customers(cartCustId, "", "", ""));
                        Console.WriteLine("Cart Items:");
                        foreach (var item in cartItems)
                        {
                            Console.WriteLine($"{item.GetProductId} - {item.GetProductName} - Rs.{item.GetPrice}");
                        }
                        break;

                    case 7:
                        Console.Write("Enter Customer ID: ");
                        int ordCustId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Shipping Address: ");
                        string address = Console.ReadLine();
                        var customers = new Customers(ordCustId, "", "", "");
                        var cart = repo.GetAllFromCart(customers);
                        List<KeyValuePair<Products, int>> orderList = new List<KeyValuePair<Products, int>>();
                        foreach (var item in cart)
                        {
                            Console.Write($"Enter quantity for {item.GetProductName}: ");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            orderList.Add(new KeyValuePair<Products, int>(item, quantity));
                        }
                        Console.WriteLine(repo.PlaceOrder(customers, orderList, address)? "Order placed successfully!": "Failed to place order.");
                        break;

                    case 8:
                        Console.Write("Enter Customer ID: ");
                        int viewCustId = Convert.ToInt32(Console.ReadLine());
                        var orders = repo.GetOrdersByCustomer(viewCustId);
                   
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"{order.Key.GetProductName} - Qty: {order.Value} - Rs.{order.Key.GetPrice}");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Thank you for using the E-Commerce App!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
