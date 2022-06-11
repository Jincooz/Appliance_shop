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
    public partial class GetCard : Form
    {
        private bool _aprove;
        public bool Aprove { get => _aprove; set => _aprove = value; }
        public GetCard()
        {
            InitializeComponent();
            Aprove = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Aprove = true;
            this.Close();
        }
    }
}
