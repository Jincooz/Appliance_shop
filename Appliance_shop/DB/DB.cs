using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1.DB
{
    class DB
    {
        private static DB _instance;
        private MySqlConnection connection;
        public static DB Instance { get
            {
                if (_instance == null)
                {
                    _instance = new DB();
                }
                return _instance;
            }
        }
        private DB()
        {
            connection = DBUtils.GetDBConnection();
        }
        //public List<List<Object>> GetShopingList(string request)
        //{
        //    string sql = "SELECT * FROM shoping_list" 
        //        + " WHERE user_id = " + ActiveUser.Instance.ID;
        //    if (request != "") sql += " WHERE " + request + ";";
        //    return Select(sql);
        ////}
        //public List<List<Object>> GetAppliance(string request)
        //{
        //    string sql = "SELECT * FROM avaliable_devices";
        //    if (request != "") sql += " WHERE " + request + ";";
        //    return Select(sql);
        //}
        public (string, List<object>) GetEnumerableTrademark()
        {
            string sql = "SELECT name FROM TRADEMARK";
            var result = Select(sql)["name"];
            return ("Trademark", result);
        }
        public (string, List<object>) GetEnumerableRole()
        {
            string sql = "SELECT name FROM ROLE";
            var result = Select(sql)["name"];
            return ("Role", result);
        }
        public (string, List<object>) GetEnumerableCategory()
        {
            string sql = "SELECT name FROM APPLIANCES_CATEGORY";
            var result = Select(sql);
            return ("Category", result["name"]);
        }
        public Dictionary<string, List<Object>> Select(string statement)
        {
            Dictionary<string,List<Object>> result = new Dictionary<string, List<Object>>();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = statement;
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (result.Count == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                result.Add(reader.GetName(i),new List<object>());
                            }
                        }
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            result[reader.GetName(i)].Add(reader.GetValue(i));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result.Add(reader.GetName(i), new List<object>());
                    }
                }
            }
            connection.Close();
            return result;
        }
        public void Procedure(string statement)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = statement;
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Duplicate entry"))
                    throw new Exception(e.Message.Substring(e.Message.IndexOf("uc_") + 3));
            }
            finally
            {
                connection.Close();
            }
        }

        public int GetAmountOfRequests(string name)
        {
            string sql = "SELECT count(*) AS amount FROM " + name;
            var result = Select(sql);
            return Convert.ToInt32(result["amount"][0]);
        }
    }
}
