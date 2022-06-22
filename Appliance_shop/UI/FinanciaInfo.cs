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
    public partial class FinanciaInfo : Form
    {
        public FinanciaInfo()
        {
            InitializeComponent();
        }

        private void FinanciaInfo_Load(object sender, EventArgs e)
        {
            var rows = DB.DB.Instance.GetEnumerableAccounts().Item2;
            double debitSum = 0, creditSum = 0;
            foreach (var row in rows)
            {
                double debit = DB.DB.Instance.GetDebitInfo(Convert.ToInt32(row)), credit = DB.DB.Instance.GetCreditInfo(Convert.ToInt32(row));
                if(debit > credit)
                {
                    debit = debit - credit;
                    debitSum += debit;
                    credit = 0;
                }
                else
                {
                    credit = credit - debit;
                    creditSum += credit;
                    debit = 0;
                }
                dataGridView1.Rows.Add(row.ToString(), debit.ToString(), credit.ToString());
            }
            if (debitSum > creditSum)
            {
                debitSum = debitSum - creditSum;
                creditSum = 0;
            }
            else
            {
                creditSum = creditSum - debitSum;
                debitSum = 0;
            }
            dataGridView1.Rows.Add("sum", debitSum.ToString(), creditSum.ToString());
        }
    }
}
