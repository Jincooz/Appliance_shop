using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DB
{
    interface IRepository
    {
        void Load(string aditionalRequest, string orderBy = "", int page = 0);
        List<List<object>> FormTable();
        void TableRowClicked(int row);
        void ActionButtonClick();
        int GetSize(string aditionalRequest);
    }
}
