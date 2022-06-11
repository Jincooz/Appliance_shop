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
    public partial class Log_in : Form
    {
        public Log_in()
        {
            InitializeComponent();
        }
        private void logInButton_Click(object sender, EventArgs e)
        {
            DB.User user = new DB.User();
            try
            {
                user.LogIn(loginTextBox.Text, passwordTextBox.Text);
                ActiveUser.Instance.User = user;
                this.Close();
            }
            catch (Exception exept)
            {
                if(exept.Message == "You are banned")
                    MessageBox.Show("You are banned", "Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Invalid login or password", "Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UI.ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.ShowDialog();
        }
    }
}
