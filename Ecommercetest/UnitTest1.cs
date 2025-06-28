using NUnit.Framework;
using DAO;
using Entity;
using System.Collections.Generic;
using myexceptions;

namespace EcommerceTests
{
    [TestFixture]
    public class OrderProcessorTests
    {
        private OrderProcessorRepositoryImpl _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new OrderProcessorRepositoryImpl();
        }

        [Test]
        public void CreateProductWhenProductIsValid()
        {
            var product = new Products
            {
                GetProductName = "Electric Kettle",
                GetPrice = 1499,
                GetDescription = "1.5L stainless steel kettle",
                GetStockQuantity = 15
            };

            bool result = _repo.CreateProduct(product);
            Assert.IsTrue(result);
        }

        [Test]
        public void AddToCart_WhenValidInput()
        {
            var customer = new Customers { GetCustomerId = 102 }; 
            var product = new Products { GetProductId = 201 };   

            bool result = _repo.AddToCart(customer, product, 2);
            Assert.IsTrue(result);
        }

        [Test]
        public void PlaceOrder_WhenCartHasValidItems()
        {
            var customer = new Customers { GetCustomerId = 103 };
            var productList = new List<KeyValuePair<Products, int>>()
            {
                new KeyValuePair<Products, int>(new Products { GetProductId = 207, GetPrice = 500 }, 2)
            };

            bool result = _repo.PlaceOrder(customer, productList, "Trichy");
            Assert.IsTrue(result);
        }

        [Test]
        public void GetOrdersByCustomer_ShouldThrowException_WhenInvalidCustomer()
        {
            int invalidCustomerId = 9999;

            Assert.Throws<CustomerNotFoundException>(() =>
            {
                _repo.GetOrdersByCustomer(invalidCustomerId);
            });
        }
    }
}
