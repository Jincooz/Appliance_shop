using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DB
{
    public class UserRepository : Repository
    {
        private List<User> _users;
        public UserRepository()
        {
            Load("");
        }

        private List<User> Users { get => _users; set => _users = value; }
        private string FormSql(string aditionalRequest, string orderBy, int page)
        {
            string sql = "SELECT * FROM users";
            if (aditionalRequest != "") sql += aditionalRequest;
            if (orderBy != "") sql += " ORDER BY " + User.RealName(orderBy);
            sql += " LIMIT " + ActiveUser.Instance.RowsOnPage.ToString() + " OFFSET " + (page * ActiveUser.Instance.RowsOnPage).ToString();
            return sql;
        }
        public void Load(string aditionalRequest, string orderBy = "", int page = 0)
        {
            Users = new List<User>();
            var data = DB.Instance.Select(FormSql(aditionalRequest, orderBy, page));
            bool firstItteration = true;
            foreach (var keyValuesPair in data)
            {
                (string key, object value) keyValuePair;
                for (int i = 0; i < keyValuesPair.Value.Count; i++)
                {
                    keyValuePair = (keyValuesPair.Key, keyValuesPair.Value[i]);
                    if (firstItteration) Users.Add(new User());
                    Users[i].Add(keyValuePair);
                }
                if (firstItteration) firstItteration = false;
            }
        }
        public List<List<object>> FormTable()
        {
            List<List<object>> result = new List<List<object>>();
            result.Add(User.FormTableColumnsName());
            foreach (var user in Users)
            {
                result.Add(user.FormTableRow());
            }
            return result;
        }
    }
}
