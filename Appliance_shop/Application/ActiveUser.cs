using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DB;

namespace WindowsFormsApp1
{
    public struct Rights
    {
        bool showAppliancesToolStripMenuItem;
        bool showLogInToolStripMenuItem;
        bool showRegisterToolStripMenuItem;
        bool showShopinglistToolStripMenuItem;
        bool showChangeProfileToolStripMenuItem;
        bool showLogOutToolStripMenuItem;
        bool showAdminToolStripMenuItem;
        bool showGetFinancialInfoToolStripMenuItem;
        bool showSeeUserListToolStripMenuItem;
        bool showAddSuplyToolStripMenuItem;

        public bool ShowAppliancesToolStripMenuItem { get => showAppliancesToolStripMenuItem; set => showAppliancesToolStripMenuItem = value; }
        public bool ShowLogInToolStripMenuItem { get => showLogInToolStripMenuItem; set => showLogInToolStripMenuItem = value; }
        public bool ShowRegisterToolStripMenuItem { get => showRegisterToolStripMenuItem; set => showRegisterToolStripMenuItem = value; }
        public bool ShowShopinglistToolStripMenuItem { get => showShopinglistToolStripMenuItem; set => showShopinglistToolStripMenuItem = value; }
        public bool ShowChangeProfileToolStripMenuItem { get => showChangeProfileToolStripMenuItem; set => showChangeProfileToolStripMenuItem = value; }
        public bool ShowLogOutToolStripMenuItem { get => showLogOutToolStripMenuItem; set => showLogOutToolStripMenuItem = value; }
        public bool ShowAdminToolStripMenuItem { get => showAdminToolStripMenuItem; set => showAdminToolStripMenuItem = value; }
        public bool ShowGetFinancialInfoToolStripMenuItem { get => showGetFinancialInfoToolStripMenuItem; set => showGetFinancialInfoToolStripMenuItem = value; }
        public bool ShowSeeUserListToolStripMenuItem { get => showSeeUserListToolStripMenuItem; set => showSeeUserListToolStripMenuItem = value; }
        public bool ShowAddSuplyToolStripMenuItem { get => showAddSuplyToolStripMenuItem; set => showAddSuplyToolStripMenuItem = value; }
    }
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
        private Dictionary<string, Rights> _roleRights;
        private ActiveUser()
        {
            _roleRights = new Dictionary<string, Rights>();
            Role = "guest";
            _roleRights.Add("guest", new Rights
            {
                ShowAppliancesToolStripMenuItem = true,
                ShowLogInToolStripMenuItem = true,
                ShowRegisterToolStripMenuItem = true
            });
            _roleRights.Add("user", new Rights
            {
                ShowAppliancesToolStripMenuItem = true,
                ShowShopinglistToolStripMenuItem = true,
                ShowChangeProfileToolStripMenuItem = true,
                ShowLogOutToolStripMenuItem = true
            });
            _roleRights.Add("admin", new Rights
            {
                ShowAppliancesToolStripMenuItem = true,
                ShowShopinglistToolStripMenuItem = true,
                ShowChangeProfileToolStripMenuItem = true,
                ShowLogOutToolStripMenuItem = true,
                ShowAdminToolStripMenuItem = true,
                ShowGetFinancialInfoToolStripMenuItem = true,
                ShowSeeUserListToolStripMenuItem = true,
                ShowAddSuplyToolStripMenuItem = true
            });
        }
        private DB.User user = new DB.User();
        private int _rowsOnPage = 10;
        public string Role { private set => User.RoleName = value; get => User.RoleName; }
        public int ID { private set => User.Id = value; get => User.Id; }
        internal User User { get => user; set => user = value; }
        public int RowsOnPage { get => _rowsOnPage; }
        public Rights Rights { get => _roleRights[Role]; }
    }
}
