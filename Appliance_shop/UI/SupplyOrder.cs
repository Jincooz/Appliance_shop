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
    public partial class SupplyOrder : Form
    {
        private bool _new;
        private DB.ShopingListRepository _data;
        private bool New { get => _new; set => _new = value; }
        private DB.ShopingListRepository Data { get => _data; set => _data = value; }
        public SupplyOrder()
        {
            InitializeComponent();
            Data = new DB.ShopingListRepository();
            Data.Appliances = new List<DB.ApplianceAmount>();
            HideAddNew();
        }
        private void ShowAddNew()
        {
            New = true;
            AddButton.Location = new System.Drawing.Point(59, 221);
            TitleLabel.Visible = true;
            TitleTextBox.Visible = true;
            CategoryLabel.Visible = true;
            CategoryTextBox.Visible = true;
            TrademarkLabel.Visible = true;
            TrademarkTextBox.Visible = true;
            PriceLabel.Visible = true;
            PriceTextBox.Visible = true;
            GuarantyLabel.Visible = true;
            GuarantyTextBox.Visible = true;
        }
        private void HideAddNew()
        {
            New = false;
            AddButton.Location = new System.Drawing.Point(59, 68);
            TitleLabel.Visible = false;
            TitleTextBox.Visible = false;
            CategoryLabel.Visible = false;
            CategoryTextBox.Visible = false;
            TrademarkLabel.Visible = false;
            TrademarkTextBox.Visible = false;
            PriceLabel.Visible = false;
            PriceTextBox.Visible = false;
            GuarantyLabel.Visible = false;
            GuarantyTextBox.Visible = false;
        }
        private bool CheckInput()
        {
            var result = true;
            if (EANMaskedTextBox.Text.Contains('_'))
            {
                EANMaskedTextBox.Focus();
                errorProvider.SetError(EANMaskedTextBox, "Full EAN");
                result = false;
            }
            if (AmountTextBox.Text == "")
            {
                AmountTextBox.Focus();
                errorProvider.SetError(AmountTextBox, "No empty amount");
                result = false;
            }
            if(!New)
                return result;

            if (CategoryTextBox.Text == "")
            {
                CategoryTextBox.Focus();
                errorProvider.SetError(CategoryTextBox, "No empty category");
                result = false;
            }
            if (TrademarkLabel.Text == "")
            {
                TrademarkLabel.Focus();
                errorProvider.SetError(TrademarkLabel, "No empty trademark");
                result = false;
            }
            if (PriceTextBox.Text == "")
            {
                PriceTextBox.Focus();
                errorProvider.SetError(PriceTextBox, "No empty price");
                result = false;
            }
            if (GuarantyTextBox.Text == "")
            {
                GuarantyTextBox.Focus();
                errorProvider.SetError(GuarantyTextBox, "No empty guatanty");
                result = false;
            }
            return result; 
        }
        private void InitializeTable()
        {
            var table = Data.FormTable();
            TableView.Columns.Clear();
            var ColumnsNames = table[0];
            for (int i = 0; i < ColumnsNames.Count; i++)
            {
                TableView.Columns.Add((string)ColumnsNames[i], (string)ColumnsNames[i]);
            }
            for (int i = 0; i < table.Count - 1; i++)
            {
                TableView.Rows.Add();
                for (int j = 0; j < ColumnsNames.Count; j++)
                {
                    TableView[j, i].Value = table[i+1][j];
                }
            }

        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            CheckInput();
            if (New) 
            {
                var trademarks = DB.DB.Instance.GetEnumerableTrademark();
                var category = DB.DB.Instance.GetEnumerableCategory();
                if(!trademarks.Item2.Contains(TrademarkTextBox.Text.ToLower()))
                {
                    var result = MessageBox.Show("Can`t find this trademark in database. Add new?",
                                                    "Error",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        AddTrademark addTrademark = new AddTrademark();
                        addTrademark.NameTextBox.Text = TrademarkTextBox.Text;
                        addTrademark.ShowDialog();
                        AddButton_Click(sender, e);
                    }
                    return;
                }
                if (!category.Item2.Contains(CategoryTextBox.Text.ToLower()))
                {
                    var result = MessageBox.Show("Can`t find this category in database. Add new?",
                                                    "Error",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        AddCategory addCategory = new AddCategory();
                        addCategory.NameTextBox.Text = CategoryTextBox.Text;
                        addCategory.ShowDialog();
                        AddButton_Click(sender, e);
                    }
                    return;
                }
                DB.Appliance appliance = new DB.Appliance
                {
                    EAN = EANMaskedTextBox.Text,
                    Category = CategoryTextBox.Text,
                    Trademark = TrademarkTextBox.Text,
                    GuarantyTime = Convert.ToInt32(GuarantyTextBox.Text),
                    Title = TitleTextBox.Text,
                    Price = Math.Round(Convert.ToDouble(PriceTextBox.Text)*1.25,2)
                };
                try
                {
                    DB.DB.Instance.AddAppliacne(appliance.EAN, appliance.Title, appliance.Price, appliance.Category, appliance.Trademark, appliance.GuarantyTime);
                    HideAddNew();
                    Data.Appliances.Add(new DB.ApplianceAmount(appliance, Convert.ToInt32(AmountTextBox.Text)));
                }
                catch (Exception excep)
                {
                    if (excep.Message == "name'")
                        MessageBox.Show("This title reserved in database",
                                                    "Error",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                for (int i = 0; i < Data.Appliances.Count; i++)
                {
                    if(Data.Appliances[i].appliance.EAN == EANMaskedTextBox.Text)
                    {
                        var refer = Data.Appliances[i];
                        refer.amount += Convert.ToInt32(AmountTextBox.Text);
                        Data.Appliances[i] = refer;
                        InitializeTable();
                        return;
                    }
                }
                DB.Appliance appliance = new DB.Appliance
                {
                    EAN = EANMaskedTextBox.Text
                };
                try
                {
                    appliance.LoadByEAN();
                    Data.Appliances.Add(new DB.ApplianceAmount(appliance, Convert.ToInt32(AmountTextBox.Text)));
                }
                catch (Exception excep)
                {
                    if (excep.Message != "New")
                        throw new Exception();
                    var result = MessageBox.Show("Can`t find this EAN in database. Add new?",
                                                    "Error",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        ShowAddNew();
                    }
                }
            }
            InitializeTable();
            approvalButton.Visible = true;
        }
        private void AmountTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < AmountTextBox.Text.Length; i++)
            {
                char c = AmountTextBox.Text[i];
                if (c < '0' || c > '9')
                    AmountTextBox.Text = "";
            }
        }
        private void GuarantyTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GuarantyTextBox.Text.Length; i++)
            {
                char c = GuarantyTextBox.Text[i];
                if (c < '0' || c > '9')
                    GuarantyTextBox.Text = "";
            }
        }
        private void PriceTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < PriceTextBox.Text.Length; i++)
            {
                char c = PriceTextBox.Text[i];
                if (c != '.' && (c < '0' || c > '9'))
                    PriceTextBox.Text = "";
            }
        }
        private void approvalButton_Click(object sender, EventArgs e)
        {
            double sum = 0;
            DB.DB.Instance.AddSupplyOrder(ActiveUser.Instance.ID);
            foreach(var applianceAmount in Data.Appliances)
            {
                DB.DB.Instance.CreateApplianceAmount(applianceAmount.appliance.EAN, applianceAmount.amount);
                sum += applianceAmount.amount * applianceAmount.appliance.Price;
            }
            DB.DB.Instance.SupplyTransaction(ActiveUser.Instance.ID,sum);
            this.Close();
        }
    }
}
