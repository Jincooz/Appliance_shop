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
    public partial class ChangeProfile : Form
    {
        public ChangeProfile()
        {
            InitializeComponent();
            loginTextBox.Text = ActiveUser.Instance.User.Login;
            emailTextBox.Text = ActiveUser.Instance.User.Email;
            phoneNumberMaskedTextBox.Text = ActiveUser.Instance.User.PhoneNumber;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                try
                {
                    ActiveUser.Instance.User.UpdateUser(loginTextBox.Text, emailTextBox.Text, phoneNumberMaskedTextBox.Text);
                    this.Close();
                }
                catch (Exception expt)
                {
                    switch (expt.Message)
                    {
                        case "login'":
                            {
                                loginTextBox.Focus();
                                errorProvider.SetError(loginTextBox, "Someone has that login");
                                break;
                            }
                        case "email'":
                            {
                                emailTextBox.Focus();
                                errorProvider.SetError(emailTextBox, "Someone has that email");
                                break;
                            }
                        case "phone_number'":
                            {
                                phoneNumberMaskedTextBox.Focus();
                                errorProvider.SetError(phoneNumberMaskedTextBox, "Someone has that phone number");
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
        private bool CheckInput()
        {
            bool result = true;
            errorProvider.Clear();
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
