namespace Entity
{
    public class Customers
    {
        private int CustomerId;
        private string CustomerName;
        private string Email;
        private string Password;

        public Customers() { }

        public Customers(int customerId, string customerName, string email, string password)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Email = email;
            Password = password;
        }
        public int GetCustomerId { get { return CustomerId; }set { CustomerId = value; }}
        public string GetCustomerName { get { return CustomerName; } set { CustomerName = value; } }
        public string GetEmail { get { return Email; } set { Email = value; } }
        public string GetPassword { get { return Password; } set { Password = value; } }
    }
}
