using Microsoft.Data.SqlClient;

namespace util
{
    public class DBConnection
    {
        public static SqlConnection GetConnection()
        {
            return PropertyUtil.GetConnection(); 
        }
    }
}
