using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DB;

namespace WindowsFormsApp1
{
    public abstract class Table
    {
        private List<string> _columnsNames;
        private List<List<Object>> _data;
        private List<Filter> _filters;
        private DB.Repository _repository;
        private string _orderBy;
        public List<List<Object>> Data { get => _data; set => _data = value; }
        public List<string> ColumnsNames { get => _columnsNames; set => _columnsNames = value; }
        internal List<Filter> Filters { get => _filters; }
        public string OrderBy { private get => _orderBy; set => _orderBy = value; }
        public int Count { get => _data.Count; }
        internal Repository Repository { get => _repository; set => _repository = value; }
        public Table()
        {
            _columnsNames = new List<string>();
            _data = new List<List<Object>>();
            _filters = new List<Filter>();
            OrderBy = "";
        }
        public List<Object> this[int i]
        {
            get => Data[i];
        }
        public Object this[int i, int j]
        {
            get => Data[i][j];
        }
        protected void SetDataInTable(List<List<Object>> table)
        {
            ColumnsNames.Clear();
            Data.Clear();
            for (int i = 0; i < table[0].Count; i++)
            {
                ColumnsNames.Add((string)table[0][i]);
            }
            for (int i = 1; i < table.Count; i++)
            {
                Data.Add(table[i]);
            }
        }
        protected string FormRequest()
        {
            string filterRequest = "";
            foreach (var filter in Filters)
            {
                string addStr = filter.GetRequest();
                if (addStr != "")
                    filterRequest += addStr + " AND ";
            }
            if (filterRequest != "") 
            {
                filterRequest = filterRequest.Insert(0, " WHERE ");
                filterRequest = filterRequest.Remove(filterRequest.Length - 4); 
            }
            return filterRequest;
        }
        protected void AddCountableFilter((string name, List<object> list) nameList)
        {
            List<string> result = new List<string>();
            string name = nameList.name;
            foreach (var element in nameList.list)
                result.Add((string)element);
            Filters.Add(new CountableFilter(name, result));
        }
        virtual public void GetDataFromRepository(int page) 
        {
            Repository.Load(FormRequest(), OrderBy, page);
            SetDataInTable(Repository.FormTable());
        }
    }

    public class ShopingListTable : Table
    {
        public ShopingListTable() : base()
        {
            Repository = new DB.ShopingListRepository();
        }
    }
    public class ApplianceTable : Table
    {
        public ApplianceTable():base()
        {
            Repository = new DB.ApplianceRepository(); 
            AddCountableFilter(DB.DB.Instance.GetEnumerableCategory());
            AddCountableFilter(DB.DB.Instance.GetEnumerableTrademark());
        }
    }

    public class UserTable : Table
    {
        public UserTable() : base()
        {
            Repository = new DB.UserRepository();
            AddCountableFilter(DB.DB.Instance.GetEnumerableRole());
        }
    }
}
