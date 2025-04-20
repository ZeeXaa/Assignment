using MySql.Data.MySqlClient;
using System.Configuration;

namespace OrderApp.Data
{
    public class DbHelper
    {
        public static MySqlConnection GetConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnStr"].ConnectionString;
            var conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }
    }
}
