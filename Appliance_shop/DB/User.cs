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
        private void LoadAllByLogin()
        {
            LoadByLogin("id, email, hashed_password, Enabled, phone_number, creation_time, last_active_time, Role");
        }
        private void LoadByLogin(string variables)
        {
            var data = DB.Instance.Select(FormSql(variables));
            foreach (var keyValuePair in data)
            {
                Add((keyValuePair.Key, keyValuePair.Value[0]));
            }
        }
        public void CheckPassword(string password)
        {
            byte[] hashBytes = Convert.FromBase64String(HashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();
        }
        private void UpdateLastSeen(int id)
        {
            DB.Instance.UpdateLastSeen(id);
        }
        public void Register(string login, string email, string phoneNumber, string roleName,
                       DateTime dateTime, string password)
        {
            Login = login;
            Email = email;
            PhoneNumber = phoneNumber;
            RoleName = roleName;
            LastLogIn = dateTime;
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            HashedPassword = Convert.ToBase64String(hashBytes);
            DB.Instance.AddUser(login:Login,
                                email:Email,
                                phoneNumber:PhoneNumber,
                                roleName:RoleName,
                                hashedPassword:HashedPassword);
            LoadByLogin("id");
            ShopingList = new ShopingListRepository();
            try
            {
                ShopingList.Load("");
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            Rights = new Rights(RoleName);
        }
        public void LogIn(string login, string password)
        {
            Login = login;
            LoadAllByLogin();
            if (!Enabled)
                throw new Exception("You are banned");
            CheckPassword(password);
            UpdateLastSeen(Id);
            ShopingList = new ShopingListRepository();
            ShopingList.Load("");
            Rights = new Rights(RoleName);
        }
        public void UpdateUser(string login, string email, string phoneNumber)
        {
            DB.Instance.UpdateUserData(Id, login, email, phoneNumber);
            Login = login;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public void RestorePassword(string login, string email, string phoneNumber, string password)
        {
            Login = login;
            LoadAllByLogin();
            if (Email != email || PhoneNumber != phoneNumber)
                throw new Exception();
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            HashedPassword = Convert.ToBase64String(hashBytes);
            DB.Instance.UpdatePassword(Id, HashedPassword);
        }
    }
}
