using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1.DB
{
    class DB
    {
        private static DB _instance;
        private readonly MySqlConnection connection;
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
        public Dictionary<string, List<Object>> Select(string statement)
        {
            Dictionary<string,List<Object>> result = new Dictionary<string, List<Object>>();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection,
                CommandText = statement
            };
            try
            {
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
                                    result.Add(reader.GetName(i), new List<object>());
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
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        private bool IsInDB(string statement)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection,
                CommandText = statement
            };
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }
        private void Procedure(string statement)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection,
                CommandText = statement
            };
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Duplicate entry"))
                    throw new Exception(e.Message.Substring(e.Message.IndexOf("uc_") + 3));
                else
                    throw e;
            }
            finally
            {
                connection.Close();
            }
        }
        public Dictionary<string, List<Object>> SelectAvaliableDevices(string selectingItems, string aditionalRequest)
        {
            string sql = $"SELECT {selectingItems} FROM avaliable_devices_full WHERE amount > 0 " + aditionalRequest;
            return DB.Instance.Select(sql);
        }
        public Dictionary<string, List<Object>> SelectAvaliableDevice(string selectingItems, string EAN)
        {
            string sql = $"SELECT {selectingItems} FROM avaliable_devices WHERE EAN = {EAN}";
            return DB.Instance.Select(sql);
        }
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
        public (string, List<object>) GetEnumerableAccounts()
        {
            string sql = "SELECT id FROM CHART_OF_ACCOUNTS";
            var result = Select(sql);
            return ("Account", result["id"]);
        }
        public List<ApplianceAmount> SelectShopingList()
        {
            List<ApplianceAmount> appliances = new List<ApplianceAmount>();
            string sql = $"SELECT appliance_EAN AS EAN, amount FROM shoping_list WHERE user_id = \"" + ActiveUser.Instance.ID + "\"";
            var data = DB.Instance.Select(sql);
            foreach (var keyValuesPair in data)
            {
                (string key, object value) keyValuePair;
                for (int i = 0; i < keyValuesPair.Value.Count; i++)
                {
                    if (i == appliances.Count)
                        appliances.Add(new ApplianceAmount(new Appliance(), 0));
                    keyValuePair = (keyValuesPair.Key, keyValuesPair.Value[i]);
                    switch ((string)keyValuePair.key)
                    {
                        case "EAN":
                            {
                                appliances[i].appliance.EAN = (string)keyValuePair.value;
                                appliances[i].appliance.LoadByEAN();
                                break;
                            }
                        case "amount":
                            {
                                int amountOfAppliances = Convert.ToInt32(keyValuePair.value);
                                var AppliaRef = appliances[i];
                                AppliaRef.amount = amountOfAppliances;
                                appliances[i] = AppliaRef;
                                break;
                            }
                    }
                }
            }
            return appliances;
        }
        public bool TrademarkIsInDB(string trademark)
        {
            string sql = $"SELECT name FROM TRADEMARK WHERE name = '{trademark}'";
            return IsInDB(sql);
        }
        public bool CategoryIsInDB(string category)
        {
            string sql = $"SELECT name FROM TRADEMARK WHERE name = '{category}'";
            return IsInDB(sql);
        }
        public bool EANIsInDB(string EAN)
        {
            string sql = $"SELECT name FROM TRADEMARK WHERE EAN = '{EAN}'";
            return IsInDB(sql);
        }
        public void UpdateLastSeen(int id)
        {
            string sql = $"CALL UPDATE_LAST_SEEN({id})";
            DB.Instance.Procedure(sql);
        }
        public void AddUser(User user)
        {
            string sql = $"CALL ADD_USER('{user.Login}','{user.Email}','{user.HashedPassword}','{user.PhoneNumber}','{user.RoleName}');";
            DB.Instance.Procedure(sql);
        }
        public void UpdateUserData(User user)
        {
            string sql = $"CALL UPDATE_USER_DATA('{user.Id}','{user.Login}','{user.Email}','{user.PhoneNumber}');";
            DB.Instance.Procedure(sql);
        }
        public void UpdateUserRole(int id, string roleName)
        {
            string sql = $"CALL UPDATE_USER_ROLE('{id}','{roleName}');";
            DB.Instance.Procedure(sql);
        }
        public void EnableUser(int id)
        {
            string sql = $"CALL ENABLE_USER({id})";
            DB.Instance.Procedure(sql);
        }
        public void DisableUser(int id)
        {
            string sql = $"CALL DISSABLE_USER({id})";
            DB.Instance.Procedure(sql);
        }
        public void UpdatePassword(int id, string newHashedPassword)
        {
            string sql = $"CALL UPDATE_PASSWORD('{newHashedPassword}','{id}');";
            DB.Instance.Procedure(sql);
        }
        public void AddAppliacne(Appliance appliance)
        {
            string sql = $"CALL ADD_APPLIANCE('{appliance.EAN}','{appliance.Title}','{appliance.Price}','{appliance.Category}','{appliance.Trademark}','{appliance.GuarantyTime}');";
            DB.Instance.Procedure(sql);
        }
        public void AddCategory(string name)
        {
            string sql = $"CALL ADD_APPLIANCE_CATEGORY('{name}');";
            DB.Instance.Procedure(sql);
        }
        public void AddTrademark(string name)
        {
            string sql = $"CALL ADD_TRADEMARK('{name}');";
            DB.Instance.Procedure(sql);
        }
        public void AddUserOrder(int user_id)
        {
            string sql = $"CALL ADD_USER_ORDER('{user_id}');";
            DB.Instance.Procedure(sql);
        }
        public void AddSupplyOrder(int user_id)
        {
            string sql = $"CALL ADD_SUPPLY_ORDER('{user_id}');";
            DB.Instance.Procedure(sql);
        }
        private bool OrderForUserDontExist(int user_id)
        {
            string sql = $"SELECT COUNT(*) AS amount FROM ORDERS o WHERE o.user_id = {user_id} AND o.Active = 1";
            var res = Select(sql);
            return Convert.ToInt32(res["amount"][0]) == 0;
        }
        public void CreateApplianceAmount(string EAN, int amount)
        {
            if (OrderForUserDontExist(ActiveUser.Instance.ID))
                AddUserOrder(ActiveUser.Instance.ID);
            string sql = $"CALL ADD_ORDER_ITEM('{EAN}','{ActiveUser.Instance.ID}','{amount}');";
            DB.Instance.Procedure(sql);
        }
        public void UpdateApplianceAmount(string EAN, int amount)
        {
            string sql = $"CALL UPDATE_ORDER_ITEM('{EAN}','{ActiveUser.Instance.ID}','{amount}');";
            DB.Instance.Procedure(sql);
        }
        public void DeleteApplianceAmount(string EAN)
        {
            string sql = $"CALL DELETE_ORDER_ITEM('{EAN}','{ActiveUser.Instance.ID}');";
            DB.Instance.Procedure(sql);
        }
        public int GetAmountOfRequests(string name)
        {
            string sql = "SELECT count(*) AS amount FROM " + name;
            var result = Select(sql);
            return Convert.ToInt32(result["amount"][0]);
        }
        public int GetAmountOfShopingElements()
        {
            string sql = "SELECT count(*) AS amount FROM shoping_list WHERE user_id = \"" + ActiveUser.Instance.ID + "\"";
            var result = Select(sql);
            return Convert.ToInt32(result["amount"][0]);
        }
        public void BuyTransaction(int user_id, double sum)
        {
            string sql =    $"START TRANSACTION;" +
                            $"CALL ADD_TRANSACTION('361','702','User buy applianse','{user_id}','{sum}');" +
                            $"CALL ADD_TRANSACTION('311','361','User pay out','{user_id}','{sum}');" +
                            $"CALL ADD_TRANSACTION('902','281','Write off appliances from the warehouse','{user_id}','{sum*0.8}');" +
                            $"CALL ADD_TRANSACTION('702','791','Write-off of income on the financial result','{user_id}','{sum}');" +
                            $"CALL ADD_TRANSACTION('791','902','Write-off of value for financial result','{user_id}','{sum*0.8}');" +
                            $"CALL CLOSE_USER_ORDER('{user_id}');" +
                            $"COMMIT;\n";
            DB.Instance.Procedure(sql);
        }
        public void SupplyTransaction(int user_id, double sum)
        {
            string sql = $"START TRANSACTION;" +
                            $"CALL ADD_TRANSACTION('281','631','Shop buy applianse','{user_id}','{sum*0.8}');" +
                            $"CALL ADD_TRANSACTION('631','311','Shop pay out','{user_id}','{sum*0.8}');" +
                            $"CALL CLOSE_SUPPLY_ORDER('{user_id}');" +
                            $"COMMIT;";
            DB.Instance.Procedure(sql);
        }
        public double GetCreditInfo(int id)
        {
            connection.Open();
            double result = 0;
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection,
                CommandText = "CALC_CREDIT"
            };
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@chart_id", MySqlDbType.VarChar).Value = id.ToString();
                MySqlParameter resultParam = new MySqlParameter("@Result", MySqlDbType.Decimal);
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);
                cmd.ExecuteNonQuery();
                if (resultParam.Value != DBNull.Value)
                {
                    result = Convert.ToDouble(resultParam.Value);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public double GetDebitInfo(int id)
        {
            connection.Open();
            double result = 0;
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = connection,
                CommandText = "CALC_DEBIT"
            };
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@chart_id", MySqlDbType.VarChar).Value = id.ToString();
                MySqlParameter resultParam = new MySqlParameter("@Result", MySqlDbType.Decimal);
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);
                cmd.ExecuteNonQuery();
                if (resultParam.Value != DBNull.Value)
                {
                    result = Convert.ToDouble(resultParam.Value);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public Rights GetRoleRights(string role_name)
        {
            Rights rights = new Rights();
            string sql = $"SELECT right_name FROM role_rights_list WHERE role_name = '{role_name}'";
            var result = Select(sql);
            rights.SetRoleRights((List<object>)result["right_name"]);
            return rights;
        }
    }
}
