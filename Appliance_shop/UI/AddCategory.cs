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
    public partial class AddCategory : Form
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private void AproveButton_Click(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "")
                return;
            try
            { 
                DB.DB.Instance.AddCategory(NameTextBox.Text);
                this.Close();
            }
            catch (Exception excep)
            {
                if (excep.Message == "name'")
                    MessageBox.Show("This name reserved in database",
                                                "Error",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                else
                    throw new Exception(excep.Message);
            }

        }
    }
}
