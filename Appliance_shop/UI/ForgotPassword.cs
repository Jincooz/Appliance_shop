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
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(CheckInput())
            {
                DB.User user = new DB.User();
                try
                {
                    user.RestorePassword(login: loginTextBox.Text,
                                            email: emailTextBox.Text,
                                            phoneNumber: phoneNumberMaskedTextBox.Text,
                                            password: passwordTextBox.Text);
                    this.Close();
                }catch (Exception exep)
                {
                    MessageBox.Show("Invalid data", "Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CheckInput()
        {
            bool result = true;
            errorProvider.Clear();
            if (passwordTextBox.Text == "")
            {
                passwordTextBox2.Focus();
                errorProvider.SetError(passwordTextBox2, "Password can`t be unfilled");
                result = false;
            }
            if (passwordTextBox2.Text != passwordTextBox.Text)
            {
                passwordTextBox2.Focus();
                errorProvider.SetError(passwordTextBox2, "Passwords not equals");
                result = false;
            }
            return result;
        }
    }
}
