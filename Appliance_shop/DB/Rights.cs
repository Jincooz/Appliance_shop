using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DB
{
    public class Rights
    {
        private Dictionary<string, bool> _rights;

        public bool ShowAppliancesToolStripMenuItem { get => GetRight("viewappliance");}
        public bool ShowLogInToolStripMenuItem { get => GetRight("login"); }
        public bool ShowRegisterToolStripMenuItem { get => GetRight("register"); }
        public bool ShowShopinglistToolStripMenuItem { get => GetRight("haveshopinglist"); }
        public bool ShowChangeProfileToolStripMenuItem { get => GetRight("changeprifile"); }
        public bool ShowLogOutToolStripMenuItem { get => GetRight("logout"); }
        public bool ShowAdminToolStripMenuItem { get => GetRight("viewfinancialinfo") || GetRight("viewusers") || GetRight("addsuply"); }
        public bool ShowGetFinancialInfoToolStripMenuItem { get => GetRight("viewfinancialinfo"); }
        public bool ShowSeeUserListToolStripMenuItem { get => GetRight("viewusers"); }
        public bool ShowAddSuplyToolStripMenuItem { get => GetRight("addsuply"); }
        public bool ActiveTableRowHeader { get => GetRight("clickrowheader"); }
        public Rights()
        {
            _rights = new Dictionary<string, bool>();
        }
        private void Add()
        {
            return;
        }
        public void SetRoleRights(List<object> data)
        {
            foreach (object right in data)
            {
                _rights[(string)right] = true;
            }
        }

        private bool GetRight(string title)
        {
            if (_rights.Keys.Contains(title))
            {
                return _rights[title];
            }
            else
            {
                return false;
            }
        }
    }
}
