namespace Entity
{
    public class Products
    {
        private int ProductId;
        private string ProductName;
        private decimal Price;
        private string Description;
        private int StockQuantity;

        public Products() { }

        public Products(int productId, string productName, decimal price, string description, int stockQuantity)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Description = description;
            StockQuantity = stockQuantity;
        }
        public int GetProductId { get { return ProductId; } set { ProductId = value; }}
        public string GetProductName { get { return ProductName; } set { ProductName = value; } }
        public decimal GetPrice { get { return Price; } set { Price = value; } }
        public string GetDescription { get { return Description; } set { Description = value; } }
        public int GetStockQuantity { get { return StockQuantity; } set { StockQuantity = value; } }
    }
}
