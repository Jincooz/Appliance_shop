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
    public partial class AddApplinceAmount : Form
    {
        private int _amount;
        private bool _approved = false;
        public int Amount { get => _amount; set => _amount = value; }
        public bool Approved { get => _approved; set => _approved = value; }
        public AddApplinceAmount()
        {
            InitializeComponent();
            _amount = 0;
        }
        private void amountTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < amountTextBox.Text.Length; i++)
            {
                char c = amountTextBox.Text[i];
                if (c < '0' || c > '9')
                    amountTextBox.Text = "";
            }
        }
        private void AproveButton_Click(object sender, EventArgs e)
        {
            Amount = int.Parse(amountTextBox.Text);
            Approved = true;
            this.Close();
        }
    }
}
