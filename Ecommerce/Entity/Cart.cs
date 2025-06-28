namespace Entity
{
    public class Cart
    {
        private int CartId;
        private int CustomerId;
        private int ProductId;
        private int Quantity;

        public Cart() { }

        public Cart(int cartId, int customerId, int productId, int quantity)
        {
            CartId = cartId;
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
        public int GetCartId { get { return CartId; } set { CartId = value; }}
        public int GetCustomerId { get { return CustomerId; } set { CustomerId = value; }}
        public int GetProductId { get { return ProductId; } set { ProductId = value; }}
        public int GetQuantity { get { return Quantity; } set { Quantity = value; } }
    }
}
