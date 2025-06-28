namespace Entity
{
    public class OrderItems
    {
        private int OrderDetailId;
        private int OrderId;
        private int ProductId;
        private int Quantity;
        public OrderItems() { }

        public OrderItems(int orderDetailId, int orderId, int productId, int quantity)
        {
            OrderDetailId = orderDetailId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
        public int GetOrderDetailId { get { return OrderDetailId; }set { OrderDetailId = value; } }
        public int GetOrderId { get { return OrderId; }set { OrderId = value; } }
        public int GetProductId { get { return ProductId; }set { ProductId = value; } }
        public int GetQuantity { get { return Quantity; } set { Quantity = value; } }
    }
}
