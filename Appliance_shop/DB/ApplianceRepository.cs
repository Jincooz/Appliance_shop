using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.DB
{
    struct ApplianceAmount 
    {
        public Appliance appliance;
        public int amount;
        public ApplianceAmount(Appliance appliance, int amount)
        {
            this.amount = amount;
            this.appliance = appliance;
        }
    }
    public class ApplianceRepository : IRepository
    {
        private List<ApplianceAmount> _appliances;
        public ApplianceRepository()
        {
            Load("");
        }

        private List<ApplianceAmount> Appliances { get => _appliances; set => _appliances = value; }
        private string FormSql(string aditionalRequest, string orderBy, int page)
        {
            string sql = "";
            if (aditionalRequest != "") sql += aditionalRequest;
            if (orderBy != "") sql += " ORDER BY " + Appliance.RealName(orderBy);
            sql += " LIMIT " + ActiveUser.Instance.RowsOnPage.ToString() + " OFFSET " + (page * ActiveUser.Instance.RowsOnPage).ToString();
            return sql;
        }
        public void Load(string aditionalRequest, string orderBy="", int page=0)
        {
            Appliances = new List<ApplianceAmount>();
            var data = DB.Instance.SelectAvaliableDevices(" EAN, amount ", FormSql(aditionalRequest, orderBy, page));
            foreach (var keyValuesPair in data)
            {
                (string key, object value) keyValuePair;
                for (int i = 0; i < keyValuesPair.Value.Count; i++)
                {
                    if(i == Appliances.Count)
                        Appliances.Add(new ApplianceAmount(new Appliance(), 0));
                    keyValuePair = (keyValuesPair.Key, keyValuesPair.Value[i]);
                    switch ((string)keyValuePair.key)
                    {
                        case "EAN":
                            {
                                Appliances[i].appliance.EAN = (string)keyValuePair.value;
                                Appliances[i].appliance.LoadByEAN();
                                break;
                            }
                        case "amount":
                            {
                                int amountOfAppliances = Convert.ToInt32(keyValuePair.value);
                                var AppliaRef = Appliances[i];
                                AppliaRef.amount = amountOfAppliances;
                                Appliances[i] = AppliaRef;
                                break;
                            }
                    }
                }
            }
        }
        private List<object> FormTableColumnsName()
        {
            List<object> result = new List<object>
            {
                "EAN",
                "Title",
                "Category",
                "Trademark",
                "Guaranty Time",
                "Price in UAH"
            };
            return result;
        }
        private List<object> FormTableRow(Appliance appliance)
        {
            List<object> result = new List<object>
            {
                appliance.EAN,
                appliance.Title,
                appliance.Category,
                appliance.Trademark,
                appliance.GuarantyTime,
                appliance.Price
            };
            return result;
        }
        public List<List<object>> FormTable()
        {
            List<List<object>> result = new List<List<object>>
            {
                FormTableColumnsName()
            };
            foreach (var ApplianceAmount in Appliances)
            {
                result.Add(FormTableRow(ApplianceAmount.appliance));
            }
            return result;
        }
        public void TableRowClicked(int row)
        {
            DialogResult result = MessageBox.Show(
                "You want add this one to your cart?",
                "Add to cart",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.Yes)
            {
                ActiveUser.Instance.User.ShopingList.Do(Appliances[row].appliance);
                var AplAmountRef = Appliances[row];
                AplAmountRef.amount -= 1;
                Appliances[row] = AplAmountRef;
            }
        }
        public int GetSize(string aditionalRequest)
        {
            string sql = "";
            if (aditionalRequest != "") sql += aditionalRequest;
            var data = DB.Instance.SelectAvaliableDevices(" COUNT(*) AS size ",sql);
            return Convert.ToInt32(data["size"][0]);
        }
        public void ActionButtonClick()
        {
            throw new NotImplementedException();
        }
    }
}
