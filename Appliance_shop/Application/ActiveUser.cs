using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DB;

namespace WindowsFormsApp1
{
    public class ActiveUser
    {
        private static ActiveUser _instance;
        public static ActiveUser Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ActiveUser();
                }
                return _instance;
            }
        }
        private ActiveUser()
        {
            Role = "guest";
            User.Rights = DB.DB.Instance.GetRoleRights(Role);
        }
        private DB.User user = new DB.User();
        private int _rowsOnPage = 10;
        public string Role { private set => User.RoleName = value; get => User.RoleName; }
        public int ID { private set => User.Id = value; get => User.Id; }
        internal User User { get => user; set => user = value; }
        public int RowsOnPage { get => _rowsOnPage; }
        public Rights Rights { get => User.Rights; }
        public void CheckPassword(string password)
        {
            byte[] hashBytes = Convert.FromBase64String(user.HashedPassword);
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
            DB.DB.Instance.UpdateLastSeen(id);
        }
        public void Register(string login, string email, string phoneNumber, string roleName,
                       DateTime dateTime, string password)
        {
            DB.User user = new DB.User();
            user.Login = login;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            user.RoleName = roleName;
            user.LastLogIn = dateTime;
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            user.HashedPassword = Convert.ToBase64String(hashBytes);
            DB.DB.Instance.AddUser(user);
            user.LoadByLogin("id");
            user.ShopingList = new ShopingListRepository();
            try
            {
                user.ShopingList.Load("");
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            user.Rights = DB.DB.Instance.GetRoleRights(user.RoleName);
        }
        public void LogIn(string login, string password)
        {
            user = new DB.User();
            user.Login = login;
            user.LoadAllByLogin();
            if (!user.Enabled)
                throw new Exception("You are banned");
            CheckPassword(password);
            UpdateLastSeen(user.Id);
            user.ShopingList = new ShopingListRepository();
            user.ShopingList.Load("");
            user.Rights = DB.DB.Instance.GetRoleRights(user.RoleName);
        }
        public void UpdateUser(string login, string email, string phoneNumber)
        {
            User newUser = new User { 
                Id = user.Id,
                Login = login,
                Email = email,
                PhoneNumber = phoneNumber
            };
            DB.DB.Instance.UpdateUserData(newUser);
            user.Login = login;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
        }
        public void RestorePassword(string login, string email, string phoneNumber, string password)
        {
            user.Login = login;
            user.LoadAllByLogin();
            if (user.Email != email || user.PhoneNumber != phoneNumber)
                throw new Exception();
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            user.HashedPassword = Convert.ToBase64String(hashBytes);
            DB.DB.Instance.UpdatePassword(user.Id, user.HashedPassword);
        }
    }
}
