using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DB
{
    class Appliance
    {
        private string _EAN;
        private string _title;
        private double _price;
        private string _trademark;
        private string _category;
        private int _guarantyTime;
        public string EAN { get => _EAN; set => _EAN = value; }
        public string Title { get => _title; set => _title = value; }
        public double Price { get => _price; set => _price = value; }
        public string Trademark { get => _trademark; set => _trademark = value; }
        public string Category { get => _category; set => _category = value; }
        public int GuarantyTime { get => _guarantyTime; set => _guarantyTime = value; }
        public void Add((string name, object value) nameValuePair)
        {
            switch (nameValuePair.name)
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
                        Price = Convert.ToDouble(nameValuePair.value);
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
                        File.AppendAllText("log.txt", "no " + nameValuePair.name + " in switch\n");
                        return;
                    }
            }
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
            return name + (desc ? " desc " : "");
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
                if (keyValuePair.Value.Count == 0)
                    throw new Exception("New");
                Add((keyValuePair.Key, keyValuePair.Value[0]));
            }
        }
    }
}
