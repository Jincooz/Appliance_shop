using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.UI
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                DB.User user = new DB.User();
                try
                {
                    user.Register(loginTextBox.Text, emailTextBox.Text, phoneNumberMaskedTextBox.Text, "User",
                       DateTime.Now, passwordTextBox.Text);
                    ActiveUser.Instance.User = user;
                    this.Close();
                }
                catch (Exception expt)
                {
                    switch(expt.Message)
                    {
                        case "login'":
                            {
                                loginTextBox.Focus();
                                errorProvider.SetError(loginTextBox, "Someone has that login, if it`s you use restore password in Log In menu");
                                break;
                            }
                        case "email'":
                            {
                                emailTextBox.Focus();
                                errorProvider.SetError(emailTextBox, "Someone has that email, if it`s you use restore password in Log In menu");
                                break;
                            }
                        case "phone_number'":
                            {
                                phoneNumberMaskedTextBox.Focus();
                                errorProvider.SetError(phoneNumberMaskedTextBox, "Someone has that phone number, if it`s you use restore password in Log In menu");
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
        }

        private void phoneNumberMaskedTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            phoneNumberMaskedTextBox.Focus();
            errorProvider.SetError(phoneNumberMaskedTextBox, "Only numbers");
            return;
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
            if (phoneNumberMaskedTextBox.Text.Contains('_'))
            {
                phoneNumberMaskedTextBox.Focus();
                errorProvider.SetError(phoneNumberMaskedTextBox, "Full number");
                result = false;
            }
            if (!emailTextBox.Text.Contains("@") || emailTextBox.Text.Split('@').Length != 2)
            {
                emailTextBox.Focus();
                errorProvider.SetError(emailTextBox, "Email must have username and domain divaided by @");
                result = false;
            }
            if (loginTextBox.Text.Length < 3 || loginTextBox.Text.Length > 20)
            {
                loginTextBox.Focus();
                errorProvider.SetError(loginTextBox, "Length of login may be from 3 to 20 characters");
                result = false;
            }
            return result;
        }
    }
}
