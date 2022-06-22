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
            User.Rights = new Rights(Role);
        }
        private DB.User user = new DB.User();
        private int _rowsOnPage = 10;
        public string Role { private set => User.RoleName = value; get => User.RoleName; }
        public int ID { private set => User.Id = value; get => User.Id; }
        internal User User { get => user; set => user = value; }
        public int RowsOnPage { get => _rowsOnPage; }
        public Rights Rights { get => User.Rights; }
    }
}
