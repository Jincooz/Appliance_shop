using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.UI
{
    public partial class ChangeUserRole : Form
    {
        string _roleName;
        bool _roleChanged = false;
        public string RoleName { get => _roleName; set => _roleName = value; }
        public bool RoleChanged { get => _roleChanged; set => _roleChanged = value; }

        public ChangeUserRole()
        {
            InitializeComponent();
            var res = DB.DB.Instance.GetEnumerableRole().Item2;
            roleComboBox.Items.AddRange(res.ToArray());
        }
        private void banButtun_Click(object sender, EventArgs e)
        {
            RoleName = "Banned";
            RoleChanged = true;
            this.Close();
        }
        private void aproveButton_Click(object sender, EventArgs e)
        {
            if (roleComboBox.Text == "") return;
            RoleName = roleComboBox.SelectedItem.ToString();
            RoleChanged = true;
            this.Close();
        }
    }
}
