namespace Entity
{
    public class Orders
    {
        private int OrderId;
        private int CustomerId;
        private DateTime OrderDate;
        private decimal TotalPrice;
        private string ShippingAddress;

        public Orders() { }

        public Orders(int orderId, int customerId, DateTime orderDate, decimal totalPrice, string shippingAddress)
        {
            OrderId = orderId;
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            ShippingAddress = shippingAddress;
        }
        public int GetOrderId { get { return OrderId; }set { OrderId = value; }}
        public int GetCustomerId { get { return CustomerId; }set { CustomerId = value; } }
        public DateTime GetOrderDate { get { return OrderDate; } set { OrderDate = value; }}
        public decimal GetTotalPrice { get { return TotalPrice; } set { TotalPrice = value; } }
        public string GetShippingAddress { get { return ShippingAddress; } set { ShippingAddress = value; } }
    }
}