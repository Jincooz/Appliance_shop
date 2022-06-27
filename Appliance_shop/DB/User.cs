using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DB
{
    class User
    {
        private int _id;
        private string _login;
        private string _email;
        private string _hashedPassword;
        private bool _enabled;
        private string _phoneNumber;
        private DateTime _creationDateTime;
        private DateTime _lastLogIn;
        private string _roleName;
        private ShopingListRepository _shopingList;
        private Rights _rights;
        public int Id { get => _id; set => _id = value; }
        public string Login { get => _login; set => _login = value; }
        public string Email { get => _email; set => _email = value; }
        public string HashedPassword { get => _hashedPassword; set => _hashedPassword = value; }
        public bool Enabled { get => _enabled; set => _enabled = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public DateTime CreationDateTime { get => _creationDateTime; set => _creationDateTime = value; }
        public string RoleName { get => _roleName; set => _roleName = value; }
        public DateTime LastLogIn { get => _lastLogIn; set => _lastLogIn = value; }
        public ShopingListRepository ShopingList { get => _shopingList; set => _shopingList = value; }
        public Rights Rights { get => _rights; set => _rights = value; }
        public void Add((string name, object value) nameValuePair)
        {
            switch (nameValuePair.name)
            {
                case "id":
                    {
                        Id = Convert.ToInt32(nameValuePair.value);
                        break;
                    }
                case "login":
                    {
                        Login = (string)nameValuePair.value;
                        break;
                    }
                case "email":
                    {
                        Email = (string)nameValuePair.value;
                        break;
                    }
                case "hashed_password":
                    {
                        HashedPassword = (string)nameValuePair.value;
                        break;
                    }
                case "Enabled":
                    {
                        Enabled = (bool)nameValuePair.value;
                        break;
                    }
                case "phone_number":
                    {
                        PhoneNumber = (string)nameValuePair.value;
                        break;
                    }
                case "creation_time":
                    {
                        CreationDateTime = Convert.ToDateTime(nameValuePair.value);
                        break;
                    }
                case "last_active_time":
                    {
                        LastLogIn = Convert.ToDateTime(nameValuePair.value);
                        break;
                    }
                case "Role":
                    {
                        RoleName = (string)nameValuePair.value;
                        break;
                    }
                default:
                    {
                        File.AppendAllText("log.txt", "no " + nameValuePair.name + " in switch\n");
                        return;
                    }
            }
        }
        public List<object> FormTableRow()
        {
            List<object> result = new List<object>
            {
                Login,
                Email,
                PhoneNumber,
                CreationDateTime.ToString(),
                LastLogIn.ToString(),
                Enabled ? RoleName : "Banned"
            };
            return result;
        }
        public static List<object> FormTableColumnsName()
        {
            List<object> result = new List<object>
            {
                "Login",
                "Email",
                "Phone Number",
                "Creation moment",
                "Last seen online",
                "Role"
            };
            return result;
        }
        public static string RealName(string name)
        {
            bool desc = false;
            if (name.EndsWith(" desc "))
            {
                desc = true;
                name = name.Remove(name.Length - 6);
            }
            switch (name)
            {
                case "Login":
                    {
                        name = "login";
                        break;
                    }
                case "Email":
                    {
                        name = "email";
                        break;
                    }
                case "Phone Number":
                    {
                        name = "phone_number";
                        break;
                    }
                case "Creation moment":
                    {
                        name = "creation_time";
                        break;
                    }
                case "Last seen online":
                    {
                        name = "last_active_time";
                        break;
                    }
                case "Role":
                    {
                        name = "Enabled desc, Role";
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }
            return name + (desc ? " desc " : "");
        }
        private string FormSql(string variables)
        {
            string sql = "SELECT " + variables + " FROM users WHERE login = \"" + Login + "\"";
            return sql;
        }
        public void LoadAllByLogin()
        {
            LoadByLogin("id, email, hashed_password, Enabled, phone_number, creation_time, last_active_time, Role");
        }
        public void LoadByLogin(string variables)
        {
            var data = DB.Instance.Select(FormSql(variables));
            foreach (var keyValuePair in data)
            {
                Add((keyValuePair.Key, keyValuePair.Value[0]));
            }
        }
    }
}
