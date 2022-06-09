using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DB
{
    class ShopingListRepository : Repository
    {
        private List<ApplianceAmount> _appliances;
        internal List<ApplianceAmount> Appliances { get => _appliances; set => _appliances = value; }
        private string FormSql()
        {
            string sql = "SELECT user_id, appliance_EAN, amount FROM shoping_list WHERE user_id = \"" + ActiveUser.Instance.ID + "\"";
            return sql;
        }
        public void Load(string aditionalRequest, string orderBy = "", int page = 0)
        {
            Appliances = new List<ApplianceAmount>();
            int amount = DB.Instance.GetAmountOfRequests("shoping_list WHERE user_id = \"" + ActiveUser.Instance.ID + "\"");
            for (int i = 0; i < amount; i++)
            {
                Appliances.Add(new ApplianceAmount(new Appliance(),0));
            }
            var data = DB.Instance.Select(FormSql());
            foreach (var keyValuesPair in data)
            {
                (string key, object value) keyValuePair;
                for (int i = 0; i < keyValuesPair.Value.Count; i++)
                {
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
                                break;
                            }
                    }
                }
            }
        }
        public List<List<object>> FormTable()
        {
            List<List<object>> result = new List<List<object>>();
            result.Add(Appliance.FormTableColumnsName());
            foreach (var ApplianceAmount in Appliances)
            {
                result.Add(ApplianceAmount.appliance.FormTableRow());
            }
            return result;
        }
    }
}
