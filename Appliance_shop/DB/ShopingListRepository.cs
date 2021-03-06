using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DB
{
    class ShopingListRepository : IRepository
    {
        private List<ApplianceAmount> _appliances;
        internal List<ApplianceAmount> Appliances { get => _appliances; set => _appliances = value; }
        public int Size { get => _appliances.Count; }
        public void Load(string aditionalRequest, string orderBy = "", int page = 0)
        {
            Appliances = DB.Instance.SelectShopingList();
        }
        private List<object> FormTableColumnsName()
        {
            List<object> result = new List<object>
            {
                "EAN",
                "Title",
                "Guaranty Time",
                "Price in UAH",
                "Amount"
            };
            return result;
        }
        private List<object> FormTableRow(ApplianceAmount applianceAmount)
        {
            List<object> result = new List<object>
            {
                applianceAmount.appliance.EAN,
                applianceAmount.appliance.Title,
                applianceAmount.appliance.GuarantyTime,
                applianceAmount.appliance.Price,
                applianceAmount.amount
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
                result.Add(FormTableRow(ApplianceAmount));
            }
            return result;
        }
        public void TableRowClicked(int row)
        {
            UI.AddApplinceAmount applinceAmount = new UI.AddApplinceAmount();
            applinceAmount.ShowDialog();
            if (applinceAmount.Approved)
            {
                int maxAmount = Convert.ToInt32(DB.Instance.SelectAvaliableDevice(" amount ", Appliances[row].appliance.EAN)["amount"][0]);
                if(applinceAmount.Amount > maxAmount)
                {
                    applinceAmount.Amount = maxAmount;
                }
                var AppliaRef = Appliances[row];
                AppliaRef.amount = applinceAmount.Amount;
                Appliances[row] = AppliaRef;
                if (Appliances[row].amount == 0)
                {
                    DB.Instance.DeleteApplianceAmount(Appliances[row].appliance.EAN);
                    Appliances.RemoveAt(row);
                }
                else
                    DB.Instance.UpdateApplianceAmount(Appliances[row].appliance.EAN, applinceAmount.Amount);
            }
        }
        public void Do(Appliance new_appliance)
        {
            foreach (var applianceAmount in Appliances)
            {
                if (applianceAmount.appliance.EAN == new_appliance.EAN)
                    throw new Exception("Already in list");
            }
            DB.Instance.CreateApplianceAmount(new_appliance.EAN, 1);
            Appliances.Add(new ApplianceAmount(new_appliance, 1));
        }
        public int GetSize(string aditionalRequest)
        {
            return DB.Instance.GetAmountOfShopingElements();
        }
        public void ActionButtonClick()
        {
            double sum = 0;
            foreach(var appliaceAmount in Appliances)
            {
                sum += appliaceAmount.amount * appliaceAmount.appliance.Price;
            }
            DB.Instance.BuyTransaction(ActiveUser.Instance.ID, Math.Round(sum,2));
            _appliances.Clear();
        }
    }
}
