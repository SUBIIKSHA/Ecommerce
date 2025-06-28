using Microsoft.Data.SqlClient;

namespace util
{
    public class PropertyUtil
    {
        private static string constring = "Data Source=DESKTOP-AFJRIAR\\SQLEXPRESS;Initial Catalog=Ecommerce;Integrated Security=True;Trust Server Certificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(constring);
        }
    }
}
