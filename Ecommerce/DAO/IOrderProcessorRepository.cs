using System;
using Entity;


namespace DAO
{
    public interface IOrderProcessorRepository
    {
        bool CreateProduct(Products product);
        bool CreateCustomer(Customers customer);
        bool DeleteProduct(int productId);
        bool DeleteCustomer(int customerId);
        bool AddToCart(Customers customer, Products product, int quantity);
        bool RemoveFromCart(Customers customer, Products product);
        List<Products> GetAllFromCart(Customers customer);
        bool PlaceOrder(Customers customer, List<KeyValuePair<Products, int>> productList, string shippingAddress);
        List<KeyValuePair<Products, int>> GetOrdersByCustomer(int customerId);
    }
}
