using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1.DB
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            string database = "online_shop";
            string username = "root";
            string password = "I;!v(||OI0Kp5d%l0|lU71ll|j32SY1O"; 
            string connString = "server=" + host + ";user id=" + username
                 + ";password=\"" + password + "\";database=" + database + ";persistsecurityinfo=True";
            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}
