using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class Appliance
    {
        private string _EAN;
        private string _title;
        private int _price;
        private string _trademark;
        private string _category;
        private int _guarantyTime;
        public string EAN { get => _EAN; set => _EAN = value; }
        public string Title { get => _title; set => _title = value; }
        public int Price { get => _price; set => _price = value; }
        public string Trademark { get => _trademark; set => _trademark = value; }
        public string Category { get => _category; set => _category = value; }
        public int GuarantyTime { get => _guarantyTime; set => _guarantyTime = value; }
        public void Add((string name, object value) nameValuePair)
        {
            switch(nameValuePair.name)
            {
                case "title":
                    {
                        Title = (string)nameValuePair.value;
                        break;
                    }
                case "EAN":
                    {
                        EAN = (string)nameValuePair.value;
                        break;
                    }
                case "price":
                    {
                        Price = Convert.ToInt32(nameValuePair.value);
                        break;
                    }
                case "trademark":
                    {
                        Trademark = (string)nameValuePair.value;
                        break;
                    }
                case "category":
                    {
                        Category = (string)nameValuePair.value;
                        break;
                    }
                case "guaranty_time":
                    {
                        GuarantyTime = Convert.ToInt32(nameValuePair.value);
                        break;
                    }
                default:
                    {
                        File.AppendAllText("log.txt", "no "+ nameValuePair.name + " in switch\n");
                        return;
                    }
            }
        }
        public List<object> FormTableRow()
        {
            List<object> result = new List<object>();
            result.Add(EAN);
            result.Add(Title);
            result.Add(Category);
            result.Add(Trademark);
            result.Add(GuarantyTime);
            result.Add(Price);
            return result;
        }
        public static List<object> FormTableColumnsName()
        {
            List<object> result = new List<object>();
            result.Add("EAN");
            result.Add("Title");
            result.Add("Category");
            result.Add("Trademark");
            result.Add("Guaranty Time");
            result.Add("Price in UAH");
            return result;
        }
        public static string RealName(string name)
        {
            bool desc = false;
            if (name.EndsWith(" desc "))
            {
                desc = true;
                name = name.Remove(name.Length - 6);
            }
            switch (name)
            {
                case "Title":
                    {
                        name = "title";
                        break;
                    }
                case "EAN":
                    {
                        name = "EAN";
                        break;
                    }
                case "Price in UAH":
                    {
                        name = "price";
                        break;
                    }
                case "Trademark":
                    {
                        name = "trademark";
                        break;
                    }
                case "Category":
                    {
                        name = "category";
                        break;
                    }
                case "Guaranty Time":
                    {
                        name = "guaranty_time";
                        break;
                    }
                default:
                    throw new NotImplementedException();                       
            }
            return name + (desc ? " desc ":"");
        }
        private string FormSql()
        {
            string sql = "SELECT title, price, trademark, category, guaranty_time FROM appliances_list WHERE EAN = \"" + EAN + "\"";
            return sql;
        }
        public void LoadByEAN()
        {
            var data = DB.Instance.Select(FormSql());
            foreach (var keyValuePair in data)
            {
                Add((keyValuePair.Key, keyValuePair.Value[0]));
            }
        }
    }
    public class ApplianceRepository : Repository
    {
        private List<ApplianceAmount> _appliances;
        public ApplianceRepository()
        {
            Load("");
        }

        private List<ApplianceAmount> Appliances { get => _appliances; set => _appliances = value; }
        private string FormSql(string aditionalRequest, string orderBy, int page)
        {
            string sql = "SELECT EAN, amount FROM avaliable_devices";
            if (aditionalRequest != "") sql += aditionalRequest;
            if (orderBy != "") sql += " ORDER BY " + Appliance.RealName(orderBy);
            sql += " LIMIT " + ActiveUser.Instance.RowsOnPage.ToString() + " OFFSET " + (page * ActiveUser.Instance.RowsOnPage).ToString();
            return sql;
        }
        public void Load(string aditionalRequest, string orderBy="", int page=0)
        {
            Appliances = new List<ApplianceAmount>();
            var data = DB.Instance.Select(FormSql(aditionalRequest, orderBy, page));
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
